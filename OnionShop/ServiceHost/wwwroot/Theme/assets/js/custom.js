const cookieName = "cart-items";
function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id == id);
    if (currentProduct !== undefined) {
        products.find(x => x.id == id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitprice : price,
            picture,
            count
        }

        products.push(product)
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}
function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_items_count").text(products.length);
    const cartItemsWraper = $("#cart_items_wraper");
    cartItemsWraper.html('');
    products.forEach(x => {
        let product = ` <div class="single-cart-item">
                                                <a href="javascript:void(0)" class="remove-icon" onclick="removeItemFromCart(${x.id})">
                                                    <i class="ion-android-close"></i>
                                                </a>
                                                <div class="image">
                                                    <a href="single-product.html">
                                                        <img src="/ProductPictures/${x.picture}"
                                                             class="img-fluid" alt="">
                                                    </a>
                                                </div>
                                                <div class="content">
                                                    <p class="product-title">
                                                        <a href="single-product.html">محصول : ${x.name}</a>
                                                    </p>
                                                    <p class="count"><span>تعداد : ${x.count}
                                                    </span> قیمت واحد : ${x.unitprice}</p>
                                                </div>
                                            </div>`;
        cartItemsWraper.append(product);

    });
}

function removeItemFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalid, count) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    var productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = count;
    var product = products[productIndex];
    const newPrice = parseInt(product.unitprice) * parseInt(count);
    $(`#${totalid}`).text(newPrice);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}