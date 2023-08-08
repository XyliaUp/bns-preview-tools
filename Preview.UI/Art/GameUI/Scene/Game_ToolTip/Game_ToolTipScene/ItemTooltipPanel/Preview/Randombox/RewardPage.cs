using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Tag.Link;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTipScene.ItemTooltipPanel.Preview.Randombox;
internal sealed class RewardPage
{
    #region Fields
    public JobSeq Job;

    public Reward DecomposeReward;

    public DecomposeByItem2 OpenItem2;
    #endregion

    #region Properties
    public bool HasOpenItem2 => !OpenItem2?.Item.INVALID() ?? false;

    public IAttributeCollection Attrs => DecomposeReward?.Attributes;
    #endregion



    #region Functions
    private static RewardPage LoadFrom(Reward Reward, DecomposeByItem2 OpenItem)
    {
        if (Reward is null) return null;

        var page = new RewardPage()
        {
            DecomposeReward = Reward,
            OpenItem2 = OpenItem,
        };


        // debug
        if (!page.Preview.Any())
            Debug.WriteLine($"empty reward");

        return page;
    }

    public static List<RewardPage> LoadFrom(DecomposeInfo DecomposeInfo)
    {
        var result = new List<RewardPage>();

        #region reward
        for (int index = 0; index < DecomposeInfo.DecomposeReward.Length; index++)
        {
            var reward = DecomposeInfo.DecomposeReward[index];
            var item2 = DecomposeInfo.Decompose_By_Item2[index];

            result.AddItem(LoadFrom(reward, item2));
        }
        #endregion


        #region job reward
        var group_job = DecomposeInfo.DecomposeJobRewards
            .Where(r => r.Value is not null)
            .Select(r => new RewardPage() { Job = r.Key, DecomposeReward = r.Value, });

        if (group_job.Any())
        {
            // if create combine-data group according to cell num
            int CellSum = group_job.Sum(group => group.Preview.Count);
            if (CellSum >= 30) result.AddRange(group_job);
            else result.Add(new RewardPage()
            {
                Job = JobSeq.JobNone,
                DecomposeReward = group_job.FirstOrDefault().DecomposeReward,
                OpenItem2 = DecomposeInfo.Job_Decompose_By_Item2.FirstOrDefault(),

                Preview = group_job.SelectMany(group => group.Preview).ToList(),
            });
        }
        #endregion

        return result;
    }
    #endregion


    #region Functions (UI)
    private List<ContentPanel> _preview;
    public List<ContentPanel> Preview
    {
        private set => _preview = value;
        get
        {
            if (_preview is null)
            {
                _preview = new();
                CreateFixed();
                CreateSelected();
                CreateRandom();
            }

            return _preview;
        }

    }


    private void Create(int Num, Func<int, string> func, Func<int, ContentPanel> func2)
    {
        var items = new Item[Num];
        Linq.For(ref items, (i) => FileCache.Data.Item[func(i)]);
        Array.Sort(items, new comparer());


        var font = new Font("Microsoft YaHei UI", 14F);
        for (int i = 1; i <= Num; i++)
        {
            var item = items[i - 1];
            if (item is null) continue;

            var c = func2(i);
            if (c is null) continue;

            c.Params[2] = item;
            c.Font = font;
            c.Text = ItemName.CreateLink(c.Text.GetText(), item.Ref);
            if (Job != JobSeq.JobNone) c.Text += $" ({Job.GetName()})";

            _preview.Add(c);
        }
    }


    private void CreateFixed()
    {
        Create(8, (num) => Attrs[$"fixed-item", num], (num) =>
        {
            var min = Attrs[$"fixed-item-min", num].ToInt16();
            var max = Attrs[$"fixed-item-max", num].ToInt16();

            string Text = "UI.ItemTooltip.RandomboxPreview.Fixed.MinMax";
            if (max == 1) Text = "UI.ItemTooltip.RandomboxPreview.Fixed";
            else if (min == max) Text = "UI.ItemTooltip.RandomboxPreview.Fixed.Min";

            var c = new ContentPanel(Text);
            c.Params[3] = min;
            c.Params[4] = max;

            return c;
        });
    }

    private void CreateRandom()
    {
        Create(90, (num) => Attrs[$"random-item", num], (num) => new ContentPanel("UI.ItemTooltip.RandomboxPreview.Random"));
    }

    private void CreateSelected()
    {
        Create(64, (num) => Attrs[$"selected-item", num], (num) =>
        {
            var c = new ContentPanel("UI.ItemTooltip.RandomboxPreview.Selected");
            c.Params[3] = Attrs[$"selected-item-count", num].ToInt16();

            return c;
        });
    }
    #endregion
}


public class comparer : IComparer<Item>
{
    public int Compare(Item x, Item y)
    {
        if (x is null) return 1;
        if (y is null) return -1;

        var GradeA = x.ItemGrade;
        var GradeB = y.ItemGrade;
        if (GradeA != GradeB) return GradeA - GradeB;

        return x.Ref.Id - y.Ref.Id;
    }
}