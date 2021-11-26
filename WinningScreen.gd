extends CanvasLayer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
func on():
	$BlackScreen.visible = true
	$Button.visible = true
	$Win.visible = true
	
func off():
	$BlackScreen.visible = false
	$Button.visible = false
	$Win.visible = false


func _on_Button_pressed():
	$BlackScreen.visible = false
	$Button.visible = false
	$Win.visible = false
	get_tree().change_scene("res://Menu.tscn")
