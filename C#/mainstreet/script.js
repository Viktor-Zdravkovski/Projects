document.addEventListener("DOMContentLoaded", () => {

    // --- 1. Scroll Animations (Intersection Observer) ---
    const observerOptions = {
        root: null,
        rootMargin: '0px',
        threshold: 0.1
    };

    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('show');
            }
        });
    }, observerOptions);

    // Initial observe
    document.querySelectorAll('.animate-block').forEach(el => observer.observe(el));


    // --- 2. Navbar Scroll Effect ---
    const nav = document.querySelector('.navbar');
    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) {
            nav.style.boxShadow = "0 4px 12px rgba(0,0,0,0.3)";
        } else {
            nav.style.boxShadow = "none";
        }
    });

    // --- 3. View Switching Logic (Home vs Menu) ---
    window.switchView = function (viewName, event) {
        if (event) event.preventDefault(); // Prevent jump to ID

        const homeView = document.getElementById('home-view');
        const menuView = document.getElementById('menu-view');
        const aboutView = document.getElementById('about-view');
        const reviewsView = document.getElementById('reviews-view');
        const contactView = document.getElementById('contact-view')
        const navLinks = document.querySelectorAll('.nav-links a');

        // Update Nav Active State
        navLinks.forEach(link => link.classList.remove('active'));

        // Find the link that corresponds to the view and make it active
        // This is a simple check, looking for the href content
        navLinks.forEach(link => {
            if (link.getAttribute('href') === '#' + viewName) {
                link.classList.add('active');
            }
        });

        // Toggle Views
        // if (viewName === 'menu') {
        //     homeView.classList.remove('active-view');
        //     setTimeout(() => {
        //         homeView.style.display = 'none';
        //         menuView.style.display = 'block';
        //         // Small delay to allow display:block to apply before opacity transition
        //         setTimeout(() => menuView.classList.add('active-view'), 50);
        //     }, 300); // Wait for fade out
        // } else {
        //     menuView.classList.remove('active-view');
        //     setTimeout(() => {
        //         menuView.style.display = 'none';
        //         homeView.style.display = 'block';
        //         setTimeout(() => homeView.classList.add('active-view'), 50);
        //     }, 300);
        // }

        const allViews = [homeView, menuView, aboutView, reviewsView, contactView];
        allViews.forEach(v => v.classList.remove('active-view'));

        // Show Selected with delay to prevent "dissolving"
        setTimeout(() => {
            allViews.forEach(v => v.style.display = 'none');

            const target = document.getElementById(viewName + '-view');
            target.style.display = 'block';

            setTimeout(() => target.classList.add('active-view'), 50);
        }, 500);

        // Scroll to top smoothly
        window.scrollTo({ top: 0, behavior: 'smooth' });
    };

    // --- 4. Menu Filter Logic ---
    window.filterMenu = function (category) {
        const items = document.querySelectorAll('.menu-item');
        const buttons = document.querySelectorAll('.tab-btn');

        // Update Active Button
        buttons.forEach(btn => {
            btn.classList.remove('active');
            // Check if button text matches category roughly or if onclick matches
            if (btn.textContent.toLowerCase().includes(category) || category === 'all' && btn.textContent === 'All') {
                // This is handled by the click event, but we ensure visual state
                if (btn === event.target) btn.classList.add('active');
            }
        });
        // Ensure the clicked button gets active class (event.target fallback)
        if (event && event.target) {
            buttons.forEach(b => b.classList.remove('active'));
            event.target.classList.add('active');
        }

        // Filter Items
        items.forEach(item => {
            // Reset animation
            item.classList.remove('fade-in-item');

            if (category === 'all' || item.getAttribute('data-category') === category) {
                item.classList.remove('hide');
                // Trigger reflow to restart animation
                void item.offsetWidth;
                item.classList.add('fade-in-item');
            } else {
                item.classList.add('hide');
            }
        });
    };
});