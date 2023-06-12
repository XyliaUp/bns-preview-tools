using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.Util.Writer
{
    public class StrWriter : TextWriter
    {
        #region Fields
        public override Encoding Encoding => Encoding.UTF8;

        readonly RichTextBox _output = null;
        public string LastVal = null;
        public int RepeatTime = 1;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public StrWriter(RichTextBox output)
        {
            _output = output;

            Console.SetOut(this);
            Console.WriteLine($"#cFF6600#Initialize成功！版本信息：{Assembly.GetExecutingAssembly().GetName().Version} (内部版本 NT2.0)");
        }
        #endregion


        #region Fields
        public void OutInfo(string Text, bool NeedTimeInfo = true)
        {
            var FinalTxt = CreateInfo(Text, NeedTimeInfo);
            if (FinalTxt != null)
            {
                _output.AppendText(FinalTxt);

                //剔除换行符后执行日志
                FinalTxt.Replace("\n", null).CreateLog();
            }
        }

        public string CreateInfo(string Text, bool NeedTimeInfo = true)
        {
            //设置当前时间
            string Result = NeedTimeInfo ? $"{DateTime.Now:T} " : null;

            //文本处理
            if (!string.IsNullOrEmpty(Text))
            {
                if (!string.IsNullOrEmpty(Result)) Result += Text.Replace("\n", "\n" + new string(' ', Result.Length)) + "\r\n";
                else Result += Text + "\r\n";
            }

            return Result;
        }

        public override void WriteLine(string Val)
        {
            #region Initialize
            string OriVal = Val;

            bool NoTimeInfo = false;   //指示不显示时间信息
            bool Repeat = false;       //指示在重复时不进行合并
            #endregion

            #region 处理扩展选项
            //对空文本正则会报错，必须先判断
            if (!string.IsNullOrEmpty(Val))
            {
                #region 处理额外标记信息
                //定义正则表达式
                var regex = new Regex("#.*?#");

                var Matches = regex.Matches(Val);
                if (Matches.Count == 0)
                {
                    //如果包含错误字样，显示为红字
                    if (OriVal.Contains("错误") || OriVal.ToLower().Contains("error") || OriVal.Contains("失败"))
                    {
                        WriteLine("#cRed#" + Val);
                        return;
                    }

                    else if (OriVal.ToLower().Contains("crash") || OriVal.Contains("崩溃"))
                    {
                        WriteLine("#cCC0099#" + Val);
                        return;
                    }
                }
                else
                {
                    foreach (Match match in Matches)
                    {
                        if (match.Success)
                        {
                            //获得额外信息
                            string Extra = match.Value.Replace("#", null)?.Trim();

                            try
                            {
                                //c开头代表颜色处理
                                if (Extra.MyStartsWith("c"))
                                {
                                    try
                                    {
                                        _output.SelectionColor = ColorTranslator.FromHtml(Extra.Remove(0, 1));
                                    }
                                    catch
                                    {
                                        _output.SelectionColor = ColorTranslator.FromHtml("#" + Extra.Remove(0, 1));
                                    }
                                }

                                //不显示时间信息
                                else if (Extra.MyEquals("notime")) NoTimeInfo = true;

                                //debug模式
                                else if (Extra.MyEquals("debug")) LogWriter.WriteLine(OriVal);

                                else if (Extra.MyEquals("repeat")) Repeat = true;
                            }
                            catch
                            {
                                LogWriter.WriteLine("设置输出参数失败...");
                            }
                        }
                    }
                }
                #endregion

                //清除额外标记信息
                OriVal = Val = regex.Replace(Val, string.Empty);
            }
            #endregion

            #region 输出内容
            //空文本异常处理
            if (string.IsNullOrEmpty(Val)) return;

            //重复文本进行合并显示
            else if (!Repeat && LastVal == Val)
            {
                _output.SelectionColor = ColorTranslator.FromHtml("#FF0066");

                Val += $" ({++RepeatTime}次)";


                int LineID = Math.Max(_output.Text.Split('\n').Length - 2, 0);
                _output.SelectionStart = _output.GetFirstCharIndexFromLine(LineID);

                //用总长减掉起始数值,获得长度
                _output.SelectionLength = _output.Text.Length - _output.SelectionStart;

                _output.SelectedText = CreateInfo(Val, !NoTimeInfo);
            }
            else
            {
                RepeatTime = 1;
                OutInfo(Val, !NoTimeInfo);
            }
            #endregion

            #region 最后处理
            //恢复原来颜色
            _output.SelectionColor = Color.Black;

            base.WriteLine(LastVal = OriVal);
            #endregion
        }


        public override void WriteLine(bool value) => WriteLine(value.ToString());

        public override void WriteLine(int value) => WriteLine(value.ToString());

        public override void WriteLine(uint value) => WriteLine(value.ToString());

        public override void WriteLine(long value) => WriteLine(value.ToString());

        public override void WriteLine(ulong value) => WriteLine(value.ToString());

        public override void WriteLine(float value) => WriteLine(value.ToString());

        public override void WriteLine(double value) => WriteLine(value.ToString());

        public override void WriteLine(decimal value) => WriteLine(value.ToString());
        #endregion
    }
}