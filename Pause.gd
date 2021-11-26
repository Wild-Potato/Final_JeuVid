extends CanvasLayer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export(bool) var can_toggle_pause: bool =true

func _process(delta):
	if Input.is_action_just_pressed("ui_cancel"):
		if !get_tree().paused:
			pause()
		else:
			resume()
			

# Called when the node enters the scene tree for the first time.
func pause():
	if can_toggle_pause:
		$Background.visible = true
		$Continue.visible = true
		$Controls.visible = true
		$Menu.visible = true
		get_tree().set_deferred("paused", true)

func resume():
	if can_toggle_pause:
		$Background.visible = false
		$Continue.visible = false
		$Controls.visible = false
		$Menu.visible = false
		get_tree().set_deferred("paused", false)


func _on_Continue_pressed():
	resume()


func _on_Menu_pressed():
	get_tree().change_scene("res://Menu.tscn")


func _on_Controls_pressed():
	$ControlsImage.visible = true
	$ControlsBack.visible = true
	$Continue.visible = false
	$Controls.visible = false
	$Menu.visible = false


func _on_ControlsBack_pressed():
	$ControlsImage.visible = false
	$ControlsBack.visible = false
	$Continue.visible = true
	$Controls.visible = true
	$Menu.visible = true
