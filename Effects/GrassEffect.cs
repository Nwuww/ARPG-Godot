using Godot;
using System;

public partial class GrassEffect : Node2D
{
	public AnimatedSprite2D animatedSprite2D;

	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("Animate");
	}

	public void _on_animated_sprite_2d_animation_finished()
	{
		QueueFree();
	}
}
