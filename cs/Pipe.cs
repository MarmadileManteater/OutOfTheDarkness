using Godot;
using OutoftheDarkness.cs;
using System;

public class Pipe : Node2D
{
	private int LeftBound = -70;
	private int RightBound = 138;
	private int Margin = 10;
	private bool GoingDown = false;
	private bool GoingUp = false;
	private int Score = 10;
	private Timer GameOverTimer { get; set; }
	private Timer SpeedUpTimer { get; set; }
	private Player Player { get; set; }
	public bool GameWin { get; set; }

	public float Speed = 1;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetParent().GetNode<Player>("Player");
		GameOverTimer = GetNode<Timer>("GameOverTimer");
		SpeedUpTimer = GetNode<Timer>("SpeedUpTimer");
		SpeedUpTimer.Connect("timeout", this, nameof(SpeedUp));
		SpeedUpTimer.Start(5);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (GameWin)
		{
			Position -= new Vector2(0, 0.5f * Speed);
			return;
		}
		if (Player.HasRetreated)// gameover state
			return;
		if (!GoingDown && !GoingUp)
		{
			if (Player.Position.x > Position.x - LeftBound)
			{
				Position += new Vector2(0.5f * Speed, 0);
			}
			if (Player.Position.x < Position.x - LeftBound)
			{
				Position -= new Vector2(0.5f * Speed, 0);
			}
		}
		if (GoingDown && Position.y < -10)
		{
			Position += new Vector2(0, 0.2f * Speed);
		} else if (GoingDown) {
			GoingUp = true;
			GoingDown = false;
		}
		if (GoingUp && Position.y > -34) {
			Position -= new Vector2(0, 0.2f * Speed);
		} else if (GoingUp) {
			GoingUp = false;
		}
		base._Process(delta);
	}

	private void SpeedUp()
	{
		if (GameWin || Player.IsAbducted)
			return;
		if (Speed < 10)
		{
			Speed += 1f;
		} else {
			// game win
			GameWin = true;
			// a little hacky but prevents double win con
			Player.HasRetreated = true;
			SpeedUpTimer.Disconnect("timeout", this, nameof(SpeedUp));
			SpeedUpTimer.Stop();
			var gameOverScene = (GameOver)ResourceLoader.Load<PackedScene>("res://scenes/GameOver.tscn").Instance();
			gameOverScene.Text = $"You succesfully bamboozled the pipe!";
			GetTree().Root.GetNode<Node2D>("Node2D").AddChild(gameOverScene);

		}
	}

	public void OnBodyEntered(object body)
	{
		if (GameWin)
			return;
		var kbody = (Abductable)body;
		if (!kbody.IsAbducted)
		{
			Score--;
		}
		kbody.Abduct(this);
		if (kbody == Player)
		{
			GameOverTimer.Connect("timeout", this, nameof(GameOver));
			GameOverTimer.Start(3);
		}
	}

	public void GameOver()
	{
		GameOverTimer.Disconnect("timeout", this, nameof(GameOver));
		GameOverTimer.Stop();
		SpeedUpTimer.Disconnect("timeout", this, nameof(SpeedUp));
		SpeedUpTimer.Stop();
		var gameOverScene = (GameOver)ResourceLoader.Load<PackedScene>("res://scenes/GameOver.tscn").Instance();
		gameOverScene.Text = $"Game Over\nScore: {Score + Speed}";
		GetTree().Root.GetNode<Node2D>("Node2D").AddChild(gameOverScene);

	}

	public void OnScannerEntered(object body)
	{
		if (GameWin)
			return;
		if (GoingUp)
			return;
		var kbody = (Abductable)body;
		if (kbody == Player)
		{
			GoingDown = true;
		}
	}

}

