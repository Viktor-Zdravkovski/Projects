document.addEventListener("DOMContentLoaded", function () {
    const token = sessionStorage.getItem('userToken');
    if (!token) {
        console.error('No token found in localStorage.');
        window.location.href = 'http://127.0.0.1:5503/index.html#/login';
        return;
    }

    fetch('https://localhost:7244/api/Category/GetAllCategories', {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token.trim()}`,
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            console.log("Response status:", response.status);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(categories => {
            renderCategories(categories);
        })
        .catch(error => {
            console.error('Error fetching categories:', error);
            if (error.message.includes('401')) {
                alert('Session expired or unauthorized access. Please log in again.');
                window.location.href = 'http://127.0.0.1:5503/index.html#/login';
            } else {
                alert('Failed to fetch categories. Please try again later.');
            }
        });

    function renderCategories(categories) {
        const categoryContainer = $('#category-container');
        categoryContainer.empty(); // Clear previous content

        categories.forEach(category => {
            const formattedCategoryName = category.name.toLowerCase().replace(/\s+/g, '-');
            const categoryElement = `
                <div class="col-lg-4 mb-3 d-flex align-items-stretch text-center">
                    <div class="card shadow tilt">
                        <img src="${category.imageUrl}" class="card-img-top" alt="${category.name}">
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title">${category.name}</h5>
                            <hr>
                            <p class="card-text mb-4">${category.description}</p>
                            <a href="#" class="btn mt-auto align-self-center mb-3 explore-btn" data-category-id="${category.id}" data-category-name="${formattedCategoryName}">Explore</a>
                        </div>
                    </div>
                </div>
            `;
            categoryContainer.append(categoryElement);
        });

        document.querySelectorAll('.explore-btn').forEach(button => {
            button.addEventListener('click', function (event) {
                event.preventDefault();
                const categoryId = this.dataset.categoryId;
                const categoryName = this.dataset.categoryName;
                window.location.hash = `/category/${categoryName}`;
                fetchProductsByCategory(categoryId);
            });
        });
    }

    function fetchProductsByCategory(categoryId) {
        fetch(`https://localhost:7244/api/Category/GetCategoryById/${categoryId}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token.trim()}`,
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(category => {
                renderProducts(category.productDtos);
                console.log("Category Products:", category.productDtos);

                document.getElementById('category-title').textContent = category.name;

                document.getElementById('categories').style.display = 'none';
                document.getElementById('category-details').style.display = 'block';
            })
            .catch(error => {
                console.error('Error fetching products:', error);
            });
    }

    function renderProducts(products) {
        const productContainer = document.getElementById('products');
        productContainer.innerHTML = '';

        if (!Array.isArray(products) || products.length === 0) {
            productContainer.innerHTML = `<p>No products found in this category.</p>`;
            return;
        }

        products.forEach(product => {
            const onSale = product.onSale;
            const priceHTML = onSale ? `<span class="original-price" style="text-decoration: line-through;">$${product.price}</span> <span class="sale-price">$${product.salePrice}</span>` : `<span>$${product.price}</span>`;

            const productHTML = `
                <div class="col-lg-4 mb-3 d-flex align-items-stretch text-center">
                    <div class="card shadow tilt">
                        <img src="${product.image}" class="card-img-top" alt="${product.name}">
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title">${product.name}</h5>
                            <p class="card-text">${product.description}</p>
                            <p class="card-text">Price: ${priceHTML}</p>
                            <button class="add-to-cart btn mt-auto align-self-center mb-3" data-product-id="${product.id}">Add to Cart</button>
                        </div>
                    </div>
                </div>
            `;
            productContainer.innerHTML += productHTML;
        });

        const addToCartButtons = productContainer.querySelectorAll('.add-to-cart');
        addToCartButtons.forEach(button => {
            button.addEventListener('click', function (event) {
                const productId = event.target.getAttribute('data-product-id');
                console.log(`Added product with ID: ${productId} to the cart`);

                addToCart(productId);
            });
        });
    }

    window.onhashchange = function () {
        const hash = window.location.hash;
        if (hash.startsWith("#/category/")) {
            document.getElementById('categories').style.display = 'none';
            document.getElementById('category-details').style.display = 'block';
        } else {
            document.getElementById('categories').style.display = 'block';
            document.getElementById('category-details').style.display = 'none';
        }
    };

    window.onhashchange();
});

document.getElementById('contact-us').addEventListener('click', function (event) {
    event.preventDefault();
    window.location.href = "http://127.0.0.1:5503/index.html#/contact";
});

async function addToCart(productId) {
    try {
        const authToken = sessionStorage.getItem('userToken');
        if (!authToken) {
            alert('You need to be logged in to add items to the cart.');
            return;
        }

        const response = await fetch(`https://localhost:7244/api/Product/GetProductById/${productId}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${authToken}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch product details');
        }

        const item = await response.json();
        console.log(item);

        if (!item) {
            console.log('Product not found');
            return;
        }

        const price = item.onSale && item.salePrice ? item.salePrice : item.price;

        const cartItem = {
            UserId: sessionStorage.getItem('userId'),
            id: item.id,
            name: item.name,
            price: price,
            description: item.description,
            image: item.image,
            quantity: 1
        };

        const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];

        const existingItemIndex = cartItems.findIndex(cartItem => cartItem.id === item.id);
        console.log("this is existing item" ,existingItemIndex);

        if (existingItemIndex !== -1) {
            cartItems[existingItemIndex].quantity += 1;
        } else {
            cartItems.push(cartItem);
        }

        sessionStorage.setItem('cartItems', JSON.stringify(cartItems));

        console.log("Item added to cart:", cartItem);
        console.log("Current cart items:", cartItems);

        updateCartBadge(cartItems.length);
        renderCart();

        calculateCartSummary();

    } catch (error) {
        console.error("Error adding item to cart:", error);
    }
}
//====================================================================================== EXPERIMENT======================================================================================


function updateCartBadge(cartLength) {
    const cartBadge = document.getElementById('cart-badge');
    if (cartBadge) {
        cartBadge.textContent = cartLength;
    }
}

function renderCart() {
    const cartContainer = document.getElementById('cart-items');
    const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];

    if (cartItems.length === 0) {
        cartContainer.innerHTML = '<p>Your cart is empty.</p>';
        return;
    }

    cartContainer.innerHTML = '';

    cartItems.forEach((item, index) => {
        const itemHTML = `
            <div class="cart-item" data-index="${index}">
                <img src="${item.image}" alt="${item.name}" class="cart-item-image">
                <div class="cart-item-details">
                    <p>${item.name}</p>
                    <p>Price: $${item.price}</p>
                    <p>Quantity: ${item.quantity}</p>
                </div>
                <button class="remove-from-cart">Remove</button>
            </div>
        `;
        cartContainer.innerHTML += itemHTML;
    });

    const removeButtons = cartContainer.querySelectorAll('.remove-from-cart');
    removeButtons.forEach((button, index) => {
        button.addEventListener('click', function () {
            removeFromCartUI(index);
        });
    });
}
function removeFromCartUI(index) {
    const cartContainer = document.getElementById('cart-items');
    let cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];

    cartItems.splice(index, 1);

    sessionStorage.setItem('cartItems', JSON.stringify(cartItems));

    renderCart();

    calculateCartSummary();
}

function renderCart() {
    const cartContainer = document.getElementById('cart-items');
    let cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];

    if (cartItems.length === 0) {
        cartContainer.innerHTML = '<p>Your cart is empty.</p>';
        return;
    }

    cartContainer.innerHTML = '';

    cartItems.forEach((item, index) => {
        const itemHTML = `
            <div class="cart-item" data-index="${index}">
                <img src="${item.image}" alt="${item.name}" class="cart-item-image">
                <div class="cart-item-details">
                    <p>${item.name}</p>
                    <p>Price: $${item.price}</p>
                    <p>Quantity: ${item.quantity}</p>
                </div>
                <button class="remove-from-cart">Remove</button>
            </div>
        `;
        cartContainer.innerHTML += itemHTML;
    });

    const removeButtons = cartContainer.querySelectorAll('.remove-from-cart');
    removeButtons.forEach((button, index) => {
        button.addEventListener('click', function () {
            removeFromCartUI(index);
        });
    });

    console.log('Updated cart items:', cartItems);

}

window.onload = function() {
    renderCart();  
    calculateCartSummary();
};

function calculateCartSummary() {
    const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];

    if (cartItems.length === 0) {
        document.getElementById('subtotal').textContent = '0.00 EUR';
        document.getElementById('total').textContent = '0.00 EUR';
        return;
    }

    let subtotal = 0;

    cartItems.forEach(item => {
        subtotal += item.price * item.quantity;
    });

    document.getElementById('subtotal').textContent = subtotal.toFixed(2) + " EUR";
    document.getElementById('total').textContent = subtotal.toFixed(2) + " EUR";
}

function updateCartItemInStorage() {
    let cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];

    sessionStorage.setItem('cartItems', JSON.stringify(cartItems));

    calculateCartSummary();
}

function placeOrder() {
    const userId = sessionStorage.getItem('userId');
    if (!userId) {
        alert('User is not authenticated!');
        return;
    }

    const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];
    if (cartItems.length === 0) {
        alert('Your cart is empty!');
        return;
    }

    const productIds = cartItems.map(item => item.id);

    fetch('https://localhost:7244/api/Order/AddOrder', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${sessionStorage.getItem('userToken')}`
        },
        body: JSON.stringify({ 
            userId: parseInt(userId),
            productIds: productIds
        })
        
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(error => {
                    console.error('Server Error:', error);
                    throw new Error(error.message || 'Failed to place the order');
                });
            }
            return response.json();
        })
        .then(data => {
            console.log('Order placed successfully:', data);
            alert('Your order has been placed successfully!');

            sessionStorage.removeItem('cartItems');
            renderCart();
        })
        .catch(error => {
            console.error('Error placing order:', error);
            alert('An error occurred while placing your order.');
        });
}


// ===============================================================================
function getLoggedInUserId() {
    return sessionStorage.getItem('userId');
}

document.addEventListener('DOMContentLoaded', function () {
    renderCart();
});

function removeFromCart(orderId) {
    console.log("Removing item with orderId:", orderId);

    if (!orderId) {
        alert('Invalid orderId');
        return;
    }

    const userToken = sessionStorage.getItem('userToken');

    if (!userToken) {
        alert('User is not authenticated.');
        return;
    }

    fetch(`https://localhost:7244/api/Order/DeleteOrderById/${orderId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${userToken}`,
        },
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(errorText => {
                    console.error('Error response text:', errorText);
                    alert(`Failed to remove order: ${errorText || 'Unknown error'}`);
                });
            }

            return response.json();
        })
        .then(data => {
            if (data && data.success) {
                alert('Order removed successfully');

                let cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];

                cartItems = cartItems.filter(item => item.id !== orderId);

                sessionStorage.setItem('cartItems', JSON.stringify(cartItems));

                renderCart();
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while trying to remove the order.');
        });
}




// =========================================================== DONT TOUCH ANYTHING FROM ABOVE IF SOMETHING DOESNT WORK =========================================
// =========================================================== CHANGE THE CODE BELLOW ==========================================================================

function sendContactNotification(event) {
    event.preventDefault(); 

    const token = sessionStorage.getItem('userToken'); 
    const email = document.getElementById('email-contact').value; 
    const message = document.getElementById('message').value;
    if (!email || !message) {
        document.getElementById('response-message').textContent = "Please fill in both fields.";
        return;
    }

    const requestBody = {
        Email: email,
        Description: message 
    };

    fetch('https://localhost:7244/api/Contact/ContactUs', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`,
        },
        body: JSON.stringify(requestBody),
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => {
                    throw new Error(text);
                });
            }

            return response.text().then(text => {
                try {
                    return JSON.parse(text);
                } catch {
                    return text;
                }
            });
        })
        .then(data => {
            if (typeof data === 'string') {
                document.getElementById('response-message').textContent = data;
            } else {
                document.getElementById('response-message').textContent = "Your message has been sent successfully!";
                document.querySelector('.contact-form').reset();
            }
        })
        .catch(error => {
            console.error('Error sending contact message:', error);
            document.getElementById('response-message').textContent = error.message || "There was an error sending your message. Please try again later.";
        });
}



document.getElementById('send').addEventListener('click', sendContactNotification);

// =========================================================UNCOMMENT BELOW==================================================================================================


function fetchOnSaleProducts() {
    const token = sessionStorage.getItem('userToken');

    fetch('https://localhost:7244/api/Product/GetOnSaleProducts', {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token ? token.trim() : ""}`,
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(onSaleProducts => {
            renderOnSaleProducts(onSaleProducts);
        })
        .catch(error => {
            console.error('Error fetching on-sale products:', error);
        });
}

function renderOnSaleProducts(onSaleProducts) {
    const salesContainer = $('#products');
    salesContainer.empty();

    if (onSaleProducts.length === 0) {
        salesContainer.append('<p>No products on sale currently.</p>');
    } else {
        onSaleProducts.forEach(product => {
            const productHTML = `
                <div class="product-card">
                    <div class="product-image-container">
                        ${product.image ? `<img src="${product.image}" alt="${product.name}" class="product-image" />` : ""}
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">${product.name}</h3>
                        <p class="product-description">${product.description || "N/A"}</p>
                        <p class="product-price">Original Price: $${product.price}</p>
                        <p class="product-sale-price">Sale Price: $${product.salePrice}</p>
                        <p class="product-quantity">Quantity: ${product.quantity}</p>
                        <button class="buy-button">Buy Now</button>
                    </div>
                </div>`;
            salesContainer.append(productHTML);
        });
    }
}

document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('sale-product-link').addEventListener('click', function (event) {
        event.preventDefault();
        // window.location.href = 'http://127.0.0.1:5503/index.html#/salespage';
        history.pushState(null, '', '/#salespage');
        location.hash = '#salespage';

        fetchOnSaleProducts();
    });
});

// =====================================================UNCOMENT ABOVE=====================================================================
// ==========================================================================================================================

$(document).ready(function () {
    $('#category-toggle').on('click', function (e) {
        e.preventDefault();
        const token = sessionStorage.getItem('userToken');
        console.log(token);

        $.ajax({
            url: 'https://localhost:7244/api/Category/GetAllCategories',
            type: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            success: function (categories) {
                $('#category-menu').empty();
                categories.forEach(function (category) {
                    $('#category-menu').append(`<li><a class="dropdown-item" href="#">${category.name}</a></li>`);
                });
            },
            error: function (xhr) {
                console.error('Error fetching categories:', xhr);
                $('#category-menu').empty().append('<li class="dropdown-item">Error loading categories</li>');
            }
        });
    });
});

function attachCategoryClickEvents() {
    document.querySelectorAll('#category-menu a').forEach(function (link) {
        link.addEventListener('click', function (event) {
            event.preventDefault();
            const category = this.getAttribute('data-category');
            showCategoryProducts(category, false, false);
            updateHash(`#/category/${category}`);
        });
    });
}


document.addEventListener('DOMContentLoaded', () => {
    const categoryItems = document.querySelectorAll('.dropdown-item');
    categoryItems.forEach(item => {
        item.addEventListener('click', function (event) {
            event.preventDefault();
            const category = this.getAttribute('data-category');
            loadCategoryProducts(category);
        });
    });

    function loadCategoryProducts(category) {
        fetch(`/api/products?category=${category}`)
            .then(response => response.json())
            .then(products => {
                if (products.length > 0) {
                    updateCarousel(products);
                } else {
                    showNoProductsMessage();
                }
            })
            .catch(error => {
                console.error('Error fetching products:', error);
                showErrorMessage();
            });
    }

    function updateCarousel(products) {
        const productsContainer = document.getElementById('products');
        productsContainer.innerHTML = '';

        products.forEach(product => {
            const productDiv = document.createElement('div');
            productDiv.classList.add('product-item');
            productDiv.innerHTML = `
                <img src="${product.imageUrl}" alt="${product.name}">
                <h4>${product.name}</h4>
                <p>${product.description}</p>
                <button class="add-to-cart" data-product-id="${product.id}">Add to Cart</button>
            `;
            productsContainer.appendChild(productDiv);
        });
    }

    function showNoProductsMessage() {
        const productsContainer = document.getElementById('products');
        productsContainer.innerHTML = '<p>No products found in this category.</p>';
    }

    function showErrorMessage() {
        const productsContainer = document.getElementById('products');
        productsContainer.innerHTML = '<p>There was an error loading products. Please try again later.</p>';
    }
});

document.addEventListener('DOMContentLoaded', function() {
    calculateCartSummary();
});