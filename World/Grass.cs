using Godot;
using System;

public partial class Grass : Node2D
{
	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		
	}

	public void GrassEffect()
	{
		var grassEffect = (GrassEffect)GD.Load<PackedScene>("res://Effects/grass_effect.tscn").Instantiate();
		GetParent().AddChild(grassEffect);
		grassEffect.Position = Position;
		grassEffect.Rotation = Rotation;
	}
	public void OnAreaEntered(Area2D area)
	{
		GD.Print("[debug] Grass OnAreaEntered()");
		QueueFree();
		GrassEffect();
	}
}
