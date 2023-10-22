using Godot;
using System;

public class GameOver : Node2D
{
	[Export]
	public string Text { get; set; }
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Dialog>("Dialog").Text = $"{Text}";
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsAction("ui_accept"))
		{
			if (@event.IsActionPressed("ui_accept"))
			{
				GetTree().ChangeScene("res://scenes/scene-1.tscn");
			}
		}
		base._Input(@event);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
