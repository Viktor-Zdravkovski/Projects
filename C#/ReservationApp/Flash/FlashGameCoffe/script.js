function setActiveLink(link) {
    let navLinks = document.querySelectorAll('.navbar-nav .nav-link');
    navLinks.forEach((item) => {
        item.classList.remove('active');
    });

    link.classList.add('active');
}

// ==================
// CSS FOR SHOWING/HIDING PAGES
// Function to hide all pages
function hideAllPages() {
    document.getElementById("homePage").style.display = "none";
    document.getElementById("menuPage").style.display = "none";
    // document.getElementById("contactUsPage").style.display = "none"; // uncomment if u want contactuspage
    document.getElementById("galleryPage").style.display = "none";
    document.getElementById("reservationSection").style.display = "none";
    document.getElementById("registerPage").style.display = "none";
    document.getElementById("loginPage").style.display = "none";
    document.getElementById("admin-panel").style.display = "none";
}


function showHomePage() {
    hideAllPages();
    document.getElementById("homePage").style.display = "block";
}

function showMenuPage() {
    hideAllPages();
    document.getElementById("menuPage").style.display = "block";
}

// uncomment if u want contactuspage
// function showContactUsPage() {
//     hideAllPages();
//     document.getElementById("contactUsPage").style.display = "block";
// }

function showGalleryPage() {
    hideAllPages();
    document.getElementById("galleryPage").style.display = "block";
}

function showRegisterPage() {
    hideAllPages();
    document.getElementById("registerPage").style.display = "block";
}

function showLoginPage() {
    hideAllPages();
    document.getElementById("loginPage").style.display = "block"
}

function showReservationPage() {
    hideAllPages();
    document.getElementById("reservationSection").style.display = "block";
}

function showAdminPanelPage() {
    document.getElementById("foot").style.display = "none";
    hideAllPages();
    document.getElementById("admin-panel").style.display = "block";
}

// REGISTER PAGE BUTTON FUCNTIONS
document.getElementById("register-link").addEventListener("click", function (event) {
    event.preventDefault();
    hideAllPages();
    document.getElementById("registerPage").style.display = "flex";
});

document.getElementById("back-to-home").addEventListener("click", function () {
    hideAllPages();
    document.getElementById("homePage").style.display = "block";
});

document.getElementById("back-to-login").addEventListener("click", function () {
    document.getElementById("registerPage").style.display = "none";

    document.getElementById("loginPage").style.display = "block";
});

// =============================================================

// LOGIN PAGE BUTTON FUNCTIONS
document.getElementById("loginButton").addEventListener("click", function () {
    hideAllPages();
    document.getElementById("loginPage").style.display = "block";
});

document.getElementById("back-to-home-login").addEventListener("click", function () {
    hideAllPages();
    document.getElementById("homePage").style.display = "block";
});

document.getElementById("back-to-register").addEventListener("click", function () {
    document.getElementById("loginPage").style.display = "none";

    document.getElementById("registerPage").style.display = "flex";
});

// LOGOUT 

function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("isLoggedIn");
    location.reload();
}
// ============================================================

// DATE PICKER
const today = new Date().toISOString().split('T')[0];

document.getElementById('date-picker').setAttribute('min', today);


document.getElementById("date-picker").addEventListener("click", function () {
    if (this.showPicker) {
        this.showPicker();
    }
});

// ============================================================


// TIME PICKER
// document.querySelectorAll(".time-slot").forEach(slot => {
//     slot.addEventListener("click", function () {
//         document.querySelectorAll(".time-slot").forEach(s => s.classList.remove("selected"));

//         this.classList.add("selected");

//         document.getElementById("selected-time-slot").value = `${this.dataset.start} - ${this.dataset.end}`;
//     });
// });
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".time-slot").forEach(slot => {
        slot.addEventListener("click", function () {
            // Remove 'selected' class from all time slots
            document.querySelectorAll(".time-slot").forEach(s => s.classList.remove("selected"));

            // Add 'selected' class to the clicked slot
            this.classList.add("selected");

            // Update hidden input value
            document.getElementById("selected-time-slot").value = this.dataset.time;

            console.log("Selected Time Slot:", this.dataset.time);
        });
    });
});

// ============================================================


// CONTACT US JS
function showTab(tabId) {
    const tabItems = document.querySelectorAll('.tab-item');
    const buttons = document.querySelectorAll('.tab-buttons button');

    tabItems.forEach(item => item.classList.remove('active'));

    buttons.forEach(button => button.classList.remove('active'));

    document.getElementById(tabId).classList.add('active');

    buttons.forEach(button => {
        if (button.getAttribute('data-tab').toLowerCase() === tabId.toLowerCase()) {
            button.classList.add('active');
        }
    });
}

// ============================================================

function fetchReservations(date) {
    const token = localStorage.getItem('token');

    if (!token) {
        console.error('Authorization token is missing!');
        return;
    }


    fetch(`https://localhost:7294/api/Reservation/GetReservedSlots?date=${date}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            const reservedSlots = data.reservedSlots; // API returns ["17:00"]
            // console.log("Reserved slots from API:", reservedSlots);

            document.querySelectorAll(".time-slot").forEach(slot => {
                const slotTime = slot.dataset.start.trim(); // Read start time
                // console.log(`Checking slot: ${slotTime}`);

                if (reservedSlots.includes(slotTime)) {
                    // console.log(`Marking ${slotTime} as reserved`);

                    // Apply styles properly
                    slot.style.backgroundColor = 'red';
                    slot.style.pointerEvents = 'none';
                    slot.style.color = 'white'; // Ensure text is readable
                    slot.classList.add("reserved"); // Add a class for CSS (optional)
                } else {
                    // console.log(`Slot ${slotTime} is available`);

                    slot.style.backgroundColor = ''; // Remove red background
                    slot.style.pointerEvents = ''; // Make it clickable again
                    slot.style.color = ''; // Reset text color
                    slot.classList.remove("reserved");
                }
            });
        })
        .catch(error => {
            console.error('Error fetching reservations:', error);
        });
}


// RESERVATIONS SCRIPT
function loadReservationData() {
    const date = document.getElementById("date-picker").value;
    const formattedDate = formatDateToDatabaseFormat(date);

    fetchReservations(formattedDate);
}

function formatDateToDatabaseFormat(dateString) {
    const date = new Date(dateString);
    return date.toISOString().split('T')[0]; // Formats to "YYYY-MM-DD"
}


// function formatDateToDatabaseFormat(dateString) {
//     const date = new Date(dateString);

//     const year = date.getFullYear();
//     const month = String(date.getMonth() + 1).padStart(2, '0');  // Ensure month is 2 digits
//     const day = String(date.getDate()).padStart(2, '0');  // Ensure day is 2 digits

//     return `${year}-${month}-${day}`;  // Return formatted date
// }



function displayReservations(reservationsForDate) {
    const reservationList = document.getElementById("reservationRecords");
    reservationList.innerHTML = ""; // Clear current list

    if (reservationsForDate.length === 0) {
        reservationList.innerHTML = "No reservations for this day, Be the first to reserve.";
        return;
    }

    reservationsForDate.forEach(reservation => {
        const reservationDiv = document.createElement("div");
        reservationDiv.classList.add("reservation-item");

        reservationDiv.innerHTML = `
      <strong>${reservation.time} - ${reservation.endTime}</strong>
      <p class="note">${reservation.note || 'No notes'}</p>
    `;

        reservationList.appendChild(reservationDiv);
    });
}

function makeReservation() {
    const date = document.getElementById("date-picker").value;
    const selectedSlot = document.getElementById("selected-time-slot").value;
    const note = document.getElementById("user-note").value;

    if (!date || !selectedSlot) {
        alert("Please select a date and a time slot.");
        return;
    }

    const [startTime, endTime] = selectedSlot.split(" - ");

    const reservation = {
        time: startTime,
        endTime: endTime,
        note: note,
    };

    saveReservationToDatabase(date, reservation);
}


function markBookedSlots(bookedTimes) {
    document.querySelectorAll(".time-slot").forEach(slot => {
        const startTime = slot.getAttribute("data-start");

        if (bookedTimes.includes(startTime)) {
            slot.classList.add("disabled-slot");
        } else {
            slot.classList.remove("disabled-slot");
        }
    });
}

function toggleMenu() {
    let menu = document.querySelector('.nav-links');
    let menuToggle = document.querySelector('.menu-toggle');

    menu.classList.toggle('active');

    if (menu.classList.contains('active')) {
        menuToggle.innerHTML = "&#10006;";
    } else {
        menuToggle.innerHTML = "&#9776;";
    }
}
function closeMenu() {
    document.querySelector('.nav-links').classList.remove('active');
}

// ======================================================== FETCHING BE ========================================================

// ========================================== FETCHING THE REGISTER ======================================================
document.getElementById("submit-register").addEventListener("click", async function () {
    const firstNameField = document.getElementById("firstName");
    const lastNameField = document.getElementById("lastName");
    const emailField = document.getElementById("email");
    const passwordField = document.getElementById("password");
    const phoneField = document.getElementById("usersPhone");

    if (!firstNameField || !lastNameField || !emailField || !passwordField || !phoneField) {
        alert("Please ensure all input fields are present.");
        return;
    }

    const firstName = firstNameField.value.trim();
    const lastName = lastNameField.value.trim();
    const email = emailField.value.trim();
    const password = passwordField.value.trim();
    const phone = phoneField.value.trim();

    if (!firstName || !lastName || !email || !password || !phone) {
        alert("Please fill in all fields!");
        return;
    }

    const registerData = {
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: password,
        PhoneNumber: phone,
        Role: this.role
    };

    try {
        const response = await fetch("https://localhost:7294/api/User/Register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(registerData)
        });

        const result = await response.text();
        alert(result);

        if (response.status === 201) {
            hideAllPages();
            document.getElementById("homePage").style.display = "block";
            alert("Please log in");
        }
    } catch (error) {
        alert("Error: Could not register user.");
        console.error(error);
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const galleryItems = document.querySelectorAll(".galleryItem");

    galleryItems.forEach((item, index) => {
        item.style.setProperty("--animation-delay", `${index * 0.2}s`);
    });

    function handleScroll() {
        galleryItems.forEach(item => {
            const rect = item.getBoundingClientRect();
            if (rect.top < window.innerHeight * 0.9) {
                item.classList.add("visible");
            }
        });
    }

    window.addEventListener("scroll", handleScroll);
    handleScroll();
});
// ====================================================================================================================

// ========================================== FETCHING THE LOGIN ======================================================
document.getElementById("submit-login").addEventListener("click", async function () {
    const email = document.getElementById("login-email").value.trim();
    const password = document.getElementById("login-password").value.trim();

    if (!email || !password) {
        alert("Please fill in both fields!");
        return;
    }

    const loginData = { email: email, password: password };

    try {
        const response = await fetch("https://localhost:7294/api/User/Login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(loginData)
        });

        const result = await response.json();

        console.log(result);

        if (response.ok) {
            alert("Login successful!");

            if (result.token) {
                localStorage.setItem("token", result.token);
            } else {
                console.warn("No token received from API.");
            }

            localStorage.setItem("isLoggedIn", "true");
            localStorage.setItem("userId", result.userId || "noUserID");
            localStorage.setItem("Role", result.role);

            document.getElementById("register-link").style.display = "none";
            document.getElementById("loginButton").style.display = "none";

            document.getElementById("LogOutButton-Container").style.display = "block";
            document.getElementById("logoutButton").style.display = "block";


            if (result.role === "Admin") {
                document.getElementById("admin-panel-link").style.display = "block";
            }

            hideAllPages();
            document.getElementById("homePage").style.display = "block";
        } else {
            alert(result.message || "Login failed!");
        }
    } catch (error) {
        alert("Error: Could not log in.");
        console.error(error);
    }
});

window.onload = function () {
    if (localStorage.getItem("isLoggedIn") === "true") {
        document.getElementById("register-link").style.display = "none";
        document.getElementById("loginButton").style.display = "none";
        document.getElementById("logoutButton").style.display = "block";
    } else {
        document.getElementById("register-link").style.display = "block";
        document.getElementById("loginButton").style.display = "block";
        document.getElementById("logoutButton").style.display = "none";
    }
};

function handleLogout() {
    localStorage.removeItem("token");
    localStorage.removeItem("isLoggedIn");
    localStorage.removeItem("userId");
    localStorage.removeItem("Role");

    document.getElementById("register-link").style.display = "block";
    document.getElementById("loginButton").style.display = "block";
    document.getElementById("logoutButton").style.display = "none";

    document.getElementById("admin-panel-link").style.display = "none";

    hideAllPages();
    document.getElementById("homePage").style.display = "block";
}


// ====================================================================================================================

// ========================================== FETCHING THE RESERVATION ================================================
async function makeReservation() {
    const userId = localStorage.getItem("userId");
    console.log("User ID from local storage:", userId);

    if (!userId) {
        alert("User not logged in.");
        return;
    }

    const selectedDate = document.getElementById("date-picker").value;
    const selectedTimeSlotElement = document.querySelector(".time-slot.selected");

    if (!selectedDate || !selectedTimeSlotElement) {
        alert("Please select a date and time slot.");
        return;
    }

    const selectedTimeSlot = selectedTimeSlotElement.dataset.time;
    const userNote = document.getElementById("user-note").value;
    // const phoneNumber = document.getElementById("phone-number").value;

    // if (!phoneNumber) {
    //     alert("Please put your phone number.");
    //     return;
    // }

    try {
        const startTime = selectedTimeSlot.split('-')[0];
        const [startHour, startMinute] = startTime.split(':').map(Number);

        const selectedDateTime = new Date(`${selectedDate}T${startHour.toString().padStart(2, '0')}:${startMinute.toString().padStart(2, '0')}:00.000Z`);

        const reservationData = {
            UserId: userId,
            StartingTime: selectedDateTime.toISOString(),
            NoteDescription: userNote,
            // PhoneNumber: phoneNumber
        };

        console.log("Final reservationData:", reservationData);

        const response = await fetch("https://localhost:7294/api/Reservation/AddReservation", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(reservationData)
        });

        if (!response.ok) {
            let errorText;
            try {
                errorText = await response.json();
            } catch {
                errorText = await response.text();
            }
            console.error("Error response:", errorText);
            alert(`Failed to add reservation: ${errorText.message || errorText}`);
            return;
        }

        const result = await response.json();
        alert("Reservation added successfully!");
        console.log("Reservation saved:", result);

    } catch (error) {
        console.error("Error adding reservation:", error);
        alert("Something went wrong. Try again!");
    }
}

// ====================================================================================================================


// ========================================== FETCHING THE ADMIN PANEL ================================================

document.addEventListener("DOMContentLoaded", function () {
    const userRole = localStorage.getItem("Role");

    if (userRole === "Admin") {
        // Show the existing Admin Panel link instead of creating a new one
        document.getElementById("admin-panel-link").style.display = "block";
    }
});



function fetchAllReservations() {
    const token = localStorage.getItem('token');

    if (!token) {
        console.error('No token found. Please log in first.');
        return;
    }

    fetch('https://localhost:7294/api/Reservation/GetAllReservations', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`,
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch reservations');
            }
            return response.json();
        })
        .then(data => {
            console.log('Reservations:', data);
            populateTable(data);
        })
        .catch(error => {
            console.error('Error fetching reservations:', error);
        });
}

function populateTable(reservations) {
    const tableBody = document.querySelector("#reservationTable tbody");
    tableBody.innerHTML = ""; // Clear previous data

    reservations.forEach(reservation => {
        const row = `
            <tr>
                <td>${reservation.id}</td>
                <td>${reservation.user.firstName}</td>
                <td>${reservation.user.lastName}</td>
                <td>${reservation.user.email}</td>
                <td>${reservation.user.phoneNumber || 'N/A'}</td>
                <td>${new Date(reservation.startingTime).toLocaleString()}</td>
                <td>${reservation.noteDescription || 'N/A'}</td>
                <td>
                    <button onclick="deleteReservation(${reservation.id})" class="delete-btn" style="border-radius: 10px; border: 2px solid black; background-color:rgba(209, 29, 29, 0.8);">Delete</button>
                </td>
            </tr>
        `;
        tableBody.innerHTML += row;
    });
}

async function getTodayReservations() {
    const token = localStorage.getItem('token');
    const userRole = localStorage.getItem('Role');
    console.log("button pressed");

    if (userRole !== "Admin") {
        console.error("Access denied! Only admins can view reservations.");
        return;
    }

    if (!token) {
        console.error('No token found. Please log in first.');
        return;
    }

    try {
        const response = await fetch('https://localhost:7294/api/Reservation/GetReservationForToday', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });

        if (!response.ok) throw new Error('Failed to fetch today\'s reservations');

        const data = await response.json();

        if(!data){
            alert("No reservations for today");
        }
        populateTable(data);
    } catch (error) {
        console.error('Error fetching today\'s reservations:', error);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const userRole = localStorage.getItem("Role");
    const userId = localStorage.getItem("UserId"); // Assuming you store UserId
    const registerButton = document.getElementById("register-link");
    const loginButton = document.getElementById("loginButton");
    const profileContainer = document.getElementById("LogOutButton-Container");

    if (userId) { // User is logged in
        registerButton.style.display = "none";
        loginButton.style.display = "none";
        profileContainer.style.display = "block"; // Show profile picture
    }

    if (userRole === "Admin") {
        document.getElementById("admin-panel-link").style.display = "block"; // Show Admin Panel button
    }
});


document.addEventListener("DOMContentLoaded", function () {
    const userRole = localStorage.getItem("Role");
    const userId = localStorage.getItem("UserId");
    const registerButton = document.getElementById("register-link");
    const loginButton = document.getElementById("loginButton");
    const profileContainer = document.getElementById("LogOutButton-Container");
    const adminPanelLink = document.getElementById("admin-panel-link");

    if (userId) { // User is logged in
        if (registerButton) registerButton.style.display = "none";
        if (loginButton) loginButton.style.display = "none";
        if (profileContainer) profileContainer.style.display = "block";
    }

    if (userRole === "Admin" && adminPanelLink) {
        adminPanelLink.style.display = "block";
    }
});

document.getElementById("logoutButton").addEventListener("click", handleLogout);
// ====================================================================================================================


// ========================================== FETCHING THE DELETE RESERVATION =========================================

function deleteReservation(reservationId) {
    const token = localStorage.getItem("token");

    if (!token) {
        console.error("Authorization token is missing");
        return;
    }

    if (!confirm("Are you sure you want to remove the reservation?")) {
        return;
    }

    fetch(`https://localhost:7294/api/Reservation/DeleteReservationById/${reservationId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`Failed to delete reservation. Status: ${response.status}`);
            }
            return response.text();
        })
        .then(message => {
            alert(message);
            location.reload();
        })
        .catch(error => {
            console.error('Error deleting reservation:', error);
            alert("Failed to delete reservation. Check console for details.");
        });
}