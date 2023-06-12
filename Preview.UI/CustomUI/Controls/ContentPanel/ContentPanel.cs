using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

using Xylia.Preview.GameUI.Controls.Designer;

using BNSTag = Xylia.Preview.Common.Tag;


namespace Xylia.Preview.GameUI.Controls
{
	[OnlyAutoSize(true)]
	[Designer(typeof(FixedDesigner))]
	public partial class ContentPanel : Panel
	{
		#region Constructor
		public ContentPanel(string Text) : this() => base.Text = Text;

		public ContentPanel()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;

			this.BackColor = Color.Transparent;
			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			this.ResizeRedraw = false;
		}
		#endregion


		#region Override Fields
		[DefaultValue(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new Size Size { get => base.Size; set => base.Size = value; }
		#endregion

		#region Fields
		/// <summary>
		/// 参数组
		/// </summary>
		public ContentParams Params = new();

		public Dictionary<uint, BNSTag.Timer> Timers = new();


		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
		public override string Text
		{
			get => base.Text;
			set
			{
				base.Text = value;
				/*if (Loaded)*/

				//TODO: 界面显示以后后再触发刷新
				this.Refresh();
			}
		}

		public string FontName { get; set; }


		/// <summary>
		/// Signal 图标
		/// </summary>
		public Bitmap Icon;


		/// <summary>
		/// 高度填充
		/// </summary>											                                                    
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public float HeightPadding = 0;


		/// <summary>																							                                                                                                      
		/// 使用字体高度
		/// </summary>
		public bool _useHeight = true;

		public bool _useMaxWidth = false;
		#endregion


		#region Functions (UI)
		/// <summary>
		/// 通知界面重绘
		/// </summary>
		public override void Refresh()
		{
			base.Refresh();

			if (string.IsNullOrWhiteSpace(Text)) return;
			else this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
		}

		/// <summary>
		/// 双击控件复制内容
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PanelContent_DoubleClick(object sender, EventArgs e)
		{
			var CopyTxt = this.Text/*.CutText()*/;
			if (!string.IsNullOrWhiteSpace(CopyTxt))
				this.Invoke(() => Clipboard.SetText(CopyTxt));
		}
		#endregion
	}
}