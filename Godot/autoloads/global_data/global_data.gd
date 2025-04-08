extends Node


func get_backend_url() -> String:
	if OS.is_debug_build():
		return "https://localhost:7023/weatherforecast"
	else:
		return "/weatherforecast"
