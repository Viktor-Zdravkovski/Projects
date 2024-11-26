$(document).ready(function() {
    $('#sign-up-form').addClass('hidden');
    $('#profile-show').hide();
    $('#login-form').removeClass('hidden');
    $('#category-menu').hide();

    if (sessionStorage.getItem('userToken')) {
        const loggedInUser = JSON.parse(sessionStorage.getItem('loggedInUser'));
        $('#login-signup-link').hide();
        $('#profile-link').removeClass('hidden');
        $('#user-name').text(loggedInUser.name);
    } else {
        $('#profile-link').addClass('hidden');
        $('#login-signup-link').show();
    }

    $('#profile-link a').click(function(event) {
        event.preventDefault();
        const userToken = sessionStorage.getItem('userToken');

        if (userToken) {
            window.location.href = "http://127.0.0.1:5503/index.html#/profile";
            $('.profile-show').css('display', 'block');
        } else {
            window.location.href = "http://127.0.0.1:5503/index.html#/login";
        }
    });

    $('#login-btn').click(function(event) {
        event.preventDefault();
        const email = $('#login-email').val().trim();
        const password = $('#password').val().trim();
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!email || !emailRegex.test(email)) {
            showAlert("Please enter a valid email address.", "red");
            return;
        }

        if (!password) {
            showAlert("Please enter your password.", "red");
            return;
        }

        const logInUserDto = { Email: email, Password: password };

        $.ajax({
            url: 'https://localhost:7244/api/User/Login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(logInUserDto),
            success: function(response) {
                console.log(response);
                const UserId = response.userId;
                console.log(UserId);
                const userName = response.username;
                sessionStorage.setItem('userToken', response.token);
                sessionStorage.setItem('loggedInUser', JSON.stringify({ email: email, name: userName }));
                sessionStorage.setItem('userId', UserId); 
                $('#login-signup-link').hide();
                $('#profile-link').removeClass('hidden');
                $('#user-name').text(userName);
                showAlert("Login successful!", "green");
            
                fetchCategories();
                
                setTimeout(function() {
                    window.location.href = "http://127.0.0.1:5503/index.html#/home";
                }, 2000);
            },
            error: function(xhr) {
                if (xhr.status === 400) {
                    showAlert("Invalid email or password.", "red");
                } else {
                    showAlert("An unexpected error occurred.", "red");
                }
            }
        });
    });

    $('#logout-btn').click(function() {
        sessionStorage.removeItem('userToken');
        sessionStorage.removeItem('loggedInUser');
        sessionStorage.removeItem('userId');
        $('#profile-link').addClass('hidden');
        $('#login-signup-link').show();
        $('#user-name').text('');
        showAlert("Logout successful!", "green");

        setTimeout(function() {
            window.location.href = "http://127.0.0.1:5503/index.html#/home";
        }, 2000);
    });

    $('#sign-up-form').on('submit', async function(e) {
        e.preventDefault();
        const requestBody = {
            FirstName: $('#first-name').val(),
            LastName: $('#last-name').val(),
            Username: $('#username').val(),
            Password: $('#sign-up-password').val(),
            Email: $('#email').val(),
            PhoneNumber: $('#phone-number').val(),
            ConfirmedPassword: $('#confirmed-password').val()
        };

        try {
            const response = await fetch('https://localhost:7244/api/User/Register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(requestBody)
            });

            const result = await response.text();
            if (response.ok) {
                alert('Registration successful! ' + result);
                $('#sign-up-form').addClass('hidden');
                $('#login-form').removeClass('hidden');
            } else {
                alert(`Registration failed: ${result}`);
            }
        } catch (error) {
            console.error('Error during registration:', error);
            alert('Error during registration. Please try again.');
        }
    });

    $('#switch-to-sign-up-btn').click(() => {
        $('#login-form').addClass('hidden');
        $('#sign-up-form').removeClass('hidden');
    });

    $('#back-to-login-btn').click(() => {
        $('#sign-up-form').addClass('hidden');
        $('#login-form').removeClass('hidden');
    });

    function showAlert(message, color) {
        $('.alert').text(message).css('color', color).show();
        setTimeout(() => {
            $('.alert').hide();
        }, 3000);
    }

    function fetchCategories() {
        const token = sessionStorage.getItem('userToken');

        if (!token) {
            console.error("User token is missing.");
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
        });
    }

    function renderCategories(categories) {
        const categoryMenu = $('#category-menu');
        categoryMenu.empty();

        if (categories.length === 0) {
            categoryMenu.append('<li class="dropdown-item">No categories available.</li>');
        } else {
            categories.forEach(category => {
                categoryMenu.append(`<li><a class="dropdown-item" href="#/category/${category.name}">${category.name}</a></li>`);
            });
        }

        categoryMenu.show();
    }

    $('#category-toggle').on('click', function(e) {
        e.preventDefault();
        $('#category-menu').toggle();
        fetchCategories();
    });

    window.onhashchange = function () {
        const hash = window.location.hash;

        if (hash.startsWith("#/home")) {
            fetchCategories();
            $('#categories').show();
            $('#category-details').hide();
        } else if (hash.startsWith("#/contact")) {
            fetchCategories();
            $('#categories').show();
            $('#category-details').hide();
        } else if (hash.startsWith("#/category/")) {
            const categoryName = hash.split('/')[2];
        }
    };

    window.onhashchange();
});