
using System;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct;

public sealed class RefInfo
{
	#region Constructor
	public RefInfo(string RefTable, int? StartOffset = null, OutType OutType = default)
	{
		this.RefTable = RefTable;
		this.StartOffset = StartOffset;
		this.OutType = OutType;
	}
	#endregion

	#region Fields
	/// <summary>
	/// 指示指向开始信息
	/// </summary>
	public int? StartOffset;

	/// <summary>
	/// 外键
	/// </summary>
	public string RefTable;

	public OutType OutType;
	#endregion


	public static RefInfo Load(string RefTable, string LinkInfo)
	{
		#region Initialize
		if (RefTable is null) throw new ArgumentNullException();

		var Link = new RefInfo(RefTable);
		if (string.IsNullOrWhiteSpace(LinkInfo)) return Link;
		#endregion

		#region 处理输出类型
		Link.OutType = LinkInfo?.Trim().ToLower() switch
		{
			"name" => OutType.Name,
			"id" => OutType.Id,

			_ => OutType.Default,
		};

		return Link;
		#endregion
	}
}

public enum OutType
{
	Default,
	Id,
	Name,

	Text,
	TextAlias,
}