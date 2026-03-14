// Attack.cs
using Godot;
using System;
using System.Globalization;

[GlobalClass]
public partial class Attack : Resource
{
    [Export] public string Name { get; set; } = "New Attack";

	[Export] public AttackType Type { get; set; } 

	[Export] public int Range { get; set; } = 1;

	[Export] public int NbHit { get; set; } = 1;

	/// <summary>
	/// Multiplier parsed from <see cref="MultiplierRaw"/>.
	/// 1.0 == 100%.
	/// </summary>
	public double Multiplier { get; private set; } = 1.0;

	private string _multiplierRaw = "100%";

	/// <summary>
	/// The multiplier used for damage calculations.
	/// Accepts values like "100%", "150%", "0.5" or "2.5".
	/// </summary>
	[Export]
	public string MultiplierRaw
	{
		get => _multiplierRaw;
		set
		{
			_multiplierRaw = value;
			Multiplier = ParseMultiplier(value);
		}
	}

	/// <summary>
	/// Compute the total damage based on a base damage value, the multiplier, and the number of hits.
	/// </summary>
	public double ComputeTotalDamage(double baseDamage)
	{
		return baseDamage * Multiplier * Math.Max(1, NbHit);
	}


	private double ComputeSingleHitDamage(double baseDamage)
	{
		return baseDamage * Multiplier;
	}

	private static double ParseMultiplier(string raw)
	{
		if (string.IsNullOrWhiteSpace(raw))
			return 1.0;

		raw = raw.Trim();

		if (raw.EndsWith("%"))
		{
			var number = raw.Substring(0, raw.Length - 1).Trim();
			if (double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out var percent))
			{
				return percent / 100.0;
			}
		}

		if (double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
		{
			return value;
		}

		return 1.0;
	}
}

public enum AttackType
{
	Physical,
	Magical,
	True
}
