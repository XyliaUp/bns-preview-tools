﻿using System.Text;
using System.Text.RegularExpressions;

namespace Xylia.Preview.Tests.Extensions;
internal class StrWriter : TextWriter
{
    #region Fields
    public override Encoding Encoding => Encoding.UTF8;

    private readonly RichTextBox _output = null;
    private string LastVal = null;
    private int RepeatTime = 1;
    #endregion

    #region Constructor
    public StrWriter(RichTextBox output)
    {
        _output = output;

        Console.SetOut(this);
    }
    #endregion


    #region Fields
    public void OutInfo(string Text, bool NeedTimeInfo = true)
    {
        var FinalTxt = CreateInfo(Text, NeedTimeInfo);
        if (FinalTxt != null)
        {
            _output.AppendText(FinalTxt);
        }
    }

    public string CreateInfo(string Text, bool NeedTimeInfo = true)
    {
        string Result = NeedTimeInfo ? $"{DateTime.Now:T} " : null;

        if (!string.IsNullOrEmpty(Text))
        {
            if (!string.IsNullOrEmpty(Result)) Result += Text.Replace("\n", "\n" + new string(' ', Result.Length)) + "\r\n";
            else Result += Text + "\r\n";
        }

        return Result;
    }



    public override void WriteLine(string value)
    {
        #region Initialize
        string OriVal = value;

        bool NoTimeInfo = false;   //指示不显示时间信息
        bool Repeat = false;       //指示在重复时不进行合并
        #endregion

        #region extra
        if (!string.IsNullOrEmpty(value))
        {
            var regex = new Regex("#.*?#");

            var Matches = regex.Matches(value);
            if (Matches.Count == 0)
            {
                if (OriVal.Contains("error"))
                {
                    WriteLine("#cRed#" + value);
                    return;
                }

                else if (OriVal.Contains("crash"))
                {
                    WriteLine("#cCC0099#" + value);
                    return;
                }
            }
            else
            {
                foreach (var match in Matches.Cast<Match>())
                {
                    if (match.Success)
                    {
                        string extra = match.Value.Replace("#", null)?.Trim()?.ToLower();

                        if (extra is null) continue;
                        else if (extra.Equals("notime")) NoTimeInfo = true;
                        else if (extra.Equals("repeat")) Repeat = true;
                    }
                }
            }

            OriVal = value = regex.Replace(value, string.Empty);
        }
        #endregion

        #region output
        if (string.IsNullOrEmpty(value)) return;
        else if (!Repeat && LastVal == value)
        {
            _output.SelectionColor = ColorTranslator.FromHtml("#FF0066");

            value += $" ({++RepeatTime}次)";


            int LineID = Math.Max(_output.Text.Split('\n').Length - 2, 0);
            _output.SelectionStart = _output.GetFirstCharIndexFromLine(LineID);

            //用总长减掉起始数值,获得长度
            _output.SelectionLength = _output.Text.Length - _output.SelectionStart;
            _output.SelectedText = CreateInfo(value, !NoTimeInfo);
        }
        else
        {
            RepeatTime = 1;
            OutInfo(value, !NoTimeInfo);
        }
        #endregion

        // resume
        _output.SelectionColor = Color.Black;
        base.WriteLine(LastVal = OriVal);
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