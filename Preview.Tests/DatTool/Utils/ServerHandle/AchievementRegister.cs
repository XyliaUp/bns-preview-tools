using System.Diagnostics;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;
public class AchievementRegister : TableHandle
{
    public void Fix()
    {
        Dictionary<string, List<string>> RefInfo = new(StringComparer.OrdinalIgnoreCase);

        ReadTable("Achievement", (record) =>
        {
            string Alias = record.Attributes["alias"].Value;
            for (int x = 1; x <= 5; x++)
            {
                var RegisterRef = record.Attributes["register-ref-" + x];
                if (RegisterRef != null)
                {
                    if (!RefInfo.ContainsKey(RegisterRef.Value))
                        RefInfo.Add(RegisterRef.Value, new List<string>());

                    RefInfo[RegisterRef.Value].Add(Alias);
                }
            }
        });



        ReadTable("AchievementRegister", (record) =>
        {
            string Alias = record.Attributes["alias"].Value;
            for (int idx = 1; idx <= 8; idx++)
                record.RemoveAttribute("achievement-" + idx);

            if (RefInfo.ContainsKey(Alias))
            {
                int idx = 0;
                foreach (var CurRef in RefInfo[Alias])
                {
                    idx++;
                    if (idx > 8)
                    {
                        Trace.WriteLine(CurRef + " 超出8个");
                        break;
                    }

                    record.SetAttribute("achievement-" + idx, CurRef);
                }
            }
        }, true);
    }
}