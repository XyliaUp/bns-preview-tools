namespace Xylia.Preview.Data.Models.Util.Sort.Common;

public static class NameEx
{
    public static string NameConvert(this string Str, bool IsConfig)
    {
        if (!IsConfig) return Str;


        string StrL = Str.ToLower();
        if (StrL == "type") StrL = StrL.Replace("type", "#1");
        if (StrL == "start") StrL = StrL.Replace("start", "#0");

        return StrL.NameConvert();
    }

    public static string NameConvert(this string Str)
    {
        string StrL = Str.ToLower();

        //特殊处理：将这几个属性放置到最前 (ASCII顺序)
        if (StrL == "alias") StrL = StrL.Replace("alias", "#3");
        if (StrL == "start") StrL = StrL.Replace("start", "*");


        //第一次转换
        StrL = StrL.Replace("start", "0").Replace("end", "1").Replace("unk", "$")
                   .Replace("brand-id", "#2").Replace("name", "#5").Replace("repairable", "#4").Replace("step", "/")
                   .Replace("mesh-id", "#6").Replace("mesh-id-2", "#7").Replace("mesh-col-1", "#8").Replace("mesh-col-1", "#9")
                   .Replace("job", "@").Replace("mastery-level", "@").Replace("level", "@").Replace("race", "@").Replace("sex", "@")
                   .Replace("idle", "icdle").Replace("id", "#2");


        //如果以Test开头
        if (StrL.StartsWith("test"))
        {
            StrL = Str.Replace("test", "!");
        }


        //如果包含money(因为mon会被转为1)，直接返回
        if (StrL.Contains("money")) return Str;

        //星期转换（实现从周一到周日顺序排序）
        StrL = StrL.Replace("mon", "mon1").Replace("tue", "mon2").Replace("wed", "mon3").Replace("thu", "mon4")
                  .Replace("fri", "mon5").Replace("sat", "mon6").Replace("sun", "mon7");


        return StrL;
    }
}