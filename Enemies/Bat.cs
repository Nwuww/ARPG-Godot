using Godot;
using System;

public partial class Bat : CharacterBody2D
{
	public Vector2 KnockbackDirection = Vector2.Zero;
	public float Friction = 200;
	[ExportGroup("自定义")]
	[ExportSubgroup("击退强度")] public float KnockBackStrength = 120; 
	[ExportSubgroup("属性")] 
	[Export] public double Health = 1000;
	[Export] public double MaxHealth = 1000;
	[Export] public double Attack = 50;
	[Export] public double Defense = 100;
	[ExportGroup("")]

	public Stats stats;
	public override void _Ready()
	{
		stats = new Stats(Health, Health, Attack, Defense);
	}

	public override void _Process(double delta)
	{
		KnockbackDirection = KnockbackDirection.MoveToward(Vector2.Zero, Friction * (float)delta);
		Velocity = KnockbackDirection;
		MoveAndSlide();
	}

	public void OnHurt(Area2D area)
	{
		GD.Print("[debug] Bat hurt");
		KnockbackDirection = ((SwordHitBox)area).KnockbackDirection * KnockBackStrength;
	}

}
