using System.Text;

using CUE4Parse.BNS.Conversion;

using Xylia.Extension;
using Xylia.Extension.Class;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Tag;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.UI.Interface;

using BNSTag = Xylia.Preview.Common.Tag;

namespace Xylia.Preview.UI.Custom.Controls;
public partial class ContentPanel
{
	#region Width
	/// <summary>
	/// 允许的最大宽度
	/// </summary>
	protected int MaxWidth = 0;


	float ExpectWidth;

	void TryExtendWidth(float Width) => this.ExpectWidth = Math.Max(this.ExpectWidth, Width);

	private int GetMaxWidth(Control o, int Padding = 0)
	{
		int MaxWidth = 0;


		//无需自动调整大小的情况
		if (!o.AutoSize)
		{
			if (MaxWidth == 0) MaxWidth = o.Width;
		}

		//获取已设置的限制宽度
		else if (o.MaximumSize.Width != 0) MaxWidth = o.MaximumSize.Width;

		//计算与母容器关系
		else if (o.Parent != null)
		{
			var e = o.Parent;
			if (e is IPreview) return GetMaxWidth(e, o.Left);

			var autoSize = false;
			var autoSizeMode = AutoSizeMode.GrowOnly;
			if (e is UserControl userControl)
			{
				autoSize = userControl.AutoSize;
				autoSizeMode = userControl.AutoSizeMode;
			}
			else if (e is Form form)
			{
				autoSize = form.AutoSize;
				autoSizeMode = form.AutoSizeMode;
			}

			//如果上级控件启用缩放, 会导致大量计算。因此此时不应进行宽度处理
			if (!autoSize || autoSizeMode != AutoSizeMode.GrowAndShrink)
			{
				if (autoSize && e.MaximumSize.Width != 0) MaxWidth = e.MaximumSize.Width;
				else MaxWidth = e.Width;

				//扣减其他大小
				Padding += o.Left + 5;

				//滚动条 20
				if (e is Form frm && frm.FormBorderStyle != FormBorderStyle.None)
					Padding += 10 + (frm.AutoScroll ? 25 : 0);
			}
		}

		return Math.Max(0, MaxWidth - Padding);
	}
	#endregion



	List<ExecuteUnit> _units;

	List<ITag> Tags;

	protected override void OnPaint(PaintEventArgs e)
	{
		this.MaxWidth = GetMaxWidth(this);
		this.ExpectWidth = _useMaxWidth ? this.MaxWidth : 0;
		float LocX = 0, LocY = 0;


		Tags = new();
		_units = new();
		this.Execute(new ExecuteParam(this).GetFont(FontName), base.Text?.Replace("\n", "<br/>"), ref LocX, ref LocY, true);

		if (this.AutoSize)
		{
			this.Width = (int)Math.Ceiling(ExpectWidth + 4.0f);
			this.Height = (int)Math.Floor(LocY);
		}

		var temp = new List<ExecuteUnit>(_units);
		foreach (var o in temp)
		{
			var p = o.param;
			var point = o.point;
			if (p.HorizontalAlignment == HorizontalAlignment.Center)
				point = new PointF((this.Width - o.Width) / 2, o.point.Y);


			if (o.text != null) e.Graphics.DrawString(o.text, p.Font, new SolidBrush(p.ForeColor), point);
			if (o.bitmap != null) e.Graphics.DrawImage(o.bitmap, new RectangleF(point, o.bitmap.Size));
		}
	}

	private void Execute(ExecuteParam param, string InfoHtml, ref float LocX, ref float LocY, bool Status = false)
	{
		int BasicHeight = param.Font.Height;
		float FinalHeight = BasicHeight;

		#region Initialize
		if (InfoHtml is null) return;

		var doc = new HtmlAgilityPack.HtmlDocument();
		doc.LoadHtml(InfoHtml);


		// Remove invalid line
		var idx = new List<int>();
		for (int i = 0; i < doc.DocumentNode.ChildNodes.Count; i++)
		{
			if (!doc.DocumentNode.ChildNodes[i].Name.Equals("br", StringComparison.OrdinalIgnoreCase)) break;
			idx.Add(i);
		}

		idx.Reverse();
		idx.ForEach(doc.DocumentNode.ChildNodes.RemoveAt);

		for (int i = doc.DocumentNode.ChildNodes.Count - 1; i >= 0; i--)
		{
			if (!doc.DocumentNode.ChildNodes[i].Name.Equals("br", StringComparison.OrdinalIgnoreCase)) break;
			doc.DocumentNode.ChildNodes.RemoveAt(i);
		}
		#endregion

		#region Tag Data
		foreach (var Node in doc.DocumentNode.ChildNodes)
		{
			string attr(string name) => Node.Attributes[name]?.Value;

			switch (Node.Name)
			{
				case "link":
				{
					var link = new LinkTag(Node);
					Tags.Add(link);


					link.Start = new PointF(LocX, LocY);
					this.Execute(param, Node.InnerHtml, ref LocX, ref LocY);
					link.Finish = new PointF(LocX, FinalHeight);
				}
				break;




				case "ga": break;

				case "#text": this.DrawString(param, ref LocX, ref LocY, Node.InnerText.Decode()); break;

				case "br":
				{
					TryExtendWidth(LocX);

					//new line
					LocX = 0;
					LocY += FinalHeight + this.Padding.Bottom;
					FinalHeight = param.Font.Height;
				}
				break;

				case "p":
				{
					LocX += attr("leftmargin").ToFloat32() / 2;
					LocY += attr("topmargin").ToFloat32() / 2;

					FinalHeight += attr("bottommargin").ToFloat32() / 2;

					var Justification = attr("justification").ToBool();
					var JustificationType = attr("justificationtype").ToEnum<JustificationType>();

					param.HorizontalAlignment = attr("horizontalalignment").ToEnum<HorizontalAlignment>();

					//Bullet																			                                                             
					var Bullets = attr("bullets");
					if (Bullets != null)
					{
						var BulletsFontset = attr("bulletsfontset");

						this.DrawString(param.GetFont(BulletsFontset), ref LocX, ref LocY, Bullets);
						LocX += 2;
					}

					this.Execute(param, Node.InnerHtml, ref LocX, ref LocY);


					//new line
					//var RightMargin = attr("rightmargin");
					LocX = 0;
					LocY += FinalHeight + this.Padding.Bottom;
					FinalHeight = param.Font.Height;
				}
				break;

				case "arg":
				{
					try
					{
						var result = this.Params.Handle(Node);
						if (result is ImageData b) FinalHeight = Math.Max(FinalHeight, DrawImage(param, ref LocX, ref LocY, b.source, BasicHeight, true /* , b.scale*/));
						else if (result is int @int) this.Execute(param, @int.ToString("N0"), ref LocX, ref LocY);
						else if (result is not null) this.Execute(param, result.ToString(), ref LocX, ref LocY);
						// else Debug.WriteLine($"arg result is null: {Node.OuterHtml}");
					}
					catch (Exception ex)
					{
						Debug.WriteLine($"handle arg failed: {Node.OuterHtml}\n\t{ex.Message}");
						this.Execute(param, "???", ref LocX, ref LocY);
					}
				}
				break;

				case "font":
				{
					var param2 = param.GetFont(attr("name"));

					FinalHeight = param2.Font.Height;
					this.Execute(param2, Node.InnerHtml, ref LocX, ref LocY);
				}
				break;

				case "image":
				{
					Bitmap bitmap = null;
					bitmap = this.GetInfo(Node.Attributes["object"]?.Value, true)?.GetValue(this) as Bitmap;

					if (DesignMode) break;


					var ImagesetPath = Node.Attributes["imagesetpath"]?.Value;
					var Path = Node.Attributes["path"]?.Value;
					var EnableScale = Node.Attributes["enablescale"]?.Value.ToBool() ?? false;
					var ScaleRate = Node.Attributes["scalerate"]?.Value.ToFloat32() ?? 1.0f;


					#region	Image
					if (ImagesetPath != null) bitmap = FileCache.Provider.LoadObject(ImagesetPath)?.GetImage();
					if (Path != null)
					{
						bitmap = FileCache.Provider.LoadObject(Path).GetImage();

						var u = attr("u").ToInt32();
						var v = attr("v").ToInt32();
						var ul = attr("ul").ToInt32();
						var vl = attr("vl").ToInt32();
						var width = attr("width").ToInt32();
						var height = attr("height").ToInt32();

						bitmap = bitmap.Clone(u, v, ul, vl);
					}

					if (bitmap is null) break;
					FinalHeight = Math.Max(FinalHeight, DrawImage(param, ref LocX, ref LocY, bitmap, BasicHeight, EnableScale, ScaleRate));
					#endregion
				}
				break;

				case "timer":
				{
					var tag = new BNSTag.Timer(Node);
					if (Timers is null || !Timers.TryGetValue(tag.id, out var timer))
					{
						Debug.WriteLine("invalid timer: " + tag.id);
						break;
					}

					this.DrawString(param, ref LocX, ref LocY, timer.ToString());
				}
				break;

				default:
				{
					Debug.WriteLine("unknown tag: " + Node.Name);
					this.DrawString(param, ref LocX, ref LocY, Node.OuterHtml);
				}
				break;
			}
		}
		#endregion


		//如果由主入口函数调用, 增加最后一行对应的行高
		if (Status) LocY += FinalHeight + this.Padding.Bottom;

		//处理普通标签的最终宽度
		TryExtendWidth(LocX);
	}



	private int DrawImage(ExecuteParam param, ref float LocX, ref float LocY,
		Bitmap bitmap, float LineHeight, bool EnableScale = true, float ScaleRate = 1.0F)
	{
		if (bitmap is null) return 0;

		//float ScaleRatio = EnableScale ? (LineHeight / bitmap.Height) : 1;
		//var ratio = ScaleRate * ScaleRatio;
		var ratio = (LineHeight / bitmap.Height);


		var width = (int)(bitmap.Width * ratio);
		var height = (int)(bitmap.Height * ratio);

		_units.Add(new ExecuteUnit(param, new PointF(LocX, LocY), new Bitmap(bitmap, width, height)));

		LocX += width;
		return height;
	}

	private void DrawString(ExecuteParam param, ref float LocX, ref float LocY, string Text)
	{
		if (string.IsNullOrWhiteSpace(Text)) return;

		var lines = SplitMultiLine(param, Text, this.MaxWidth, ref LocX, ref LocY);
		_units.AddRange(lines);

		if (lines.Count > 1) TryExtendWidth(this.MaxWidth);
	}

	public static List<ExecuteUnit> SplitMultiLine(ExecuteParam param, string Txt, int MaxWidth, ref float LocX, ref float LocY)
	{
		var LineTxts = new List<ExecuteUnit>();

		//起始坐标, 用于处理绘制位置
		float StartLocX = LocX;
		float StartLocY = LocY;

		#region 判断文本总长度是否大于最大宽度
		if (MaxWidth != 0 && (LocX + Txt.MeasureString(param.Font).Width > MaxWidth))
		{
			//当前行的总和宽度（初始需要计算当前的LoX数值）
			var CurTxt = new StringBuilder();

			//逐字符计算信息
			for (int i = 0; i < Txt.Length; i++)
			{
				//计算当前字符宽度
				var CharSize = Txt[i].ToString().MeasureString(param.Font);
				if (LocX + CharSize.Width > MaxWidth)
				{
					//截断之前的文本
					LineTxts.Add(new ExecuteUnit(param, new PointF(StartLocX, StartLocY), CurTxt.ToString()));

					//Initialize文本生成器
					CurTxt.Clear();

					//获取换行之后的新的位置信息
					LocX = StartLocX = 0;
					LocY += CharSize.Height;
					StartLocY = LocY;
				}

				//累积信息
				LocX += CharSize.Width;
				CurTxt.Append(Txt[i]);
			}

			//判断是否有剩余的文本
			if (CurTxt.Length != 0 && !string.IsNullOrWhiteSpace(CurTxt.ToString()))
			{
				LineTxts.Add(new ExecuteUnit(param, new PointF(StartLocX, StartLocY), CurTxt.ToString()));
			}

			return LineTxts;
		}
		#endregion

		var size = Txt.MeasureString(param.Font);
		LineTxts.Add(new ExecuteUnit(param, new PointF(StartLocX, StartLocY), Txt));

		//获取下一个绘制起始点
		LocX += size.Width;

		return LineTxts;
	}
}