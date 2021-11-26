using Godot;
using System;

public class EnemieBandits : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    const int speed = 32;
    public bool isMovingLeft = true;
    const int GRAVITY = 15;
    Vector2  Velocity = new Vector2(0, 0);

    
    Sprite currentSprite;
     AnimationPlayer animPlayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        currentSprite = GetNode<Sprite>("Sprite");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animPlayer.Play("Moving");

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      //animPlayer.Play("Idle");
      move_character();
      DetecTurnAround();
  }

  public void move_character()
  {
      if (isMovingLeft)
      {
          Velocity.x = -speed;
      }
      else
      {
        Velocity.x = speed;
      }
    
    Velocity.y += GRAVITY; 

    Velocity = MoveAndSlide(Velocity, Vector2.Up);
  }
  public void DetecTurnAround()
  {
    
      if (!GetNode<RayCast2D>("RayCast2D").IsColliding() && IsOnFloor())
      {
          GD.Print("Time to turn around");
          isMovingLeft = !isMovingLeft;
          currentSprite.FlipH = !currentSprite.FlipH;
          
      }
  }

  
}
