var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    addItem: function (productId) {
        $.ajax({
            url: "ShoppingCart/Add",
            data: {
                productId:productId
            },
            type: "POST",
            dataType: "json",
            success: function (res) {
                if (res.status) {
                    alert("Them sp thanh cong");
                }
            }
        });
    },
    deleteItem: function (productId) {
        $.ajax({
            url: "ShoppingCart/DeleteItem",
            data: {
                productId: productId
            },
            type: "POST",
            dataType: "json",
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    registerEvent: function () {
        $("#btnAddToCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $("#btnDelete").off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $("#btnContinue").off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });
        $("#btnDeleteAll").off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: "ShoppingCart/DeleteAll",
                type: "POST",
                dataType: "json",
                success: function (res) {
                    if (res.status) {
                        cart.loadData();
                    }
                }
            });
        });
        $("#btnCheckOut").off('click').on('click', function (e) {
            e.preventDefault();
            $('#checkOut').removeClass("hidden");
        });
        $("#chkUseLoginInfo").off('click').on('click', function (e) {
            if ($(this).prop("checked")) {
                $.ajax({
                    url: "ShoppingCart/GetUser",
                    type: "POST",
                    dataType: "json",
                    success: function (res) {
                        if (res.status) {
                            var user = res.data;
                            $('#txtName').val(user.FullName);
                            $('#txtAddress').val(user.Address);
                            $('#txtEmail').val(user.Email);
                            $('#txtPhone').val(user.PhoneNumber);
                        }
                    }
                });
            } else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
            
        });
        
        $(".Quantity").off('keyup').on('keyup', function (e) {
            e.preventDefault();
            var quantity = parseInt($(this).val());
            if (isNaN(quantity) === false) {
                var price = parseFloat($(this).data('price'));
                var productId = parseInt($(this).data('id'));
                var amount = quantity * price;
                $('#amount_' + productId).text(amount);
            }
            else {
                $('#amount_' + productId).text('0');
            }
            $('#lblTotalOrder').text(cart.getTotalOrder());
            cart.updateAll();
        });
    },
    updateAll: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: "ShoppingCart/Update",
            type: "POST",
            dataType: "json",
            data: {
                cartData: JSON.stringify(cartData)
            },
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                    console.log("Update OK");
                }
            }
        });
    },
    getTotalOrder: function () {
        var listTextBox = $(".Quantity");
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    },
    loadData: function () {
        $.ajax({
            url: "ShoppingCart/GetAll",
            type: "GET",
            dataType: "json",
            success: function (res) {
                if (res.status) {
                    var html = '';
                    var template = $('#tplCart').html();
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            Quantity: item.Quantity,
                            Amount: item.Quantity * item.Product.Price
                        });
                    });
                    if (html === '') {
                        $('#cartContent').html('khong co san pham nao trong gio');
                    }
                    else {
                        $("#cartBody").html(html);
                        $('#lblTotalOrder').text(cart.getTotalOrder());
                    }
                    cart.registerEvent();
                }
            }
        });
    }
}
cart.init();