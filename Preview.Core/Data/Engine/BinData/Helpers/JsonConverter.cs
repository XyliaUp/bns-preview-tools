using System.Text;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Models;
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
		serializer.Serialize(writer, value.RecordId);

		writer.WritePropertyName("variation");
		serializer.Serialize(writer, value.RecordVariationId);

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

		if (value.IsArray)
		{
			WriteArray(writer, value.AsArray, serializer);
		}
		else if (value.IsDocument)
		{
			WriteObject(writer, value.AsDocument, serializer);
		}
		else
		{
			writer.WriteValue(value.RawValue);
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