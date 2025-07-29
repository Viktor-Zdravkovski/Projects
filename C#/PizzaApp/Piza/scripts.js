// // ===================== SHOW HIDE PAGES ========================
function hideAllPages() {
    document.getElementById("HoMePaGe").style.display = "none";
    document.getElementById("AboutUsPage").style.display = "none";
    document.getElementById("OurMenuPage").style.display = "none";
    document.getElementById("GalleryPageId").style.display = "none";
    document.getElementById("ContactUs").style.display = "none";
}

function showHomePage() {
    hideAllPages();
    document.getElementById("HoMePaGe").style.display = "flex";
}

function showAboutUsPage() {
    hideAllPages();
    document.getElementById("AboutUsPage").style.display = "flex";
}

function showOurMenuPage() {
    hideAllPages();
    document.getElementById("OurMenuPage").style.display = "flex";
}

function showGalleryPage() {
    hideAllPages();
    document.getElementById("GalleryPageId").style.display = "flex";
}

function showContactUsPage() {
    hideAllPages();
    document.getElementById("ContactUs").style.display = "flex";
}
// ====================================================================

// ===================== HAMBURGER OPTIONS =======================

function toggleMenu() {
    const navLinks = document.querySelector('.nav-links');
    navLinks.classList.toggle('open');
}

// ===============================================================

// ===================== MENU PAGE SLIDER ========================

const pages = document.querySelectorAll('.menuPage');
const nextBtn = document.getElementById('nextBtn');
const prevBtn = document.getElementById('prevBtn');

let currentPage = 0;

function updatePages() {
    pages.forEach((page, index) => {
        page.classList.toggle('active', index === currentPage);
    });

    prevBtn.disabled = currentPage === 0;
    nextBtn.disabled = currentPage === pages.length - 1;
}

nextBtn.addEventListener('click', () => {
    if (currentPage < pages.length - 1) {
        currentPage++;
        updatePages();
    }
});

prevBtn.addEventListener('click', () => {
    if (currentPage > 0) {
        currentPage--;
        updatePages();
    }
});

updatePages();

// ====================================================================

// ================ SHOW ADDITIONAL INFO ON ORDER =====================

function toggleExtraInfo() {
    const extraInfo = document.getElementById("extraInfoWrapper");
    const button = document.getElementById("menuToggleButton");

    const isVisible = extraInfo.style.display === "flex";

    extraInfo.style.display = isVisible ? "none" : "flex";
    button.textContent = isVisible ? "Where to order?" : "↑";
}

// ====================================================================

// ================ FUNCTION TO SHOW BIGGER PICTURE ===================

document.querySelectorAll('.imageCard img').forEach(img => {
    img.addEventListener('click', function () {
        let modal = document.getElementById("imageModal");
        let modalImage = document.getElementById("modalImage");
        modal.style.display = "block";
        modalImage.src = this.src;
    });
});

document.querySelector('.closeModal').addEventListener('click', function () {
    let modal = document.getElementById("imageModal");
    modal.style.display = "none";
});

document.getElementById("imageModal").addEventListener('click', function (e) {
    if (e.target === this) {
        this.style.display = "none";
    }
});

// ====================================================================

// ===================== CHANGE THE MAP ========================

const slides = document.querySelectorAll('.mapSlide');
const leftArrow = document.querySelector('.leftArrow');
const rightArrow = document.querySelector('.rightArrow');
let currentSlide = 0;

function showSlide(index) {
    slides.forEach((slide, i) => {
        slide.classList.toggle('active', i === index);
    });
}

leftArrow.addEventListener('click', () => {
    currentSlide = (currentSlide - 1 + slides.length) % slides.length;
    showSlide(currentSlide);
});

rightArrow.addEventListener('click', () => {
    currentSlide = (currentSlide + 1) % slides.length;
    showSlide(currentSlide);
});

showSlide(currentSlide);

// ====================================================================