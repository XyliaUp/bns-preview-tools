using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BnsBinTool.Core;
using BnsBinTool.Core.DataStructs;
using BnsBinTool.Core.Definitions;
using BnsBinTool.Core.Helpers;

using Xylia.Preview.Data.Definition;

using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Helper;

public class DatafileAliasResolverHelper
{
	public static void Resolve(ResolvedAliases resolvedAliases, DatafileDefinition datafileDef, IEnumerable<TableModel> tablesEnumerable, bool ignoreInvalidReferences = false)
	{
		var tables = tablesEnumerable.ToDictionary(x => (int)x.Type);

		foreach (var tableDef in datafileDef.TableDefinitions)
		{
			var byRef = new Dictionary<Ref, string>();
			var byAlias = new Dictionary<string, Ref>();

			resolvedAliases.ByRef[tableDef.Type] = byRef;
			resolvedAliases.ByAlias[tableDef.Type] = byAlias;
		}

		Parallel.ForEach(datafileDef.TableDefinitions, tableDef =>
		{
			var byRef = resolvedAliases.ByRef[tableDef.Type];
			var byAlias = resolvedAliases.ByAlias[tableDef.Type];

			var aliasAttrDef = ((MyTableDefinition)tableDef)["alias"];
			if (aliasAttrDef == null)
				return;

		
			if (!tables.TryGetValue(tableDef.Type, out var table))
				return;

			foreach (var record in table.Records)
			{
				var reference = new Ref(record.RecordId, record.RecordVariationId);
				var alias = record.StringLookup.GetString(record.Get<int>(aliasAttrDef.Offset));

				if (alias != null)
				{
					byRef[reference] = alias;
					byAlias[alias] = reference;
				}
				else
				{
					if (!ignoreInvalidReferences)
						ThrowHelper.ThrowException($"Failed to read alias (This usually means ur using wrong table definition) {tableDef.Name}");
				}
			}
		});
	}

	public static void ResolveXmlDatAlias(ResolvedAliases resolvedAliases, DatafileDefinition datafileDef)
	{
		string extractedXmlDatPath = null;
		string extractedLocalDatPath = null;


		if (extractedXmlDatPath != null)
		{
			{
				var byAlias = resolvedAliases.ByAlias[datafileDef["quest"].Type];
				var byRef = resolvedAliases.ByRef[datafileDef["quest"].Type];
				XmlRecordsHelper.EnumerateRecords("xml.dat", extractedXmlDatPath, "quest/questdata*.xml", "quest", record =>
				{
					if (record.TryGetValue("alias", out var alias)
						&& record.TryGetValue("id", out var id_str)
						&& int.TryParse(id_str, out var id))
					{
						byAlias[alias] = new Ref(id);
						byRef[new Ref(id)] = alias;
					}
				});
			}

			if (datafileDef.TryGetValue("tutorialskillsequence", out _))
			{
				var byAlias = resolvedAliases.ByAlias[datafileDef["tutorialskillsequence"].Type];
				var byRef = resolvedAliases.ByRef[datafileDef["tutorialskillsequence"].Type];
				var autoId = 1;
				XmlRecordsHelper.EnumerateRecords("xml.dat", extractedXmlDatPath, "tutorialskillsequencedata*.xml", "tutorialSkillSequence", record =>
				{
					if (record.TryGetValue("alias", out var alias))
					{
						byAlias[alias] = new Ref(autoId);
						byRef[new Ref(autoId)] = alias;
					}

					autoId++;
				});
			}
			
			{
				var byAlias = resolvedAliases.ByAlias[datafileDef["summoned-sequence"].Type];
				var byRef = resolvedAliases.ByRef[datafileDef["summoned-sequence"].Type];
				var autoId = 1;
				XmlRecordsHelper.EnumerateRecords("xml.dat", extractedXmlDatPath, "summonedsequencedata*.xml", "summoned-sequence", record =>
				{
					if (record.TryGetValue("alias", out var alias))
					{
						byAlias[alias] = new Ref(autoId);
						byRef[new Ref(autoId)] = alias;
					}

					autoId++;
				});
			}

			if (datafileDef.TryGetValue("skill-training-sequence", out _))
			{
				var byAlias = resolvedAliases.ByAlias[datafileDef["skill-training-sequence"].Type];
				var byRef = resolvedAliases.ByRef[datafileDef["skill-training-sequence"].Type];
				var autoId = 1;
				XmlRecordsHelper.EnumerateRecords("xml.dat", extractedXmlDatPath, "skilltrainingsequencedata*.xml", "skill-training-sequence", record =>
				{
					if (record.TryGetValue("alias", out var alias))
					{
						byAlias[alias] = new Ref(autoId);
						byRef[new Ref(autoId)] = alias;
					}

					autoId++;
				});
			}

			{
				var byAlias = resolvedAliases.ByAlias[datafileDef["contextscript"].Type];
				var byRef = resolvedAliases.ByRef[datafileDef["contextscript"].Type];
				var autoId = 1;
				XmlRecordsHelper.EnumerateRecords("xml.dat", extractedXmlDatPath, "skill3_contextscriptdata*.xml", "contextscript", record =>
				{
					if (record.TryGetValue("alias", out var alias))
					{
						byAlias[alias] = new Ref(autoId);
						byRef[new Ref(autoId)] = alias;
					}

					autoId++;
				});
			}
		}

		if (extractedLocalDatPath != null && datafileDef.TryGetValue("surveyquestions", out _))
		{
			var byAlias = resolvedAliases.ByAlias[datafileDef["surveyquestions"].Type];
			var byRef = resolvedAliases.ByRef[datafileDef["surveyquestions"].Type];
			var autoId = 1;
			XmlRecordsHelper.EnumerateRecords("local.dat", extractedLocalDatPath, "outsource/surveyquestions.x16", "surveyQuestion", record =>
			{
				if (record.TryGetValue("alias", out var alias))
				{
					byAlias[alias] = new Ref(autoId);
					byRef[new Ref(autoId)] = alias;
				}

				autoId++;
			});
		}
	}
}