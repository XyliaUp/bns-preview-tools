using System;
using System.Diagnostics;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Data.Table.XmlRecord
{
	public sealed class RefConvert : ConvertClass
    {
        public override object Construct(Type type, params object[] param)
        {
            if (type.HasImplementedRawGeneric(typeof(BaseRecord)))
            {
                var record = (BaseRecord)Activator.CreateInstance(type);
                if (!record.ContainAttribute<AliasRecord>()) Debug.WriteLine($"warning: target is not AliasRecord ({type.Name})");

                record.alias = (string)param[0];
                return record;
            }

            return base.Construct(type, param);
        }
    }
}