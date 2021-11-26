extends CanvasLayer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var score1


# Called when the node enters the scene tree for the first time.
func _ready():
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	#if Input.is_action_just_pressed("ui_debug"):
		
	score1= Global.score
	get_node("ScorePoints").text = str(score1)
	

