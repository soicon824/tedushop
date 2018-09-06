var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function() {
        $("#txtKeyword").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    data: {
                        name: request.term
                    },
                    error(xhr, status, error) {
                        //response(res.data);
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            minLength: 2,
            select: function (event, ui) {
                //$("#txtKeyword").val(ui.item.data);
                //log("Selected: " + ui.item.value + " aka " + ui.item.id);
            }
        });
    }
}
common.init();