using Godot;
using System;

namespace FragileGame.Game
{
    public class Ending : Area2D
    {

        [Export(PropertyHint.File, "*.tscn")]
        public string scene;
        
        private AnimatedSprite sprite;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready() { 
            sprite = GetNode<AnimatedSprite>("AnimatedSprite");
            sprite.Play();
        }

         // Called every frame. 'delta' is the elapsed time since the previous frame.
         public override void _Process(float delta)
         {
         }

         public void OnEndingEntered(KinematicBody2D body) {
             if (body.IsInGroup("Player")) {
                 QueueFree();
                 GetTree().ChangeScene(scene);
             }
         }
    }
}
