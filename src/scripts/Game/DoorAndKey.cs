using Godot;
using System;

namespace FragileGame.Game
{
    public class DoorAndKey : Node2D
    {
        private AnimatedSprite keySprite;

        private Area2D key;
        private Area2D door;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            keySprite = GetNode<AnimatedSprite>("Key/AnimatedSprite");
            keySprite.Play();
            key = GetNode<Area2D>("Key");
            door = GetNode<Area2D>("Door");
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta) { }

        public void OnKeyBodyEntered(KinematicBody2D body)
        {
            if (body.IsInGroup("Player"))
            {
                QueueFree();
            }
        }
    }
}
