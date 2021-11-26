extends Node2D

var Enemy = preload("res://Ressources/Enemie/EnemieBandits.tscn")
var switcher = 0
# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(Global.score == 2):
		$WinningScreen.on()


func _on_SpawnTimer_timeout():
	print("Click")
	var enemy = Enemy.instance()
	var enemy2 = Enemy.instance()
	add_child(enemy)
	add_child(enemy2)
	if(switcher == 0):
		switcher = 1
		enemy.position = $Spawn.position
	if(switcher == 1):
		switcher = 0
		enemy2.position = $Spawn2.position
