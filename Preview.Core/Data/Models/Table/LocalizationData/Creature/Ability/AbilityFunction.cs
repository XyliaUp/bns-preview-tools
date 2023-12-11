namespace Xylia.Preview.Data.Models.Creature;
public class AbilityFunction
{
    #region Fields
    public CreatureField Type;

    /// <summary>
    /// The rate of change
    /// 变化率（要求百分比形式, 此数值必定为整数）
    /// </summary>
    public int K;

    /// <summary>
    /// Constant Item (or initial value)
    /// 常数项 (起始值, 要求百分比形式)
    /// </summary>
    public int C;

    /// <summary>
    /// 等级修正参数
    /// </summary>
    public double μ;
    public double Φ;

    /// <summary>
    /// 当未计算出μ、Φ数值时, 可以使用特定等级数值进行临时替代
    /// </summary>
    public List<LevelFactor> LevelFactors = new();
    #endregion

    #region Properties
    public string Text => this.Type.ToString();
	#endregion


	#region Methods
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
        double ConvertPercent = (double)value * (0.01 * K) / (value + factor);
        return ConvertPercent + 0.01 * C;
    }


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

        return value * K / (percent - C) - value;
    }

    /// <summary>
    /// 计算修正参数
    /// </summary>
    /// <param name="factor1"></param>
    /// <param name="factor2"></param>
    public void GetFactorParam(LevelFactor factor1, LevelFactor factor2)
    {
        Φ = factor1.CalΦ(factor2);
        μ = factor1.Calμ(Φ);
    }
	#endregion

	#region Factor
	public class LevelFactor
    {
        public sbyte Level;

        public double Value;

        public LevelFactor(sbyte level, double value)
        {
            Level = level;
            Value = value;
        }

        public double CalΦ(LevelFactor factor2) => Math.Log(Value / factor2.Value) / (Level - factor2.Level);

        public double Calμ(double Φ) => Value / Math.Exp(Φ * Level);
    }
	#endregion


	#region Instance
	public static AbilityFunction AttackHit => new()
    {
        Type = CreatureField.AttackHitBasePercent,
        C = 85,
        K = 96,
        LevelFactors = new() { new(60, 6081.99) }
    };

    public static AbilityFunction AttackPierce => new()
    {
        //防御穿刺
        Type = CreatureField.AttackPierceBasePercent,
        K = 95,
        μ = 87.7627795879303,
        Φ = 0.0796897978783624,
    };

    public static AbilityFunction AttackParryPierce => new()
    {
        //格挡穿刺
        Type = CreatureField.AttackParryPiercePercent,
        K = 95,
        LevelFactors = new() { new(60, 20963.86) }
    };

    public static AbilityFunction AttackCritical => new()
    {
        Type = CreatureField.AttackCriticalBasePercent,
        K = 97,
        LevelFactors = new() { new(60, 7937.55) }
    };

    public static AbilityFunction AttackCriticalDamage => new()
    {
        Type = CreatureField.AttackCriticalDamagePercent,
        C = 125,
        K = 291,
        LevelFactors = new() { new(60, 7201.28) }
    };

    public static AbilityFunction DefendCritical => new()
    {
        Type = CreatureField.DefendCriticalBasePercent,
        K = 25,
        LevelFactors = new() { new(60, -19.47) }
    };

    public static AbilityFunction DefendCriticalDamage => new()
    {
        Type = CreatureField.DefendCriticalDamagePercent,
        K = 291,
        LevelFactors = new() { new(60, 2374.28) }
    };

    public static AbilityFunction DefendBounce => new()
    {
        Type = CreatureField.DefendBouncePercent,
    };

    public static AbilityFunction DefendDodge => new()
    {
        Type = CreatureField.DefendDodgeBasePercent,
        K = 95,
        LevelFactors = new() { new(60, 10464.33) }
    };

    public static AbilityFunction DefendParry => new()
    {
        Type = CreatureField.DefendParryBasePercent,
        K = 97,
        LevelFactors = new() { new(60, 5239.02) }
    };

    public static AbilityFunction DefendParryReducePercent => new()
    {
        //格挡伤害减免
        Type = CreatureField.DefendParryReducePercent,
        C = 30,
        K = 98,
        LevelFactors = new() { new(60, 21701.77) }
    };

    public static AbilityFunction DefendPerfectParry => new()
    {
        Type = CreatureField.DefendPerfectParryBasePercent,
    };

    public static AbilityFunction DefendImmune => new()
    {
        Type = CreatureField.DefendImmuneBasePercent,
    };

    public static AbilityFunction DefendMiss => new()
    {
        Type = CreatureField.DefendMissBasePercent,
    };

    public static AbilityFunction DefendPerfectParryReducePercent => new()
    {
        Type = CreatureField.DefendPerfectParryReducePercent,
    };

    public static AbilityFunction DefendCounterReducePercent => new()
    {
        Type = CreatureField.DefendCounterReducePercent,
    };

    //反击武功强化
    //public static AbilityFunction CounterEnhance => new()
    //{
    //	Type = CreatureField.CounterEnhance,
    //	K = 285,
    //	LevelFactors = new() { new(60, 9835.18) }
    //};

    //防御武功 强化
    //public static AbilityFunction DefenceParryDamageReducePercent => new()
    //{
    //	Type = CreatureField.DefenceParryDamageReduce,
    //	K = 291,
    //	LevelFactors = new() { new(60, 10042.45) }
    //};



    public static AbilityFunction HealPower => new()
    {
        Type = CreatureField.HealPowerBasePercent,
        C = 100,
        K = 54,
        LevelFactors = new() { new(60, 2796.48) }
    };

    public static AbilityFunction AoeDefend => new()
    {
        Type = CreatureField.AoeDefendBasePercent,
    };

    public static AbilityFunction AbnormalAttack => new()
    {
        Type = CreatureField.AbnormalAttackBasePercent,
        C = 100,
        K = 291,
        LevelFactors = new() { new(60, 12744.27) }
    };

    public static AbilityFunction AbnormalDefend => new()
    {
        Type = CreatureField.AbnormalDefendBasePercent,
    };

    public static AbilityFunction Hate => new()
    {
        Type = CreatureField.HateBasePercent,
    };

    public static AbilityFunction AttackAttribute => new()
    {
        Type = CreatureField.AttackAttributeBasePercent,
        C = 100,
        K = 291,
        LevelFactors = new() { new(60, 12002.79) }
    };

    public static AbilityFunction AttackAbnormalHit => new()
    {
        Type = CreatureField.AttackAbnormalHitBasePercent,
    };

    public static AbilityFunction DefendAbnormalDodge => new()
    {
        Type = CreatureField.DefendAbnormalDodgeBasePercent,
    };

    public static AbilityFunction SupportPower => new()
    {
        Type = CreatureField.SupportPowerBasePercent,
    };
    #endregion
}