using Godot;
using OutoftheDarkness.cs;
using System;

public class Player : Abductable
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public bool Left { get; set; }
	public bool Right { get; set; }
	public bool HasRetreated { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsAction("ui_left"))
		{
			if (@event.IsActionPressed("ui_left"))
			{
				Left = true;
			}
			if (@event.IsActionReleased("ui_left"))
			{
				Left = false;
			}
		}
		if (@event.IsAction("ui_right"))
		{
			if (@event.IsActionPressed("ui_right"))
			{
				Right = true;
			}
			if (@event.IsActionReleased("ui_right"))
			{
				Right = false;
			}
		}
		base._Input(@event);
	}

	 // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (!IsAbducted)
		{
			if (Left)
			{
				MoveAndCollide(new Vector2(-2, 0));
			}
			if (Right)
			{
				MoveAndCollide(new Vector2(2, 0));
			}
		}
		if (Math.Abs(Position.x) > 800 && !HasRetreated && !IsAbducted)
		{
			HasRetreated = true;
			
			var scene = ResourceLoader.Load<PackedScene>("res://scenes/RanToTheLeftButThereAreNoGoodEndings.tscn").Instance();
			GetTree().Root.GetNode<Node2D>("Node2D").AddChild(scene);
		}
		base._Process(delta);
	}
}
