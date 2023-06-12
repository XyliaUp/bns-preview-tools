using System.Collections.Generic;
using System.Linq;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Data.Models.BinData.Table.Attributes
{
	public sealed class DbData : IAttributeCollection
    {
        #region Constructor
        private readonly DatafileToXmlConverterHelper convert;

        public readonly BnsBinTool.Core.Models.Record record;

        private readonly Dictionary<string, AttributeDefinition> defName;

        private readonly Dictionary<AttributeDefinition, string> data;

        public DbData(DatafileToXmlConverterHelper convert, Dictionary<short, Dictionary<string, AttributeDefinition>> attrDef, BnsBinTool.Core.Models.Record record)
        {
            this.convert = convert;
            this.record = record;

            data = new();
            if (!attrDef.TryGetValue(record.SubclassType, out defName))
                defName = attrDef[-1];

            if (defName.TryGetValue("type", out var typeDef))
                data[typeDef] = typeDef.OriginalName;
        }
        #endregion


        #region Functions
        private void ProcessObject() => defName.Values.Where(o => !data.ContainsKey(o)).ForEach(attr => ProcessObject(attr));

        public string ProcessObject(AttributeDefinition attr) => data[attr] = convert.ConvertRecord(record, attr) ?? attr.DefaultValue;
        #endregion


        #region Attribute
        public string this[string param] => ContainsKey(param, out var Value) ? Value : null;

        public bool ContainsKey(string Name, out string Value)
        {
            // 如果存在定义
            if (defName.TryGetValue(Name, out var attrDef))
            {
                // 如果没有对应值, 说明没有Load Property
                if (!data.TryGetValue(attrDef, out Value))
                {
                    Value = ProcessObject(attrDef);
                }

                return true;
            }

            Value = null;
            return false;
        }

        public override string ToString() => GetEnumerator().ToIEnumerable().Aggregate("<record ", (sum, now) => sum + $"{now.Key}=\"{now.Value}\" ", result => result + "/>");

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            ProcessObject();
            foreach (var attr in data.Where(o => o.Value != o.Key.DefaultValue))
                yield return new(attr.Key.Name, attr.Value);

            yield break;
        }
        #endregion
    }
}