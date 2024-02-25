using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using HandyControl.Controls;
using HandyControl.Interactivity;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Search;
using Microsoft.Win32;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Helpers;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views;
public partial class TextView
{
	#region Constructor
	private FoldingManager manager;

	public TextView()
	{
		InitializeComponent();
		RegisterCommands(this.CommandBindings);

		var search = SearchPanel.Install(Editor);
		manager = FoldingManager.Install(Editor.TextArea);
		Editor.TextArea.Caret.PositionChanged += (s, e) => OnPositionChanged(search);
	}
	#endregion

	#region Methods (UI)
	private void RegisterCommands(CommandBindingCollection commandBindings)
	{
		commandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveCommand, CanExecuteSave));
		commandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAsCommand, CanExecuteSaveAs));
		commandBindings.Add(new CommandBinding(ApplicationCommands.Replace, ReplaceInFilesCommand, CanExecuteSaveAs));

		// unable to edit in comparison mode
		commandBindings.Add(new CommandBinding(ControlCommands.Switch, delegate { }, CanExecuteSaveAs));
	}

	private async void Window_Loaded(object sender, RoutedEventArgs e)
	{
		if (UserSettings.Default.Text_LoadPrevious)
		{
			OldSource = UserSettings.Default.Text_OldPath;
			NewSource = UserSettings.Default.Text_NewPath;
			await RenderView();
		}
	}

	private async void OpenLeftFileMenuItem_Click(object sender, RoutedEventArgs e)
	{
		if (OpenTextFile(out var file))
		{
			UserSettings.Default.Text_OldPath = OldSource = file;
			await RenderView();
		}
	}

	private async void OpenRightFileMenuItem_Click(object sender, RoutedEventArgs e)
	{
		if (OpenTextFile(out var file))
		{
			UserSettings.Default.Text_NewPath = NewSource = file;
			await RenderView();
		}
	}

	private void InlineModeToggle_Click(object sender, RoutedEventArgs e)
	{

	}

	private void SideBySideModeToggle_Click(object sender, RoutedEventArgs e)
	{

	}

	private void CollapseUnchangedSectionsToggle_Click(object sender, RoutedEventArgs e)
	{

	}
	#endregion


	#region Methods
	bool inloading = false;

	private string OldSource;
	private string NewSource;
	private LocalProvider source;
	private List<TextDiffPiece> diffResult;

	private bool OpenTextFile(out string file)
	{
		var dialog = new OpenFileDialog
		{
			Filter = @"All files|*.*|game text file|local*.dat|output text file|*.x16"
		};

		if (dialog.ShowDialog() == true)
		{
			file = dialog.FileName;
			return true;
		}

		file = null;
		return false;
	}

	private async Task RenderView()
	{
		ReadStatus.IsChecked = false;

		#region Source
		var source1 = await Task.Run(() => new BnsDatabase(new LocalProvider(OldSource)));
		var source2 = await Task.Run(() => new BnsDatabase(new LocalProvider(NewSource)));

		var TextTable1 = source1.Get<Text>();
		var TextTable2 = source2.Get<Text>();

		var IsEmpty1 = TextTable1.IsEmpty();
		var IsEmpty2 = TextTable2.IsEmpty();
		#endregion

		#region Lines
		if (IsEmpty1 && IsEmpty2) return;
		else if (!IsEmpty1 && !IsEmpty2)
		{
			this.InlineHeaderText.Text = source1.Provider.Name + " → " + source2.Provider.Name;

			// create diff
			diffResult = await Task.Run(() => TextDiff.Diff(TextTable1, TextTable2));

			source?.Dispose();
			source = null;
			source1.Dispose();
			source2.Dispose();


			// areas
			var builder = new StringBuilder();
			var strategy = new TextAreaManager(this.Editor);

			int areaStart = 0;
			var areaType = ChangeType.Unchanged;
			for (int i = 0; i < diffResult.Count; i++)
			{
				// text
				var line = diffResult[i];

				if (i != 0) builder.AppendLine();
				if (line.oldtext != null) builder.Append(line.oldtext + " → ");
				builder.Append(line.text);

				// handle
				if (line.Type != areaType)
				{
					strategy.Add(new() { Type = areaType, StartLine = areaStart, EndLine = i - 1 });

					areaType = line.Type;
					areaStart = i;
				}
			}

			strategy.Add(new() { Type = areaType, StartLine = areaStart, EndLine = diffResult.Count - 1 });

			Editor.Text = builder.ToString();
			strategy.UpdateFoldings(manager);
			strategy.UpdateRenders();
		}
		else
		{
			diffResult = null;

			this.source = (IsEmpty2 ? source1 : source2).Provider as LocalProvider;
			this.InlineHeaderText.Text = source.Name;

			var settings = new TableWriterSettings() { Encoding = Encoding.Unicode };
			Editor.Text = await Task.Run(() => settings.Encoding.GetString((TextTable1 ?? TextTable2).Source.WriteXml(settings)));
		}
		#endregion
	}

	private void OnPositionChanged(Control sender)
	{
		#region Show
		var caret = Editor.TextArea.Caret;
		ColumnNumber.Text = caret.Column.ToString();

		var lineNum = Editor.TextArea.Caret.Line.ToString();
		if (LineNumber.Text == lineNum) return;
		else LineNumber.Text = lineNum;
		#endregion

		#region Preview
		var index = caret.Line - 1;
		if (diffResult is null)
		{
			var line = Editor.Document.Lines[index];
			var text = Editor.Document.Text.Substring(line.Offset, line.Length);

			sender.Tag = Text.Parse(text);
		}
		else if (diffResult.Count > index)
		{
			sender.Tag = diffResult[index];
		}
		#endregion
	}




	private void CanExecuteSave(object sender, CanExecuteRoutedEventArgs e)
	{
		// only single file and left source
		e.CanExecute = !inloading && source != null && source.CanSave;
	}

	private void CanExecuteSaveAs(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = !inloading && source != null;
	}

	private void SaveAsCommand(object sender, RoutedEventArgs e)
	{
		var dialog = new SaveFileDialog
		{
			FileName = "TextData",
			Filter = "xml file|*.x16",
		};
		if (dialog.ShowDialog() == true)
		{
			File.WriteAllText(dialog.FileName, Editor.Text, Encoding.Unicode);
		}
	}

	private void ReplaceInFilesCommand(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFolderDialog();
		if (dialog.ShowDialog() != true) return;

		Task.Run(() =>
		{
			try
			{
				var files = new DirectoryInfo(dialog.FolderName).GetFiles("*.x16", SearchOption.AllDirectories);
				if (files.Length == 0) throw new Exception(StringHelper.Get("TextView_NotExist_x16"));

				source.ReplaceText(files);

				// reload text
				var settings = new TableWriterSettings() { Encoding = Encoding.Unicode };
				var text = settings.Encoding.GetString(source.TextTable.WriteXml(settings));

				Dispatcher.Invoke(() => Editor.Text = text);
				Growl.Success(StringHelper.Get("TextView_ReplaceCompleted", nameof(TextView)));
			}
			catch (XmlException ex)
			{
				Growl.Error(string.Format("{1}\n{0}", ex.Message, ex.SourceUri), nameof(TextView));
			}
			catch (Exception ex)
			{
				Growl.Error(string.Format("{0}", ex.Message), nameof(TextView));
			}
		});
	}

	private void SaveCommand(object sender, RoutedEventArgs e)
	{
		inloading = true;

		Task.Run(() =>
		{
			try
			{
				var data = Encoding.Unicode.GetBytes(this.Dispatcher.Invoke(() => Editor.Text));
				source.Save(data);

				Growl.Success(StringHelper.Get("TextView_SaveCompleted"), nameof(TextView));
			}
			catch (Exception ex)
			{
				Growl.Error(ex.Message, nameof(TextView));
			}
			finally
			{
				inloading = false;
			}
		});
	}
	#endregion
}


#region Area
internal class TextAreaManager
{
	ICSharpCode.AvalonEdit.TextEditor _editor;
	List<TextArea> _areas;

	public TextAreaManager(ICSharpCode.AvalonEdit.TextEditor TextEditor)
	{
		_areas = new();
		_editor = TextEditor;
	}


	public void Add(TextArea area)
	{
		_areas.Add(area);
	}

	public void UpdateFoldings(FoldingManager manager)
	{
		var document = _editor.Document;

		var foldings = _areas.Where(x => x.StartLine < x.EndLine).Select(x => new NewFolding()
		{
			StartOffset = document.Lines[x.StartLine].Offset,
			EndOffset = document.Lines[x.EndLine].EndOffset,

			Name = x.Type.ToString(),
			DefaultClosed = x.Type == ChangeType.Unchanged
		});

		manager.UpdateFoldings(foldings, -1);
	}

	public void UpdateRenders()
	{
		var textView = _editor.TextArea.TextView;
		textView.BackgroundRenderers.Clear();

		foreach (var x in _areas)
		{
			if (x.Type == ChangeType.Unchanged) continue;
			textView.BackgroundRenderers.Add(new TextAreaRenderer(x));
		}
	}


	internal class TextArea
	{
		public ChangeType Type;

		public int StartLine;
		public int EndLine;
	}

	internal class TextAreaRenderer : IBackgroundRenderer
	{
		#region Fields	
		public static readonly Color DefaultBorder = Color.FromArgb(52, 0, 255, 110);
		public static readonly Color InsertedBackground = Color.FromArgb(64, 96, 216, 32);
		public static readonly Color ModifyedBackground = Color.FromArgb(64, 216, 32, 32);
		public static readonly Color DeletedBackground = Color.FromArgb(64, 216, 32, 32);

		public KnownLayer Layer => KnownLayer.Selection;
		private Pen BorderPen { get; set; }
		private SolidColorBrush BackgroundBrush { get; set; }

		private TextArea Area { get; set; }
		#endregion


		public TextAreaRenderer(TextArea area)
		{
			this.Area = area;

			this.BorderPen = new Pen(new SolidColorBrush(DefaultBorder), 1);
			this.BorderPen.Freeze();

			this.BackgroundBrush = area.Type switch
			{
				ChangeType.Inserted => new SolidColorBrush(InsertedBackground),
				ChangeType.Modified => new SolidColorBrush(ModifyedBackground),
				ChangeType.Deleted => new SolidColorBrush(DeletedBackground),
				_ => null
			};
			this.BackgroundBrush?.Freeze();
		}

		public void Draw(ICSharpCode.AvalonEdit.Rendering.TextView textView, DrawingContext drawingContext)
		{
			// valid
			ArgumentNullException.ThrowIfNull(textView);
			ArgumentNullException.ThrowIfNull(drawingContext);

			// view area
			if (textView.VisualLines is null ||
				textView.VisualLines.First().FirstDocumentLine.LineNumber > Area.EndLine ||
				textView.VisualLines.Last().LastDocumentLine.LineNumber <= Area.StartLine) return;

			var line1 = textView.GetVisualLine(Area.StartLine + 1);
			var line2 = textView.GetVisualLine(Area.EndLine + 1);


			// rect
			double posY = line1 is null ? 0 : (line1.VisualTop - textView.ScrollOffset.Y);
			double height = 0;
			if (line1 == null && line2 == null) height = textView.ActualHeight;
			if (line1 != null && line2 == null) height = textView.ActualHeight - posY;
			if (line1 == null && line2 != null) height = line2.VisualTop + line2.Height - textView.ScrollOffset.Y;
			if (line1 != null && line2 != null) height = line2.VisualTop + line2.Height - line1.VisualTop;

			// background
			if (height < 0) return;
			var geometry = new RectangleGeometry(new Rect(0, posY, textView.ActualWidth, height));
			if (geometry != null) drawingContext.DrawGeometry(BackgroundBrush, this.BorderPen, geometry);
		}
	}
}
#endregion