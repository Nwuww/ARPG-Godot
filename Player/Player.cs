using Godot;
using System;
using System.Threading.Tasks;
public enum PlayerState
{
	Idle,
	Run,
	Attack,
	Roll,
	Death
}
public partial class Player : CharacterBody2D
{
	[ExportGroup("速度常数")]
	[Export] public float Max_Speed = 130;
	[Export] public float Acceleration = 1200;
	[Export] public float Friction = 2000;
	[Export] public float Roll_Speed = 100;
	[ExportGroup("")]
	
	public AnimationTree animationTree;
	public AnimationNodeStateMachinePlayback playback_state;
	public PlayerState state = PlayerState.Idle;
	public Vector2 RollVector = Vector2.Down;
	public SwordHitBox Sword;

	public override void _Ready()
	{
		// GD.Print("Hello from sakana");
		
		// foreach (Node child in GetChildren())
		// {
		// 	GD.Print("child: " + child.Name);
		// 	if (child.Name == "Marker2D")
		// 	{
		// 		foreach (Node grandchild in child.GetChildren())
		// 		{
		// 			GD.Print("- grandchild: " + grandchild.Name);
		// 		}
		// 	}
		// }

		Velocity = Vector2.Zero;
		animationTree = GetNode<AnimationTree>("AnimationTree");
		Sword = GetNode<Marker2D>("Marker2D").GetNode<SwordHitBox>("HitBox");
		Sword.KnockbackDirection = RollVector;
		playback_state = 
			(AnimationNodeStateMachinePlayback)(GodotObject)animationTree.Get("parameters/playback");
		playback_state.Travel("Idle");
	}
	
	public override void _Process(double delta)
	{
		switch (state)
		{
			case PlayerState.Idle:
				MoveSate(delta);
				break;
			case PlayerState.Run:
				MoveSate(delta);
				break;
			case PlayerState.Attack:
				AttackState(delta);
				break;
			case PlayerState.Roll:
				RollState(delta);
				break;
			case PlayerState.Death:
				break;
			default:
				break;
		}
	}

	public void MoveSate(double delta)
	{
		// move direction input
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();
		if (inputDir != Vector2.Zero)
		{
			RollVector = inputDir;
			Sword.KnockbackDirection = inputDir;
			animationTree.Set("parameters/Idle/blend_position", inputDir);
			animationTree.Set("parameters/Run/blend_position", inputDir);
			animationTree.Set("parameters/Attack/blend_position", inputDir);
			animationTree.Set("parameters/Roll/blend_position", inputDir);
			playback_state.Travel("Run");
			Velocity = Velocity.MoveToward(inputDir * Max_Speed, Acceleration * (float)delta);
		}
		else
		{
			playback_state.Travel("Idle");
			Velocity = Velocity.MoveToward(Vector2.Zero, Friction * (float)delta);
		}

		// key input monitor
		if (Input.IsActionJustPressed("attack"))
		{
			state = PlayerState.Attack;
		}
		else if (Input.IsActionJustPressed("roll"))
		{
			state = PlayerState.Roll;
		}
		else if (inputDir == Vector2.Zero)
		{
			state = PlayerState.Idle;
		}
		else
		{
			state = PlayerState.Run;
		}

		MoveAndSlide();
	}

	public void AttackState(double delta)
	{
		Velocity = Velocity.MoveToward(Vector2.Zero, Friction * (float)delta);
		playback_state.Travel("Attack");
	}
	public void RollState(double delta)
	{
		Velocity = RollVector * Roll_Speed;
		playback_state.Travel("Roll");
		MoveAndCollide(Velocity * (float)delta);
	}

	public void OnAttackAnimationFinished()
	{
		state = PlayerState.Idle;
	}
	public void OnRollAnimationFinished()
	{
		state = PlayerState.Idle;
	}
}
