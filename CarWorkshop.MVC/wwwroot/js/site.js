
const RenderCarWorkshopServices = (data, container) => {
	container.empty();
	for (const item of data) {
		container.append(
			`
				<div class="card border-secondary mb-3" style="max-width: 18rem;">
					<div class="card-header">${item.cost}</div>
					<div class="card-body">
						<h5 class="card-title">${item.description}</h5>
					</div>
				</div>
				`)
	}
}

const LoadCarWorkshopServices = () => {
	const container = $("#services");

	const encodedName = container.data("encodedName");

	$.ajax({
		url: `/CarWorkshop/${encodedName}/CarWorkshopService`,
		type: "get",
		success: function (data) {
			if (!data.length) {
				container.html("There are no services in this car workshop")
			}
			else {
				RenderCarWorkshopServices(data, container)
			}
		},
		error: function () {
			toastr["danger"]("Something went wrong")
		}
	})
}