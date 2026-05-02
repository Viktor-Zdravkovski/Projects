import { Link, useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import styles from "../../styles/Header.module.css";

function Header() {
    const location = useLocation();
    const [activeLink, setActiveLink] = useState("home");

    useEffect(() => {
        if (location.pathname === "/") setActiveLink("home");
        else if (location.pathname === "/about") setActiveLink("about");
        else if (location.pathname === "/rooms") setActiveLink("rooms");
        else if (location.pathname === "/contact") setActiveLink("contact");
    }, [location]);

    return (
        <header className={styles.navbar}>
            {/* Logo Section */}
            <div className={styles.logo}>
                <img src="/logo.png" alt="Hotel Logo" />
                <div className={styles.logoText}>
                    <h1>RoyalStay</h1>
                    <p>Since 1989</p>
                </div>
            </div>

            {/* Navigation */}
            <nav>
                <ul>
                    <li>
                        <Link
                            to="/"
                            className={activeLink === "home" ? styles.active : ""}
                        >
                            Home
                        </Link>
                    </li>
                    <li>
                        <Link
                            to="/about"
                            onClick={() => console.log("About clicked")}
                            className={activeLink === "about" ? styles.active : ""}
                        >
                            About
                        </Link>
                    </li>
                    <li>
                        <Link
                            to="/rooms"
                            className={activeLink === "rooms" ? styles.active : ""}
                        >
                            Rooms
                        </Link>
                    </li>
                    <li>
                        <Link
                            to="/contact"
                            className={activeLink === "contact" ? styles.active : ""}
                        >
                            Contact
                        </Link>
                    </li>
                </ul>
            </nav>

            {/* Auth Buttons */}
            <div className={styles.authButtons}>
                <button className={styles.register}>Register</button>
                <button className={styles.login}>Login</button>
            </div>
        </header>
    );
}

export default Header;


// import styles from './Header.module.css'
// import { useState } from 'react'

// function Header() {
//     const [activeLink, setActiveLink] = useState('home')

//     const handleLinkClick = (link) => {
//         setActiveLink(link)
//     }

//     return (
//         <header className={styles.navbar}>
//             {/* --- Logo Section --- */}
//             <div className={styles.logo}>
//                 <img src="/logo.png" alt="Hotel Logo" />
//                 <div className={styles.logoText}>
//                     <h1>RoyalStay</h1>
//                     <p>Since 1989</p>
//                 </div>
//             </div>

//             {/* --- Navigation Links --- */}
//             <nav>
//                 <ul>
//                     <li>
//                         <a
//                             href="#"
//                             className={activeLink === 'home' ? styles.active : ''}
//                             onClick={() => handleLinkClick('home')}
//                         >
//                             Home
//                         </a>
//                     </li>
//                     <li>
//                         <a
//                             href="#"
//                             className={activeLink === 'about' ? styles.active : ''}
//                             onClick={() => handleLinkClick('about')}
//                         >
//                             About
//                         </a>
//                     </li>
//                     <li>
//                         <a
//                             href="#"
//                             className={activeLink === 'rooms' ? styles.active : ''}
//                             onClick={() => handleLinkClick('rooms')}
//                         >
//                             Rooms
//                         </a>
//                     </li>
//                     <li>
//                         <a
//                             href="#"
//                             className={activeLink === 'contact' ? styles.active : ''}
//                             onClick={() => handleLinkClick('contact')}
//                         >
//                             Contact
//                         </a>
//                     </li>
//                 </ul>
//             </nav>

//             {/* --- Auth Buttons --- */}
//             <div className={styles.authButtons}>
//                 <button className={styles.register}>Register</button>
//                 <button className={styles.login}>Login</button>
//             </div>
//         </header>
//     )
// }

// export default Header
