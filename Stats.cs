using Godot;
using System;

public partial class Stats : Node
{
	[ExportGroup("属性")]
	public double Health;
	public double MaxHealth;
	public double Attack;
	public double Defense;

	public Stats(double health, double maxHealth, double attack, double defense)
	{
		Health = health;
		MaxHealth = maxHealth;
		Attack = attack;
		Defense = defense;
	}
}
