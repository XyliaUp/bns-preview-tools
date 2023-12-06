using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using HandyControl.Controls;
using HandyControl.Data;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Search;

using Microsoft.Win32;

using Xylia.Preview.Data;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Helpers;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views;
public partial class TextView
{
	#region Constructor
	private readonly FoldingManager foldingManager;

	public TextView()
	{
		InitializeComponent();
		RegisterCommands(this.CommandBindings);

		var search = SearchPanel.Install(Editor);
		foldingManager = FoldingManager.Install(Editor.TextArea);
		Editor.TextArea.Caret.PositionChanged += (s, e) => OnPositionChanged(search);
	}
	#endregion

	#region Methods
	private void RegisterCommands(CommandBindingCollection commandBindings)
	{
		commandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveCommand, CanExecuteSave));
		commandBindings.Add(new CommandBinding(ApplicationCommands.Replace, ReplaceInFilesCommand, CanExecuteSave));
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		if (UserSettings.Default.Text_LoadPrevious)
		{
			OldSource = UserSettings.Default.Text_OldPath;
			NewSource = UserSettings.Default.Text_NewPath;
			RenderView();
		}
	}

	private void OpenLeftFileMenuItem_Click(object sender, RoutedEventArgs e)
	{
		if (OpenTextFile(out var file))
		{
			UserSettings.Default.Text_OldPath = OldSource = file;
			RenderView();
		}
	}

	private void OpenRightFileMenuItem_Click(object sender, RoutedEventArgs e)
	{
		if (OpenTextFile(out var file))
		{
			UserSettings.Default.Text_NewPath = NewSource = file;
			RenderView();
		}
	}


	private void CanExecuteSave(object sender, CanExecuteRoutedEventArgs e)
	{
		// only single file and left source
		e.CanExecute = OldSource != null && diffResult is null;
	}

	private void SaveCommand(object sender, RoutedEventArgs e)
	{
		var dialog = new SaveFileDialog
		{
			FileName = "TextData",
			Filter = "xml file|*.x16",
		};
		if (dialog.ShowDialog() == true)
		{
			File.WriteAllText(dialog.FileName, Editor.Text);
		}
	}

	private void ReplaceInFilesCommand(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFolderDialog();
		if (dialog.ShowDialog() == true)
		{
			var files = new DirectoryInfo(dialog.FolderName).GetFiles("*.x16", SearchOption.AllDirectories);

			Task.Run(() =>
			{
				try
				{
					using var source1 = new BnsDatabase(new LocalProvider(OldSource));
					(source1.Provider as LocalProvider).Rewrite(files);

					Growl.SuccessGlobal(new GrowlInfo() { Message = "Replace completed", StaysOpen = true });
				}
				catch (Exception ex)
				{
					Growl.WarningGlobal(ex.Message);
				}
			});
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


	private bool OpenTextFile(out string file)
	{
		var dialog = new OpenFileDialog
		{
			Filter = @"All files|*.*|game text file|local*.dat|output text file|TextData*.xml"
		};

		if (dialog.ShowDialog() == true)
		{
			file = dialog.FileName;
			return true;
		}

		file = null;
		return false;
	}
	#endregion


	#region Differ
	private string OldSource;
	private string NewSource;
	private List<TextDiffPiece> diffResult;

	private void RenderView()
	{
		#region Source
		using var source1 = new BnsDatabase(new LocalProvider(OldSource));
		using var source2 = new BnsDatabase(new LocalProvider(NewSource));

		bool IsEmpty1 = source1.Text is null || !source1.Text.Any();
		bool IsEmpty2 = source2.Text is null || !source2.Text.Any();

		// HeaderText
		if (IsEmpty1 && IsEmpty2) return;
		else if (!IsEmpty1 && !IsEmpty2) this.InlineHeaderText.Text = source1.Provider.Name + " → " + source2.Provider.Name;
		else
		{
			if (IsEmpty1) this.InlineHeaderText.Text = source2.Provider.Name;
			if (IsEmpty2) this.InlineHeaderText.Text = source1.Provider.Name;
		}
		#endregion

		#region Lines
		// compare
		if (!IsEmpty1 && !IsEmpty2)
		{
			var strategy = new TextFoldingStrategy();
			var builder = new StringBuilder();

			diffResult = TextDiff.Diff(source1.Text, source2.Text);

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
					strategy.Lines.Add(new TextArea() { Type = areaType, StartLine = areaStart, EndLine = i - 1 });

					areaType = line.Type;
					areaStart = i;
				}
			}

			strategy.Lines.Add(new TextArea() { Type = areaType, StartLine = areaStart, EndLine = diffResult.Count - 1 });


			Editor.Text = builder.ToString();
			strategy.UpdateFoldings(foldingManager, Editor.Document);
			strategy.UpdateRenders(Editor.TextArea.TextView);
		}
		else
		{
			diffResult = null;
			Editor.Text = (source1.Text ?? source2.Text).WriteXml();
		}
		#endregion
	}


	private void OnPositionChanged(Control sender)
	{
		// preview	
		var index = Editor.TextArea.Caret.Line - 1;
		if (diffResult is null)
		{
			var line = Editor.Document.Lines[index];
			var text = Editor.Document.Text.Substring(line.Offset, line.Length);

			sender.Tag = Record.Parse(text)?.Attributes;
		}
		else if (diffResult.Count <= index)
		{
			sender.Tag = diffResult[index];
		}
	}
	#endregion
}


#region Area
internal class TextArea
{
	public ChangeType Type;

	public int StartLine;
	public int EndLine;
}

internal class TextFoldingStrategy
{
	public List<TextArea> Lines { get; }

	public TextFoldingStrategy()
	{
		Lines = new();
	}


	public void UpdateFoldings(FoldingManager manager, TextDocument document)
	{
		var foldings = Lines.Where(x => x.StartLine < x.EndLine).Select(x => new NewFolding()
		{
			StartOffset = document.Lines[x.StartLine].Offset,
			EndOffset = document.Lines[x.EndLine].EndOffset,

			Name = x.Type.ToString(),
			DefaultClosed = x.Type == ChangeType.Unchanged
		});

		manager.UpdateFoldings(foldings, -1);
	}

	public void UpdateRenders(ICSharpCode.AvalonEdit.Rendering.TextView textView)
	{
		textView.BackgroundRenderers.Clear();

		foreach (var x in Lines)
		{
			if (x.Type == ChangeType.Unchanged) continue;
			textView.BackgroundRenderers.Add(new TextAreaRenderer(x));
		}
	}
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
			textView.VisualLines.Last().LastDocumentLine.LineNumber < Area.StartLine) return;

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

		// text
		//var format = new FormattedText(Area.Type.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, BackgroundBrush, 96);
		//format.SetFontWeight(FontWeights.UltraBold);
		//drawingContext.DrawText(format, new Point((textView.ActualWidth - format.Width) / 2, posY));
	}
}
#endregion