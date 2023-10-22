using Godot;
using OutoftheDarkness.cs;
using System;

public class Dialog : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private RichTextLabel Backdrop { get; set; }
	private RichTextLabel Foredrop { get; set; }
	private Timer Timer { get; set; }

	private DialogText[] CurrentDialogs { get; set; }

	private int PhraseNum { get; set; }

	[Export]
	public string Text { 
		set
		{
			if (Backdrop != null) {
				Backdrop.BbcodeText = $"[center]{value}[/center]";
				Foredrop.BbcodeText = $"[center]{value}[/center]";
			}
		}
		get
		{
			if (Backdrop != null)
			{
				return Backdrop.Text;
			} else {
				return "";
			}
		}
	}

	public int VisibleCharacters
	{
		set
		{
			Foredrop.VisibleCharacters = value;
			Backdrop.VisibleCharacters = value;
		}
		get
		{
			return Backdrop.VisibleCharacters;
		}
	}

	public override void _Ready()
	{
		Backdrop = GetNode<RichTextLabel>("backdrop");
		Foredrop = GetNode<RichTextLabel>("foredrop");
		Timer = GetNode<Timer>("Timer");
	}

	public void NewDialog(DialogText[] given)
	{
		CurrentDialogs = given;
		NextPhrase();
	}
	
	public void NextPhrase()
	{
		if (VisibleCharacters < Text.Length && VisibleCharacters != -1)
		{
			VisibleCharacters++;
			// don't allow the next phrase to be pushed when the current phrase isn't done yet
			return;
		}
		if (PhraseNum > 0 && CurrentDialogs[PhraseNum - 1].AfterDequeue != null)
		{
			CurrentDialogs[PhraseNum - 1].AfterDequeue.Invoke();
		}
		if (PhraseNum < CurrentDialogs.Length)
		{
			var dialog = CurrentDialogs[PhraseNum];
			VisibleCharacters = 0;
			Text = dialog.Text;
			Timer.Connect("timeout", this, nameof(this.RevealText));
			Timer.Start(0.1f);
			PhraseNum++;
		} else {
			PhraseNum = 0;
			CurrentDialogs = new DialogText[] { };
			Text = "";
		}
	}

	public void RevealText()
	{
		if (VisibleCharacters < Text.Length) {
			VisibleCharacters++;
		} else {
			Timer.Disconnect("timeout", this, nameof(this.RevealText));
			Timer.Stop();
		}
	}
}
