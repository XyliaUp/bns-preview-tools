using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

using Xylia.Extension;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_Intension.Game_IntensionScene;

[DesignTimeVisible(false)]
public sealed class OptionList : Panel
{
    #region Constructor
    public OptionList()
    {
        AutoScroll = true;
    }


    [DllImport("user32.dll")]
    private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

    protected override void WndProc(ref Message m)
    {
        //禁用水平滚动条
        ShowScrollBar(Handle, 0, false);
        base.WndProc(ref m);
    }
    #endregion

    #region 内容处理
    [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [Category()]
    public readonly List<string> Options = new();

    public void Add(string Option) => Options.Add(Option);

    public void Clear() => Options.Clear();
    #endregion



    #region Functions
    public int SelectedIndex = 0;

    public event EventHandler SelectedItemChanged;

    public void RefreshList()
    {
        Controls.Clear();
        VerticalScroll.Value = 0;

        SuspendLayout();

        int LocY = 0;
        for (int idx = 0; idx < Options.Count; idx++)
        {
            var CurIdx = idx;
            var panel = new ContentPanel(Options[idx])
            {
                Location = new Point(0, LocY),
                ForeColor = ForeColor,

                _useMaxWidth = true,
            };
            panel.Click += new((s, e) =>
            {
                Controls.OfType<ContentPanel>().ForEach(o => o.BackColor = Color.Transparent);
                panel.BackColor = Color.SteelBlue;

                SelectedIndex = CurIdx;
                SelectedItemChanged?.Invoke(s, e);
            });

            Controls.Add(panel);
            LocY = panel.Bottom;
        }

        ResumeLayout(true);
    }
    #endregion
}