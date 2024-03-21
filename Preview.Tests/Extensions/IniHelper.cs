using IniParser;
using Xylia.Preview.Properties;

namespace Xylia.Preview.Tests.Extensions;
internal class IniHelper : Settings
{
    internal static IniHelper Instance { get; } = new IniHelper();


    public string ReadValue(string section, string name) => Configuration[section][name];

    public void WriteValue(string section, string name, object value)
    {
        Configuration[section][name] = value?.ToString();
        new FileIniDataParser().WriteFile(ConfigPath, Configuration);
    }
}