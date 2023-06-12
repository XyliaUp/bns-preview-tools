using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using CUE4Parse.BNS;

using Xylia.Extension;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Interface;
using Xylia.Preview.Common.Tag;
using Xylia.Preview.Data.Record;

using BNSTag = Xylia.Preview.Common.Tag;


namespace Xylia.Preview.GameUI.Controls
{
	public partial class ContentPanel
	{
		#region Width
		/// <summary>
		/// 允许的最大宽度
		/// </summary>
		int MaxWidth = 0;


		float ExpectWidth;

		void TryExtendWidth(float Width) => this.ExpectWidth = Math.Max(this.ExpectWidth, Width);

		/// <summary>
		/// 计算最大宽度
		/// </summary>
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

		private void PanelContent_Paint(object sender, PaintEventArgs e)
		{
			this.MaxWidth = GetMaxWidth(this);
			this.ExpectWidth = _useMaxWidth ? this.MaxWidth : 0;
			float LocX = 0, LocY = 0;


			_units = new();
			this.Execute(new ExecuteParam(this).GetFont(FontName, _useHeight), this.Text?.Replace("\n", "<br/>"), ref LocX, ref LocY, true);

			//变更大小
			if (this.AutoSize)
			{
				this.Width = (int)Math.Ceiling(ExpectWidth + 4.0f);
				//this.Width = this.MaxWidth;
				this.Height = (int)Math.Floor(LocY);
			}

			var temp = new List<ExecuteUnit>(_units);
			foreach (var o in temp)
			{
				var p = o.param;

				PointF point = o.point;
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

			if (Status && this.Icon != null)
			{
				DrawImage(param, this.Icon,                                                                                                                                                                   BasicHeight, ref LocX, ref LocY);
				LocX += 5;
			}

			#region Initialize
			var doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(InfoHtml);

			// Remove invalid line
			var idx = new List<int>();
			for (int i = 0; i < doc.DocumentNode.ChildNodes.Count; i++)
			{
				if (doc.DocumentNode.ChildNodes[i].Name.MyEquals("br")) idx.Add(i);
				else break;
			}

			idx.Reverse();
			idx.ForEach(i => doc.DocumentNode.ChildNodes.RemoveAt(i));

			for (int i = doc.DocumentNode.ChildNodes.Count - 1; i >= 0; i--)
			{
				if (doc.DocumentNode.ChildNodes[i].Name.MyEquals("br")) doc.DocumentNode.ChildNodes.RemoveAt(i);
				else break;
			}
			#endregion

			#region Tag Data
			foreach (var Node in doc.DocumentNode.ChildNodes)
			{
				string attr(string name) => Node.Attributes[name]?.Value;


				switch (Node.Name)
				{
					case "ga": break;

					case "#text": this.DrawString(param, ref LocX, ref LocY, Node.InnerText.Decode()); break;

					case "br":
					{
						TryExtendWidth(LocX);

						//new line
						LocX = 0;
						LocY += FinalHeight + this.HeightPadding;
						FinalHeight = param.Font.Height;
					}
					break;

					case "p":
					{
						LocX += attr("leftmargin").ToFloat() / 2;
						LocY += HeightPadding = attr("topmargin").ToFloat() / 2;

						FinalHeight += attr("bottommargin").ToFloat() / 2;

						var Justification = attr("justification").ToBool();
						var JustificationType = attr("justificationtype").ToEnum<JustificationType>();

						param.HorizontalAlignment = attr("horizontalalignment").ToEnum<HorizontalAlignment>();

						//Bullet																			                                                             
						var Bullets = attr("bullets");
						if (Bullets != null)
						{
							var BulletsFontset = attr("bulletsfontset");

							this.DrawString(param.GetFont(BulletsFontset, _useHeight), ref LocX, ref LocY, Bullets);
							LocX += 2;
						}

						this.Execute(param, Node.InnerHtml, ref LocX, ref LocY);


						//new line
						//var RightMargin = attr("rightmargin");
						LocX = 0;
						LocY += FinalHeight + this.HeightPadding;
						FinalHeight = param.Font.Height;
					}
					break;

					case "arg":
					{
						try
						{
							var result = this.Params.Handle(Node);
							if (result is Bitmap b) FinalHeight = Math.Max(FinalHeight, DrawImage(param, b, BasicHeight, ref LocX, ref LocY));
							else if (result is int @int) this.Execute(param, @int.ToString("N0"), ref LocX, ref LocY);     //数值类型追加千位分隔符
							else if (result is not null) this.Execute(param, result.ToString(), ref LocX, ref LocY);
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
						//Get FontSet
						var param2 = param.GetFont(attr("name"), _useHeight);

						FinalHeight = param2.Font.Height;
						this.Execute(param2, Node.InnerHtml, ref LocX, ref LocY);
					}
					break;

					case "image":
					{
						if (DesignMode) break;

						#region	Get Image
						Bitmap bitmap = null;
						var ImagesetPath = Node.Attributes["imagesetpath"]?.Value;
						if (ImagesetPath != null) bitmap = ImagesetPath.GetUObject().GetImage();

						var Path = Node.Attributes["path"]?.Value;
						if (Path != null)
						{
							bitmap = Path.GetUObject().GetImage();

							var u = attr("u").ToInt();
							var v = attr("v").ToInt();
							var ul = attr("ul").ToInt();
							var vl = attr("vl").ToInt();
							var width = attr("width").ToInt();
							var height = attr("height").ToInt();

							bitmap = bitmap.GetSubImage(u, v, ul, vl);
						}

						if (bitmap is null) break;
						#endregion

						#region Get Scale
						bool EnableScale = attr("enablescale").ToBool();
						if (!EnableScale || !float.TryParse(attr("scalerate"), out float ScaleRate)) ScaleRate = 1.0F;
						#endregion


						FinalHeight = Math.Max(FinalHeight, DrawImage(param, bitmap, BasicHeight, ref LocX, ref LocY
							, EnableScale, ScaleRate));
					}
					break;

					case "link":
					{
						var IgnoreInput = attr("ignoreinput").ToBool();

						var id = attr("id");
						if (id == "none") break;

						//achievement:291_event_SoulEvent_Extreme_0004_step1:123.3.0.1.1.1.626f57f5.1.0.0.0.1
						//item-name:3d0a04.1.270F
						//skill:SRK_B1_DollQueen_AirBomb

						var obj = id.CastObject();
						if (obj is Text @text) this.SetToolTip(text.Attributes["text"]);
						else obj.PreviewShow();
					}
					break;

					case "timer":
					{
						var id = (uint)attr("id").ToInt();
						if (Timers is null || !Timers.TryGetValue(id, out var timer))
						{
							Debug.WriteLine("invalid timer: " + id);
							break;
						}

						var type = attr("type").ToEnum<BNSTag.Timer.TimerType>();
						this.DrawString(param, ref LocX, ref LocY, timer.ToString(attr("type")));
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
			if (Status) LocY += FinalHeight + this.HeightPadding;

			//处理普通标签的最终宽度
			TryExtendWidth(LocX);
		}

		private int DrawImage(ExecuteParam param, Bitmap bitmap, int CurLineHeight, ref float LocX, ref float LocY, bool EnableScale = true, float ScaleRate = 1.0F)
		{
			if (bitmap is null) return 0;

			//自适应行高度
			float ScaleRatio = EnableScale ? ((float)CurLineHeight / bitmap.Height) : 1;

			//计算图片比率
			var ratio = ScaleRate * ScaleRatio;
			var CurWidth = (int)(bitmap.Width * ratio);
			var CurHeight = (int)(bitmap.Height * ratio);

			//绘制图像
			var point = new PointF((int)Math.Ceiling(LocX), (int)Math.Ceiling(LocY - 2));
			_units.Add(new ExecuteUnit(param, point, bitmap.Thumbnail(ratio)));

			//计算新的位置
			LocX += CurWidth;
			return CurHeight;
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
}