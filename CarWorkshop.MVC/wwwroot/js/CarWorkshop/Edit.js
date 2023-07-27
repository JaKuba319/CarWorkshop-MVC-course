$(document).ready(function () {
	LoadCarWorkshopServices()

	$("#createCarWorkshopServiceModalForm").submit(function (event) {
		event.preventDefault();
		$.ajax({
			url: $(this).attr('action'),
			type: $(this).attr('method'),
			data: $(this).serialize(),
			success: function (data) {
				toastr["success"]("Created carworkshop service")
				LoadCarWorkshopServices()
			},
			error: function () {
				toastr["error"]("Something went wrong")
			}
		})
	});
});
