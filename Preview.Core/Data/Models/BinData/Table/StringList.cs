using System.Text;

using BnsBinTool.Core.Models;

namespace Xylia.Preview.Data.Models.BinData.Table;
public sealed class StringList : List<string>
{
    #region Constructor
    private readonly HashSet<string> ht = new(StringComparer.OrdinalIgnoreCase);

    public StringList(StringLookup stringLookup)
    {
        int start = 0, end = 0;
        var size = stringLookup.Data.Length;
        while (end < size)
        {
            if (stringLookup.Data[end] == 0 && stringLookup.Data[end + 1] == 0)
            {
                byte[] tmp = new byte[end - start];
                Array.Copy(stringLookup.Data, start, tmp, 0, end - start);

                string w = Encoding.Unicode.GetString(tmp);
                Add(w);

                start = end + 2;
            }

            end += 2;
        }
    }
    #endregion

    #region Functions
    public new void Add(string item)
    {
        if (!ht.Contains(item))
            ht.Add(item);

        base.Add(item);
    }

    public new bool Remove(string item)
    {
        ht.Remove(item);
        return base.Remove(item);
    }

    public new void Clear()
    {
        base.Clear();
        ht.Clear();
    }

    public new bool Contains(string String) => ht.Contains(String);
    #endregion
}