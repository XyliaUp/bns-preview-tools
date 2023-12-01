using System.Text;

using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;
public class RecordBuilder
{
	private readonly DatafileDefinition _definitions;

	private readonly StringBuilder _stringBuilder = new StringBuilder(0x2000);
	private readonly Dictionary<string, int> _existingStringOffset = new Dictionary<string, int>(0x2000);

	// States
	private bool _isCompressed;


	public StringLookup StringLookup { get; private set; }

	public void InitializeTable(bool isCompressed, StringLookup loadStringLookup = null)
	{
		_isCompressed = isCompressed;

		if (isCompressed)
			return;

		// Per table
		if (loadStringLookup == null)
		{
			StringLookup = new StringLookup { IsPerTable = true };

			_stringBuilder.Clear();
			_existingStringOffset.Clear();
		}
		else
		{
			StringLookup = loadStringLookup;

			_stringBuilder.Clear();
			_existingStringOffset.Clear();

			var data = Encoding.Unicode.GetString(loadStringLookup.Data);

			if (data.Length * 2 != loadStringLookup.Data.Length)
				throw new Exception("Invalid loaded lookup length");

			// TODO: load offsets for existing strings
			_stringBuilder.Append(data);
		}
	}

	public void InitializeRecord()
	{
		if (_isCompressed)
		{
			// Per record
			StringLookup = new StringLookup();

			_stringBuilder.Clear();
			_existingStringOffset.Clear();
		}
	}

	public void InitializeMutateRecord(Record record)
	{
		if (_isCompressed)
		{
			var stringLookup = record.StringLookup;
			StringLookup = stringLookup;

			_stringBuilder.Clear();
			_existingStringOffset.Clear();

			var data = Encoding.Unicode.GetString(stringLookup.Data);

			if (data.Length * 2 != stringLookup.Data.Length)
				throw new Exception("Invalid loaded lookup length");

			// TODO: load offsets for existing strings
			_stringBuilder.Append(data);
		}
	}

	public void FinalizeMutateRecord()
	{
		if (_isCompressed)
		{
			// Per record
			StringLookup.Data = Encoding.Unicode.GetBytes(_stringBuilder.ToString());
		}
	}

	public void FinalizeRecord()
	{
		if (_isCompressed)
		{
			// Per record
			StringLookup.Data = Encoding.Unicode.GetBytes(_stringBuilder.ToString());
		}
	}

	public void FinalizeTable()
	{
		if (_isCompressed)
			return;

		// Per table
		StringLookup.Data = Encoding.Unicode.GetBytes(_stringBuilder.ToString());
	}



	private void SetString(Record record, AttributeDefinition attrDef, string value)
	{
		value ??= attrDef.AttributeDefaultValues.DString;

		if (true || !_existingStringOffset.TryGetValue(value, out var stringOffset))
		{
			stringOffset = _stringBuilder.Length * 2;
			_stringBuilder.Append(value);
			_stringBuilder.Append('\0');
			_existingStringOffset[value] = stringOffset;
		}

		record.Set(attrDef.Offset, stringOffset);
	}

	private void SetNative(Record record, AttributeDefinition attrDef, string value)
	{
		value ??= "";

		var offset = _stringBuilder.Length * 2;
		_stringBuilder.Append(value);
		_stringBuilder.Append('\0');

		record.Set(attrDef.Offset, new Native(_stringBuilder.Length * 2 - offset, offset));
	}
}