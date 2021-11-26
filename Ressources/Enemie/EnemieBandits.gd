extends KinematicBody2D
var is_moving_left = true

var gravity =  10 
var velocity = Vector2(0, 0)
var health = 2
onready var player = get_node("/root/World/Player")
var speed = 32 # pixels per second

func _ready():
	$AnimationPlayer.play("Moving")

func _process(_delta):
	if $AnimationPlayer.current_animation == "Attack":
		return
	if $AnimationPlayer.current_animation == "Damaged":
		return	
		
	if health <= 0:
		$AnimationPlayer.play("Dead")
		Global.scoreplusplus()
		
	
	if $AnimationPlayer.current_animation == "Dead":
		queue_free()
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
	$AnimationPlayer.play("Moving")


func _on_PlayerDetector_body_entered(body):
	$AnimationPlayer.play("Attack")
	print("Isattacking")


func _on_Hurtbox_area_entered(area):
	health -= 1
	print(health)
	$AnimationPlayer.play("Damaged")
	
	#queue_free()
