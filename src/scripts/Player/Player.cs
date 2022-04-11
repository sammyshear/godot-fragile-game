using Godot;
using System;

namespace FragileGame.Player
{
    public class Player : KinematicBody2D
    {
        [Export]
        public int speed = 20;

        [Export]
        public int jumpSpeed = -1000;

        [Export]
        public int gravity = 4000;

        private AnimatedSprite sprite;
        private SpriteFrames frames;
        private int maxHealth;
        private int curHealth;
        private Vector2 velocity = Vector2.Zero;
        private Vector2 nextMove;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            sprite = GetNode<AnimatedSprite>("Pivot/Body");
            frames = sprite.Frames;
            if (frames != null)
            {
                maxHealth = frames.GetFrameCount("default");
            }
            curHealth = maxHealth;
        }

        public override void _PhysicsProcess(float delta)
        {
            GetInput();
            velocity.y += gravity * delta;
            nextMove = velocity;
            velocity = MoveAndSlide(nextMove, Vector2.Up);
            if (Input.IsActionJustPressed("jump"))
            {
                if (IsOnFloor())
                {
                    velocity.y = jumpSpeed;
                }
            }

            if (IsOnFloor() && nextMove.y >= 1700)
            {
                NextFrame();
            }
            else if (IsOnWall() && (nextMove.x >= 1000 || nextMove.x <= -1000))
            {
                NextFrame();
            }
        }

        public void GetInput()
        {
            if (Input.IsActionPressed("walk_right"))
            {
                velocity.x += speed;
            }
            else if (Input.IsActionPressed("walk_left"))
            {
                velocity.x -= speed;
            }
            else
            {
                velocity.x = 0;
            }
        }

        public void NextFrame()
        {
            curHealth -= 1;
            sprite.Frame = maxHealth - curHealth;
            if (curHealth == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetTree().ChangeScene("res://src/scenes/GameOver.tscn");
        }
    }
}
