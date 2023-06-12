using System;

namespace Xylia.Match.Windows.Attribute.Data
{
	/// <summary>
	/// 等级修正系数
	/// </summary>
	public class LevelFactor
	{
		public byte Level;

		public double Value;

		public LevelFactor(byte level , double value)
		{
			this.Level = level;
			this.Value = value;
		}



		public double CalΦ(LevelFactor factor2) => Math.Log(this.Value / factor2.Value) / (this.Level - factor2.Level);

		public double Calμ(double Φ) => this.Value / Math.Exp(Φ * this.Level);
	}
}