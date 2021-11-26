extends Node2D

var Enemy = preload("res://Ressources/Enemie/EnemieSlime.tscn")

# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	Global.scorereset()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(Global.score == 20):
		get_tree().change_scene("res://Level2.tscn")
		
	


func _on_SpawnTimer_timeout():
	print("Click")
	var enemy = Enemy.instance()
	var enemy2 = Enemy.instance()
	add_child(enemy)
	enemy.position = $Spawn.position
	add_child(enemy2)
	enemy2.position = $Spawn2.position
