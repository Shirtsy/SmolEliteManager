extends Node


func get_backend_url() -> String:
	var url: String
	if OS.has_feature("web"):
		print_debug("Web version detected")
		url = "https://" + JavaScriptBridge.eval("window.location.host;")
	else:
		print_debug("Web version not detected")
		url = "https://localhost:7023"
	print_debug("URL: " + url)
	return url
