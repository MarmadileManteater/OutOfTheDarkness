using Godot;
using OutoftheDarkness.cs;
using System;

public class Ally : Abductable
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	private KinematicBody2D Player { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetParent().GetNode<KinematicBody2D>("Player");   

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (!IsAbducted)
		{
			if (Player.Position.x < Position.x)
			{
				MoveAndCollide(new Vector2(-1, 0));
			}
			if (Player.Position.x > Position.x)
			{
				MoveAndCollide(new Vector2(1, 0));
			}
		}
		base._Process(delta);
	}
}
