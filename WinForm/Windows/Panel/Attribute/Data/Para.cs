using System.Xml;

using Xylia.Extension;

namespace Xylia.Match.Windows.Attribute.Data;
public class ParaEntity
{
	#region Constructor
	public ParaEntity()
	{

	}

	public ParaEntity(XmlElement xe)
	{
		this.name = xe.Attributes["name"]?.Value;
		this.category = xe.Attributes["category"]?.Value;
		this.type = xe.Attributes["type"]?.Value.ToEnum<ParaType>() ?? default;

		this.K = xe.Attributes["k"]?.Value.ToInt() ?? 0;
		this.C = xe.Attributes["c"]?.Value.ToInt() ?? 0;
		this.μ = xe.Attributes["μ"]?.Value.ToDouble() ?? 0;
		this.Φ = xe.Attributes["Φ"]?.Value.ToDouble() ?? 0;
		foreach (XmlElement level in xe.SelectNodes("./level"))
		{
			var key = level.Attributes["key"]?.Value.ToByte() ?? 0;
			var value = level.Attributes["value"]?.Value.ToDouble() ?? 0;
			if (value == 0) continue;

			this.LevelFactors.Add(new(key, value));
		}
	}
	#endregion




	public ParaType type;

	public string name;

	public string category;

	/// <summary>
	/// The rate of change
	/// 变化率（要求百分比形式, 此数值必定为整数）
	/// </summary>
	public double K;

	/// <summary>
	/// Constant Item (or initial value)
	/// 常数项 (起始值, 要求百分比形式)
	/// </summary>
	public double C;


	#region level factor
	/// <summary>
	/// 当未计算出μ、Φ数值时, 可以使用特定等级数值进行临时替代
	/// </summary>
	public List<LevelFactor> LevelFactors = new();



	/// <summary>
	/// 修正参数1
	/// </summary>
	public double μ;

	/// <summary>
	/// 修正参数2
	/// </summary>
	public double Φ;

	public double GetFactor(byte level)
	{
		if (μ == 0) throw new ArgumentException(nameof(μ));
		else if (Φ == 0) throw new ArgumentException(nameof(Φ));

		return μ * Math.Exp(Φ * level);
	}

	/// <summary>
	/// 已知变化率、常数项、Property数值与Property比率时, 获取特定的等级修正系数
	/// </summary>
	/// <param name="value"></param>
	/// <param name="percent"></param>
	/// <returns></returns>
	public double GetFactor(double value, double percent)
	{
		if (K == 0) throw new ArgumentException(nameof(K));

		return (value * K) / (percent - C) - value;
	}

	/// <summary>
	/// 计算修正参数
	/// </summary>
	/// <param name="factor1"></param>
	/// <param name="factor2"></param>
	public void GetFactorParam(LevelFactor factor1, LevelFactor factor2)
	{
		Φ = factor1.CalΦ(factor2);
		μ = factor1.Calμ(this.Φ);
	}
	#endregion




	public double GetPercent(double value, byte level)
	{
		double factor = 0;

		try
		{
			if (μ == 0) throw new ArgumentNullException(nameof(μ));
			else if (Φ == 0) throw new ArgumentNullException(nameof(Φ));

			factor = GetFactor(level);
		}
		catch
		{
			var o = LevelFactors.Find(f => f.Level == level);
			if (o is null) throw;

			factor = o.Value;
		}

		return GetPercent(value, factor);
	}

	public double GetPercent(double value, double factor)
	{
		double ConvertPercent = ((double)value * (0.01 * K) / (value + factor));
		return ConvertPercent + 0.01 * C;
	}





	#region Static Functions
	public static List<ParaEntity> LoadParas(XmlDocument doc)
	{
		var data = new List<ParaEntity>();
		foreach (XmlElement xmlNode in doc.SelectNodes("*/record"))
			data.Add(new ParaEntity(xmlNode));

		return data;
	}
	#endregion
}