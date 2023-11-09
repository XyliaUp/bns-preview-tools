using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Search;

using Microsoft.Win32;

using Xylia.Preview.Data;
using Xylia.Preview.UI.Helpers;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views.Pages;
public partial class TextPage : Window
{
	#region Constructor
	private FoldingManager foldingManager;

	public TextPage()
	{
		InitializeComponent();

		var search = SearchPanel.Install(Editor);
		foldingManager = FoldingManager.Install(Editor.TextArea);
		Editor.TextArea.Caret.PositionChanged += (s, e) => OnPositionChanged(search);
	}
	#endregion

	#region Methods
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
		if (OldSource is null || NewSource is null) return;

		using var Source1 = new BnsDatabase(new LocalProvider(OldSource));
		using var Source2 = new BnsDatabase(new LocalProvider(NewSource));
		this.InlineHeaderText.Text = Source1.Provider.Name + " → " + Source2.Provider.Name;

		// init
		var strategy = new TextFoldingStrategy();
		var builder = new StringBuilder();
		diffResult = TextDiff.Diff(Source1.Text, Source2.Text);

		int areaStart = 0;
		var areaType = ChangeType.Unchanged;
		for (int i = 0; i < diffResult.Count; i++)
		{
			// text
			var line = diffResult[i];

			if (i != 0) builder.AppendLine();
			if (line.TextOld != null) builder.Append(line.TextOld + " → ");
			builder.Append(line.Text);

			// handle
			if (line.Type != areaType)
			{
				strategy.Lines.Add(new TextArea() { Type = areaType, StartLine = areaStart, EndLine = i - 1 });

				areaType = line.Type;
				areaStart = i;
			}
		}

		strategy.Lines.Add(new TextArea() { Type = areaType, StartLine = areaStart, EndLine = diffResult.Count - 1 });


		// set text
		Editor.Text = builder.ToString();
		strategy.UpdateFoldings(foldingManager, Editor.Document);
		strategy.UpdateRenders(Editor.TextArea.TextView);
	}

	private void OnPositionChanged(Control sender)
	{
		// get current line
		//var index2 = Editor.TextArea.Caret.Line - 1;
		//var line = Editor.Document.Lines[index2];
		//search.Tag = Editor.Document.Text.Substring(line.Offset, line.Length);

		// preview	
		var index = Editor.TextArea.Caret.Line - 1;
		if (diffResult is null || diffResult.Count <= index) return;

		sender.Tag = diffResult[index];
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

	public void UpdateRenders(TextView textView)
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

	public void Draw(TextView textView, DrawingContext drawingContext)
	{        
		// valid
		ArgumentNullException.ThrowIfNull(textView);
		ArgumentNullException.ThrowIfNull(drawingContext);

		if (textView.VisualLines is null ||
			textView.VisualLines.First().FirstDocumentLine.LineNumber > Area.EndLine ||
			textView.VisualLines.Last().LastDocumentLine.LineNumber < Area.StartLine) return;

		// pos
		var line1 = textView.GetVisualLine(Area.StartLine + 1);
		var line2 = textView.GetVisualLine(Area.EndLine + 1);

		var posY = line1 is null ? 0 : (line1.VisualTop - textView.ScrollOffset.Y);
		var height = textView.ActualHeight;
		if (line1 == null && line2 != null) height = line2.VisualTop + line2.Height;
		if (line1 != null && line2 != null) height = line2.VisualTop + line2.Height - line1.VisualTop;

		// background
		var geometry = new RectangleGeometry(new Rect(0, posY, textView.ActualWidth, height));
		if (geometry != null) drawingContext.DrawGeometry(BackgroundBrush, this.BorderPen, geometry);

		// text
		//var format = new FormattedText(Area.Type.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, BackgroundBrush, 96);
		//format.SetFontWeight(FontWeights.UltraBold);
		//drawingContext.DrawText(format, new Point((textView.ActualWidth - format.Width) / 2, posY));
	}
}
#endregion