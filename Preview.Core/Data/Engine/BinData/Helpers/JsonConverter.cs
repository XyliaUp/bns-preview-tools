using System.Text;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;
public class TableConverter : JsonConverter<Table>
{
	public override void WriteJson(JsonWriter writer, Table value, JsonSerializer serializer)
	{
		writer.WriteStartObject();

		writer.WritePropertyName("Definition");
		serializer.Serialize(writer, value.Definition);

		writer.WritePropertyName("IsCompressed");
		serializer.Serialize(writer, value.IsCompressed);

		if (!value.IsCompressed)
		{
			var strings = value.Records.FirstOrDefault()?.StringLookup;
			if (strings != null) serializer.Serialize(writer, strings);
		}

		writer.WritePropertyName("Records");
		serializer.Serialize(writer, value.Records);

		writer.WriteEndObject();
	}

	public override Table ReadJson(JsonReader reader, Type objectType, Table existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		throw new NotImplementedException();
	}
}

public class RecordConverter : JsonConverter<Record>
{
	public override void WriteJson(JsonWriter writer, Record value, JsonSerializer serializer)
	{
		writer.WriteStartObject();

		writer.WritePropertyName("id");
		serializer.Serialize(writer, value.PrimaryKey.Id);

		writer.WritePropertyName("variation");
		serializer.Serialize(writer, value.PrimaryKey.Variant);

		if (value.SubclassType != -1)
		{
			writer.WritePropertyName("SubclassType");
			serializer.Serialize(writer, value.SubclassType);
		}

		writer.WritePropertyName("size");
		serializer.Serialize(writer, value.DataSize);

		writer.WritePropertyName("data");
		serializer.Serialize(writer, value.Data.ToHex(false));

		if (!value.StringLookup.IsPerTable)
			serializer.Serialize(writer, value.StringLookup);

		writer.WriteEndObject();
	}

	public override Record ReadJson(JsonReader reader, Type objectType, Record existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		throw new NotImplementedException();
	}
}

public class StringLookupConverter : JsonConverter<StringLookup>
{
	public override void WriteJson(JsonWriter writer, StringLookup value, JsonSerializer serializer)
	{
		long length = 0;
		var strings = Encoding.Unicode.GetString(value.Data).Split('\0', StringSplitOptions.RemoveEmptyEntries);

		writer.WritePropertyName("String");
		writer.WriteStartArray();

		for (int i = 0; i < strings.Length; i++)
		{
			var w = strings[i];
			if (w.Length > 0)
			{
				writer.WriteStartObject();

				writer.WritePropertyName("index");
				serializer.Serialize(writer, length);

				writer.WritePropertyName(name: "key");
				serializer.Serialize(writer, i);

				writer.WritePropertyName(name: "value");
				serializer.Serialize(writer, w);

				writer.WriteEndObject();
			}

			length += (w.Length + 1) * 2;
		}

		writer.WriteEndArray();
	}

	public override StringLookup ReadJson(JsonReader reader, Type objectType, StringLookup existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		throw new NotImplementedException();
	}
}



public class AttributeValueConverter : JsonConverter<AttributeValue>
{
	public override void WriteJson(JsonWriter writer, AttributeValue value, JsonSerializer serializer)
	{
		serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

		switch (value.Type)
		{
			case AttributeType.TNone:
			{
				if (value.IsArray) WriteArray(writer, value.AsArray, serializer);
				else if (value.IsDocument) WriteObject(writer, value.AsDocument, serializer);
				else writer.WriteValue(value?.ToString());
				break;
			}

			case AttributeType.TInt8: writer.WriteValue(value?.AsInt8); break;
			case AttributeType.TInt16: writer.WriteValue(value?.AsInt16); break;
			case AttributeType.TInt32: writer.WriteValue(value?.AsInt32); break;
			case AttributeType.TInt64: writer.WriteValue(value?.AsInt64); break;
			case AttributeType.TFloat32: writer.WriteValue(value?.AsFloat); break;
			case AttributeType.TBool: writer.WriteValue(value?.AsBoolean); break;
			//case AttributeType.TString:
			//	break;
			//case AttributeType.TSeq:
			//	break;
			//case AttributeType.TSeq16:
			//	break;
			//case AttributeType.TRef:
			//	break;
			//case AttributeType.TTRef:
			//	break;
			//case AttributeType.TSub:
			//	break;
			//case AttributeType.TSu:
			//	break;
			//case AttributeType.TVector16:
			//	break;
			//case AttributeType.TVector32:
			//	break;
			//case AttributeType.TIColor:
			//	break;
			//case AttributeType.TFColor:
			//	break;
			//case AttributeType.TBox:
			//	break;
			//case AttributeType.TAngle:
			//	break;
			//case AttributeType.TMsec:
			//	break;
			//case AttributeType.TDistance:
			//	break;
			//case AttributeType.TVelocity:
			//	break;
			//case AttributeType.TProp_seq:
			//	break;
			//case AttributeType.TProp_field:
			//	break;
			//case AttributeType.TScript_obj:
			//	break;
			//case AttributeType.TNative:
			//	break;
			//case AttributeType.TVersion:
			//	break;
			//case AttributeType.TIcon:
			// break;
			//case AttributeType.TTime32:
			//	break;
			//case AttributeType.TTime64:
			//	break;
			//case AttributeType.TXUnknown1:
			//	break;
			//case AttributeType.TXUnknown2:
			//	break;

			default:
				writer.WriteValue(value?.ToString());
				break;
		}
	}

	private static void WriteObject(JsonWriter writer, AttributeDocument obj, JsonSerializer serializer)
	{
		writer.WriteStartObject();

		foreach (var el in obj)
		{
			writer.WritePropertyName(el.Key);
			serializer.Serialize(writer, el.Value);
		}

		writer.WriteEndObject();
	}

	private static void WriteArray(JsonWriter writer, AttributeArray arr, JsonSerializer serializer)
	{
		writer.WriteStartArray();

		foreach (var item in arr)
		{
			serializer.Serialize(writer, item);
		}

		writer.WriteEndArray();
	}


	public override AttributeValue ReadJson(JsonReader reader, Type objectType, AttributeValue existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		throw new NotImplementedException();
	}
}