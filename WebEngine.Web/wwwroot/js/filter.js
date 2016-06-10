$(document).ready(function () {
	let elements = @Model.Items;

	for(let i = 0; i < elements.length; i++) {

		if(elements[i].FilterItemType == 'DropDownList'){

			$('#'+elements[i].PropertyId).click(function(){
				let value = $('#'+elements[i].PropertyId).val();

				var model = {
					Category: "SmartPhone",
					Properties: [
						{ "Id": elements[i].PropertyId, "Value": value, "IsRange" : false },
						//{ "Id": 4, "Value": "11", "IsRange" : false }
					],
				}

				var filter = JSON.stringify(model)

				$.ajax({
					type: 'POST',
					url: '/Product/Index',
					contentType: 'application/json; charset=utf-8',
					data: filter
				}).success(function (data) {
					alert("ok");
				}).fail(function (data) {
					alert("ошибка");
				});
			});
				
		} else if(elements[i].FilterItemType == 'Range'){

		}
	}
});