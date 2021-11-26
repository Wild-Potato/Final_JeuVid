extends KinematicBody2D


var is_moving_left = true

var gravity =  10 
var velocity = Vector2(0, 0)

var speed = 32 # pixels per second

func _ready():
	$AnimationPlayer.play("Move")

func _process(_delta):
	if $AnimationPlayer.current_animation == "Attack":
		return
	
	move_character()
	detect_turn_around()
		
func move_character():
	velocity.x = -speed if is_moving_left else speed
	velocity.y += gravity
	
	velocity = move_and_slide(velocity, Vector2.UP)

func detect_turn_around():
	if not $RayCast2D.is_colliding() and is_on_floor():
		is_moving_left = !is_moving_left
		scale.x = -scale.x

func hit():
	$AttackDetector.monitoring = true

func end_of_hit():
	$AttackDetector.monitoring = false
	
func start_walk():
	$AnimationPlayer.play("Move")



func _on_PlayerDetector_body_entered(body):
	$AnimationPlayer.play("Attack")




func _on_Hurtbox_area_entered(area):
	$AnimationPlayer.play("Die")
	
	Global.scoreplusplus()
	
	#queue_free()
