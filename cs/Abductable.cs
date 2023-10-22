using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutoftheDarkness.cs
{
    public class Abductable : KinematicBody2D
    {
        public bool IsAbducted { get; set; }
        public Pipe Abductor { get; set; }
        public Vector2 AbductorPosition { get; set; }
        public void Abduct(Pipe abductor)
        {
            if (!IsAbducted)
            {
                IsAbducted = true;
                Abductor = abductor;
                AbductorPosition = abductor.Position + Vector2.Zero;
            }
        }
        public override void _Process(float delta)
        {
            if (IsAbducted && Position.y > 33)
            {
                MoveAndSlide(new Vector2(0, -100));
            } else if (IsAbducted)
            {
                Hide();
            }
            if (IsAbducted)
            {
                var dx = AbductorPosition.x - Abductor.Position.x;
                AbductorPosition = Abductor.Position;
                Position -= new Vector2(dx, 0);
            }
            base._Process(delta);
        }
    }
}
