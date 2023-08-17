
$(document).ready(function () {
    var cart = {
        init: function () {
            cart.loadData();
            cart.registerEvent();
        },
        registerEvent: function () {
            $('.js-addcart-detail').off('click').on('click', function (e) {
                var nameProduct = $(this).parent().parent().parent().parent().find('.js-name-detail').html();
                swal(nameProduct, "is added to cart !", "success");
                e.preventDefault();
                cart.addCartItem();
            });
            
            $('.deleteCartItem').off('click').on('click', function (e) {
                e.preventDefault();
                var productDetailID = parseInt($(this).data('id'));
                cart.deleteCartItem(productDetailID);
            });
            $('.cartItemQuantity').off('keyup').on('keyup', function () {
                var quantity = parseInt($(this).val());
                var productDetailID = parseInt($(this).data('id'));
                var price = parseFloat($(this).data('price'));
                if (isNaN(quantity) == true) {
                    $(this).val('1');
                    $('#amount_' + productDetailID).text(price);
                }
                cart.updateAll();
            });
        },
        execAddCartItem: function (productDetailID, productID, quantity) {
            $.ajax({
                url: '/Cart/Add',
                data: {
                    productDetailID: productDetailID,
                    productID: productID,
                    quantity: quantity
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        cart.loadData();
                    }
                }
            });
        },
        loadData: function () {
            var html = "";
            var html2 = "";
            var totalPrice = 0;
            var totalQuantity = 0;
            $.ajax({
                url: '/Cart/GetAll',
                type: 'GET',
                dataType: 'json',
                success: function (res) {
                    //var template = $('#tplCart').html();
                    
                    html = `<tr class="table_head">
                        <th class="column-1" > Product</th>
									<th class="column-2"></th>
                                    <th class="column-3">Size</th>
									<th class="column-3">Price</th>
									<th class="column-4">Quantity</th>
									<th class="column-5">Total</th>
								</tr>`;

                    var data = res.data;
                    
                    $.each(data, function (i, item) {
                        var currentPrice;
                        var priceBeforeDiscount;
                        console.log("item");
                        console.log(item);
                        if (item.productDetail.discount != 0) {
                            currentPrice = item.productDetail.unitSellingPrice * (1 - item.productDetail.discount / 100);
                            priceBeforeDiscount = item.productDetail.unitSellingPrice;
                        }
                        else
                            currentPrice = item.productDetail.unitSellingPrice;
                        html += `<tr class="table_row">
									<td class="column-1">
										<div class="how-itemcart1 deleteCartItem" data-id="${item.productDetail.productDetailId}">
											<img src="../../../ProductImages/${item.productDetail.image}" alt="IMG" data-id="${item.productDetail.productDetailId}" class="deleteCartItem" onclick="onclickDelete()">
										</div>
									</td>
									<td class="column-2">${item.product.name}</td>
                                    <td class="column-3">${item.sizeName}</td>`
                        if (priceBeforeDiscount != null) {
                            html += `<td class="column-3">$${currentPrice}<span class="priceBeforeDiscount">$${priceBeforeDiscount}</span> </td>`
                        }
                        else
                            html += `<td class="column-3">$ ${currentPrice}</td>`
                        html += `<td class="column-4">
										<div class="form-outline">
											<input type="number" min="1" data-productid="${item.productID}" data-id="${item.productDetail.productDetailId}" data-price="${item.productDetail.unitSellingPrice}" class="form-control cartItemQuantity" value="${item.quantity}"/>
										</div>
									</td>
									<td class="column-5" id="amount_${item.productDetail.productDetailId}">$ ${item.quantity * currentPrice}</td>
								</tr>`	
                                	
                        html2 += `<li class="header-cart-item flex-w flex-t m-b-12">
						<div class="header-cart-item-img deleteCartItem" data-id="${item.productDetail.productDetailId}">
							<img src="../../../ProductImages/${item.productDetail.image}" alt="IMG">
						</div>

						<div class="header-cart-item-txt p-t-8">
							<a href="#" class="header-cart-item-name m-b-18 hov-cl1 trans-04">
								${item.product.name}
							</a>

							<span class="header-cart-item-info">
								${item.quantity} x $${currentPrice}
							</span>
						</div>
					</li>`
                        totalPrice += item.quantity * currentPrice;
                        totalQuantity += item.quantity;
                    });

                    $('#cartBody').html(html);
                    $('#cartBody2').html(html2);
                    $('#asideCartTotalPrice').text("Total: $" + totalPrice);
                    $('.cartTotalPrice').text("$" + totalPrice);
                    document.querySelector(".js-show-cart").setAttribute("data-notify", totalQuantity);
                    cart.registerEvent();     
                },
                error: function (xhr) {
                    alert(xhr.responsesText)
                }
            });
        },
        addCartItem: function () {
            var colorID = parseInt($('.productDetailColor option:selected').data('id'));
            var sizeID = parseInt($('.productDetailSize option:selected').data('id'));
            var productID = parseInt($('#btnAddToCart').data('id'));
            var quantity = $('#numProductToOrder').val();
            var url = "https://localhost:7079/api/productapi/GetProductDetailID?productid=" + productID
                + "&colorid=" + colorID + "&sizeid=" + sizeID;
            var productDetailID;
            
            $.ajax({
                type: "GET",
                url: url,
                dataType: "json",
                success: function (res) {
                    
                    productDetailID = res;
                    
                    cart.execAddCartItem(productDetailID, productID, quantity);
                }
            });
        },
        deleteCartItem: function (productDetailID) {
            $.ajax({
                url: '/Cart/DeleteItem',
                data: {
                    productDetailID: productDetailID
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        cart.loadData();
                    }
                }
            });
        },
        updateAll: function () {
            var cartList = [];
            $.each($('.cartItemQuantity'), function (i, item) {
                cartList.push({
                    ProductDetailID: $(item).data('id'),
                    ProductID: $(item).data('productid'),
                    Quantity: $(item).val()
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: {
                    cartData: JSON.stringify(cartList)
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        cart.loadData();
                    }
                }
            });
        }
    }
    cart.init();

    /*==================================================================
    [ User change product color ]*/
    function changeUnitSellingPrice(productID, colorID) {
        var unitSellingPrice = 0;
        $.ajax({
            url: '/api/productapi/GetProductDetailPrice',
            data: {
                productID: productID,
                colorID: colorID
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                
                
                $('.productDetailPrice').text("$" + response.currentPrice);
                if (response.discount != '') {
                    $('.priceBeforeDiscount').text(response.priceBeforeDiscount);
                    $('.discount').text(response.discount);
                }
                else {
                    $('.priceBeforeDiscount').text('');
                    $('.discount').text('');
                }
                
            }
        });
        
    }
    function changeSize(productID, colorID) {
        
        $.ajax({
            url: '/api/productapi/GetProductDetailSize',
            data: {
                productID: productID,
                colorID: colorID
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var html = "";
                $.each(response, function (i, item) {
                    html += `<option data-id="${item.sizeId}">${item.name}</option>`;
                });
                $(".productDetailSize").html(html);
            }
        });
    }
    // Khi nguoi dung thay doi mau thi thay doi gia tien, anh, size
    $('.productDetailColor').change(function () {

        var productID = $(this).find(":selected").data('productid');
        var colorID = $(this).find(":selected").data('id');
        var imgTag = document.querySelectorAll("li[role=presentation] img");
        var divImg = document.querySelectorAll(".item-slick3.slick-slide");
        var imgTag2 = document.querySelectorAll(".item-slick3.slick-slide div img");
        var aTag = document.querySelectorAll(".item-slick3.slick-slide div a");
        changeUnitSellingPrice(productID, colorID);
        changeSize(productID, colorID);
        var html = ``;
        $.ajax({
            url: '/api/productapi/GetProductDetailImage',
            data: {
                productID: productID,
                colorID: colorID
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                
                $.each(response, function (i, item) {
                    var url = "../../ProductImages/" + item.fileName;
                    imgTag[i].src = url;
                    imgTag2[i].src = url;
                    aTag[i].setAttribute("href", url);
                    divImg[i].setAttribute("data-thumb", url);
                });
            }
        });

    });

    /*==================================================================
    [ User hover on product card ]*/
    $(".block2 .row").hide();
    $(".block2").hover(
        function () {
            $(this).children(".row").show();
            $(this).children(".block2-txt").children(".block2-txt-child1").children("a").hide();
        },
        function () {
            $(this).children(".row").hide();
            $(this).children(".block2-txt").children(".block2-txt-child1").children("a").show();
        }
    );

    $(".colorImage").hover(
        function () {
            var productDetailID = $(this).data("productdetailid");
            var productID = $(this).data("productid");
            $.ajax({
                url: '/api/productapi/GetProductDetail',
                data: {
                    productDetailID: productDetailID
                },
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    document.querySelector("#mainImage_" + productID).setAttribute("src", "/ProductImages/" + response.image);
                    if (response.discount != 0) {
                        var priceBeforeDiscount = "$" + response.unitSellingPrice;
                        var discount = response.discount + "% off";
                        //res = JSON.parse(response);
                        var currentPrice = response.unitSellingPrice * (1 - response.discount / 100);
                        
                        console.log(currentPrice);
                        $('#currentPrice_' + productID).text('$' + currentPrice);
                        $('#priceBeforeDiscount_' + productID).text(priceBeforeDiscount);
                        $('#discount_' + productID).text(discount);
                    }
                    else {
                        var currentPrice = response.unitSellingPrice;
                        $('#currentPrice_' + productID).text('$' + currentPrice);
                        $('#priceBeforeDiscount_' + productID).text('');
                        $('#discount_' + productID).text('');
                    }
                }
            });
        },
        function () {
            
        }
    );
});
