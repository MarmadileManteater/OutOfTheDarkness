using Godot;
using OutoftheDarkness.cs;
using System;

public class Scene1 : Node2D
{
	private Dialog Dialog { get; set; }
	private Timer Timer { get; set; }
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer = GetNode<Timer>("Timer");
		Dialog = GetNode<Dialog>("Camera2D/Dialog");
		Dialog.NewDialog(new[] {
			new DialogText
			{
				Text = "Breaking News: it has been revealed that the U.S.-"
			},
			new DialogText
			{
				Text = "-government has been employing beings from-"
			},
			new DialogText
			{
				Text = "-another planet."
			},
			new DialogText
			{
				Text = "These beings are currently outside of-"
			},
			new DialogText
			{
				Text = "-Area 51 making a list of demands that include-",
			},
			new DialogText
			{
				Text = "-a living wage, safer working-conditions and-",
			},
			new DialogText
			{
				Text = "-less exclusionary family leave requirements",
			},
			new DialogText
			{
				Text = "All in all, the main message is that-",
			},
			new DialogText
			{
				Text = "-they are done hiding in darkness.",
				AfterDequeue = () =>
				{
					Timer.Connect("timeout", this, nameof(SwitchScene));
					// wait 2s and then flip to the next scene
					Timer.Start(2);
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

	public void SwitchScene()
	{
		GetTree().ChangeScene("res://scenes/scene-2-5.tscn");
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
