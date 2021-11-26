extends Node2D

var Enemy = preload("res://Ressources/Enemie/EnemieSlime.tscn")
# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_SpawnTimer_timeout():
	var enemy = Enemy.instance()
	add_child(enemy)
	enemy.position = $Spawn.position
