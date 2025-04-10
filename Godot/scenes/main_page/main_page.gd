extends Control


@onready var http: HTTPRequest = $HTTPRequest
@onready var request_timer: Timer = $RequestTimer

var url: String = GlobalData.backend_url


func _ready() -> void:
	make_request()


func _process(_delta: float) -> void:
	pass


func _on_request_timer_timeout() -> void:
	make_request()


func _on_http_request_request_completed(
	_result: int,
	_response_code: int,
	_headers: PackedStringArray,
	body: PackedByteArray
) -> void:
	var body_text: String = body.get_string_from_utf8()
	var body_data: Array = JSON.parse_string(body_text) if body_text != "" else []
	var new_label: Label = Label.new()
	new_label.text = body_text
	$Margin/TabContainer/ScrollContainer2/VBoxContainer.add_child(new_label)
	request_timer.start()


func make_request() -> void:
	var error: Error = http.request(url + "/journal")
	if error != OK:
		push_error("An error occurred in the HTTP request.")
