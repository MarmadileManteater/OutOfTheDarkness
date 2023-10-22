using Godot;
using OutoftheDarkness.cs;
using System;

public class RanToTheLeftButThereAreNoGoodEndings : Node2D
{
	private Dialog Dialog { get; set; }
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Dialog = GetNode<Dialog>("Dialog");
		Dialog.NewDialog(new[]
		{
			new DialogText
			{
				Text = "The ET workers may have escaped the pipe of-"
			},
			new DialogText
			{
				Text = "-darkness, but you wouldn't really call-"
			},
			new DialogText
			{
				Text = "-retreating a victory for a strike."
			},
			new DialogText
			{
				Text = "Game Over\nScore: 10",
				AfterDequeue = () =>
				{
					GetTree().ChangeScene("res://scenes/scene-1.tscn");
				}
			}
		});
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsAction("ui_accept"))
		{
			Dialog.NextPhrase();
		}
		base._Input(@event);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
