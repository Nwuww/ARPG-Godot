using Godot;
using System;

public partial class Bat : CharacterBody2D
{
	public Vector2 KnockbackDirection = Vector2.Zero;
	public float Friction = 200;
	public float KnockBackStrength = 120;
	public override void _Ready()
	{
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
