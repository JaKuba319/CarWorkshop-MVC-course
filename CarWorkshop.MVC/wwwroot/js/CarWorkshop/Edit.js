$(document).ready(function () {
	$("#createCarWorkshopServiceModalForm").submit(function (event) {
		event.preventDefault();		
		$.ajax({
			url: $(this).attr('action'),
			type: $(this).attr('method'),
			data: $(this).serialize(),
			success: function (data) {
				toastr["success"]("Created carworkshop service")
			},
			error: function () {
				toastr["danger"]("Something went wrong")
			}
		})
	})
})
