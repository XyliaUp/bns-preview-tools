namespace Xylia.Match.Windows.Attribute.Data;
public class LevelFactor
{
	public sbyte Level;

	public double Value;

	public LevelFactor(sbyte level , double value)
	{
		this.Level = level;
		this.Value = value;
	}



	public double CalΦ(LevelFactor factor2) => Math.Log(this.Value / factor2.Value) / (this.Level - factor2.Level);

	public double Calμ(double Φ) => this.Value / Math.Exp(Φ * this.Level);
}