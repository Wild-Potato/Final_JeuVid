using Godot;
using System;

public class Player : KinematicBody2D
{
    Vector2 UP = new Vector2(0, -1);
    const int GRAVITY = 15;
    const int MAXFALLSPEED = 300;
    const int MAXSPEED = 150;
    const int JUMPFORCE = 300;
    int temps =1;
    int attack =1;
    int hurt =0;

    double health = 8;

    const int ACCEL = 10;
    Vector2 vZero = new Vector2();

    bool facing_right = true;

    int hitConter =0;
    Vector2 motion = new Vector2();

    Sprite currentSprite;
    AnimationPlayer animPlayer;
        // Called when the node enters the scene tree for the first time.
    AnimationTree animationTree;
    TextureProgress healthBar;
    Area2D area;
    Label healthDebug;
    Label hitConterDebug;
    Label playerStateDebug;
    public override void _Ready()
    {
        healthBar = GetNode<TextureProgress>("HealthBar/TextureProgress");
        currentSprite = GetNode<Sprite>("Sprite");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationTree = GetNode<AnimationTree>("AnimationTree");
        area = GetNode<Area2D>("_on_Hurtbox_area_entered");
        healthDebug = GetNode<Label>("Debug/healthDebug");
        hitConterDebug = GetNode<Label>("Debug/hitConterDebug");
        playerStateDebug = GetNode<Label>("Debug/playerState");
    }

    

    public override void _PhysicsProcess(float delta)
    {
        
        healthBar.Value = health;
        
        healthDebug.Text = $"Health :{health}";
        hitConterDebug.Text = $"HitCounter :{hitConter}";
        motion.y += GRAVITY;

        if(motion.y > MAXFALLSPEED) {
            motion.y = MAXFALLSPEED;
        }

        if (facing_right) {
            currentSprite.FlipH = false;
        } else {
            currentSprite.FlipH = true;
        }

        if (motion.x > MAXSPEED)
        {
            motion.x = MAXSPEED;
        }
        if ( motion.x < -MAXSPEED)
        {
             motion.x = -MAXSPEED;            
        }

         //motion.x = motion.Clamped(MAXSPEED).x;

        if (Input.IsActionPressed("ui_left")) {
            playerStateDebug.Text = "State : Run";
            temps =1;
            motion.x -= ACCEL;
            facing_right = false;
            animationTree.Set("parameters/Mouvement/current",1);
            //animPlayer.Play("Run");
        } else if (Input.IsActionPressed("ui_right")) {
            playerStateDebug.Text = "State : Run";
            temps =1;
            motion.x += ACCEL;
            facing_right = true;
            animationTree.Set("parameters/Mouvement/current",1);
            //animPlayer.Play("Run");
        } else {
           // motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
           motion.x=0;
           if (temps ==1)
           {
               animationTree.Set("parameters/Mouvement/current",0);
                playerStateDebug.Text = "State : Idle";
           }
            
            //animPlayer.Play("Idle");
        }
        if (health >= 1)
        {
            animationTree.Set("parameters/Is_Alive/current",0);
        }

        if (IsOnFloor())
        {
            if (Input.IsActionJustPressed("ui_attack"))
            {
                playerStateDebug.Text = $"State : Attack{attack}";
                hitConter++;
                if (attack==2)
                {
                    attack = 1;
                }
                else
                {
                    attack = 2;
                }
            }
            

            animationTree.Set("parameters/In_Air_State/current",0);
            // On ne regarde qu'un seul fois et non le maintient de la touche
            

            if (Input.IsActionJustPressed("ui_attack") &&  attack ==2) {
                playerStateDebug.Text = $"State : Attack{attack}";
                do
                {
                    GD.Print($"Attack 2 {attack}");
                    animationTree.Set("parameters/Mouvement/current",4);
                    
                    motion.x=0;
                    temps = 0;
                   
                    
                    
                } while (temps ==1);
               
               
            }

            if (hurt == 1) {
                
                health --;
                
                 GD.Print($"Hurt health left : {healthBar.Value}");

                do
                {
                    //GD.Print($"Hurt health left : {health}");
                    animationTree.Set("parameters/Mouvement/current",6);
                    hurt = 0;
                    motion.x=0;
                    temps = 0;
                    
                   
                    
                    
                } while (temps ==1);
               
               
            }

            if (health <= 0) {
                
                do
                {
                   
                    animationTree.Set("parameters/Is_Alive/current",1);

                    motion.x=0;
                    temps = 0;
                   
                    
                    
                } while (temps ==1);
               
               
            }
            if(Input.IsActionJustPressed("ui_menu"))
            {
                GetTree().ChangeScene("res://Menu.tscn");
            }
            if (Input.IsActionJustPressed("ui_startAgain"))
            {
                 GetTree().ChangeScene("res://Level1.tscn");
            }
            if (Input.IsActionJustPressed("ui_1"))
            {
                 GetTree().ChangeScene("res://Level1.tscn");
            }
            if (Input.IsActionJustPressed("ui_2"))
            {
                 GetTree().ChangeScene("res://Level2.tscn");
            }

             if (Input.IsActionJustPressed("ui_debug"))
            {
                healthDebug.Visible= !healthDebug.Visible;
                hitConterDebug.Visible= !hitConterDebug.Visible;
                playerStateDebug.Visible= !playerStateDebug.Visible;
            }
            
            if (Input.IsActionJustPressed("ui_attack") &&  attack ==1) {
                
                do
                {
                    GD.Print($"Attack 1 {attack}");
                    animationTree.Set("parameters/Mouvement/current",2);
                    
                    motion.x=0;
                    temps = 0;
                   
                    
                } while (temps ==1);
                
               
            }

            if (Input.IsActionJustPressed("ui_jump")) {
                playerStateDebug.Text = $"State : Jump";
                motion.y = -JUMPFORCE;
                //GD.Print($"motion.y = {motion.y}");
                //Console.WriteLine($"motion.y = {motion.y}");
            }

            
        }
            

        if (!IsOnFloor()) {
            animationTree.Set("parameters/In_Air_State/current",1);
            if (motion.y < 0) {
                animationTree.Set("parameters/in_air/current",1);
                //animPlayer.Play("Jump");
            } else if (motion.y > 0) {
                animationTree.Set("parameters/in_air/current",0);
                //animPlayer.Play("Fall");
            }
        }

        motion = MoveAndSlide(motion, UP);
        
    } 
    public void _on_Hurtbox_area_entered(KinematicBody2D body)
    {
        hurt = 1;
        GD.Print("ENtered");
        
        
    }

    public void _on_Hurtbox_body_entered(KinematicBody2D body)
    {
        hurt ++;
        //GD.Print("ENtered");
        
        
    }
 
}