using System.Text.RegularExpressions;

namespace Xylia.Preview.Common.Extension;
public class SortByString : IComparer<string>
{
    #region Constructor
    public SortByString() { }

    public SortByString(bool IgnoreCase) => this.IgnoreCase = IgnoreCase;
    #endregion


    public bool Common { get; set; } = true;

    public bool IgnoreCase { get; set; } = false;


    public int Compare(string x, string y)
    {
        Method.SortRule Flag = 0;
        if (Common) Flag |= Method.SortRule.Common;
        if (IgnoreCase) Flag |= Method.SortRule.IgnoreCase;

        return Method.StrCompare(x, y, Flag);
    }
}

public static class Method
{
    [Flags]
    public enum SortRule
    {
        /// <summary>
        /// 普通规则
        /// </summary>
        Common = 1,

        /// <summary>
        /// 逐一对比字符
        /// </summary>
        EveryChar = 2,

        /// <summary>
        /// 忽略大小写
        /// </summary>
        IgnoreCase = 4,
    }

    public static int StrCompare(object TextA, object TextB, SortRule SortRule = SortRule.Common)
    {
        #region 初始化字符集
        string StrA = TextA.ToString(), StrB = TextB.ToString();
        if ((SortRule & SortRule.IgnoreCase) == SortRule.IgnoreCase)
        {
            StrA = StrA.ToLower();
            StrB = StrB.ToLower();
        }

        var TxtArray1 = StrA.ToCharArray();
        var TxtArray2 = StrB.ToCharArray();
        #endregion


        #region 比对方法
        bool CompareDigit = SortRule.HasFlag(SortRule.Common);
        if (CompareDigit)
        {
            //对于形如 12_34 的文本，不使用数值比较 
            Regex regex = new(@"[0-9]\d*_[0-9]\d*");
            if (regex.IsMatch(StrA)) CompareDigit = false;
            if (regex.IsMatch(StrB)) CompareDigit = false;
        }


        int i = 0, j = 0;
        while (i < TxtArray1.Length && j < TxtArray2.Length)
        {
            char A = TxtArray1[i], B = TxtArray2[j];

            //如果2个字符都是数字且允许被按照数字对比大小
            if (CompareDigit && char.IsDigit(A) && char.IsDigit(B))
            {
                string s1 = "", s2 = "";

                //读取到结尾或不为数字的字符位置
                while (i < TxtArray1.Length && char.IsDigit(TxtArray1[i]))
                {
                    s1 += TxtArray1[i];
                    i++;
                }

                //读取到结尾或不为数字的字符位置
                while (j < TxtArray2.Length && char.IsDigit(TxtArray2[j]))
                {
                    s2 += TxtArray2[j];
                    j++;
                }

                //对比大小
                if (int.Parse(s1) > int.Parse(s2)) return 1;
                if (int.Parse(s1) < int.Parse(s2)) return -1;
            }

            //按照字符大小排序
            else
            {
                //逐字符比较
                if (TxtArray1[i] > TxtArray2[j]) return 1;

                else if (TxtArray1[i] < TxtArray2[j]) return -1;

                else
                {
                    i++;
                    j++;
                }
            }
        }

        return TxtArray1.Length - TxtArray2.Length;
        #endregion
    }
}