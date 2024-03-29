﻿using System.Windows;
using System.Xml;

using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Search;

namespace Xylia.Preview.UI.Views.Editor;
public partial class TextEditor : Window
{
	#region Constructor
	private FoldingManager foldingManager;
	private XmlFoldingStrategy foldingStrategy;

	public TextEditor()
	{
		InitializeComponent();

		SearchPanel.Install(Editor);
		foldingManager = FoldingManager.Install(Editor.TextArea);
		foldingStrategy = new XmlFoldingStrategy();
	}
	#endregion

	#region Properties
	public string Text
	{
		get => Editor.Text;
		set => Editor.Text = value;
	}
	#endregion

	#region Methods
	private void Editor_TextChanged(object sender, EventArgs e)
	{
		foldingStrategy.UpdateFoldings(foldingManager, Editor.Document);
	}

	public static void Register(string name)
	{
		var resource = new Uri($"pack://application:,,,/Preview.UI;component/Resources/Xshd/{name}.xshd");
		using var stream = Application.GetResourceStream(resource).Stream;
		using var reader = new XmlTextReader(stream);

		var definition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
		HighlightingManager.Instance.RegisterHighlighting("SQL", new string[] { ".sql" }, definition);
	}
	#endregion
}