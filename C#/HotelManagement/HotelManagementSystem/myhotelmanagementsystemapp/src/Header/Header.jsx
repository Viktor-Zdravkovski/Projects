import styles from './Header.module.css'
import { useState } from 'react'

function Header() {
    const [activeLink, setActiveLink] = useState('home')

    const handleLinkClick = (link) => {
        setActiveLink(link)
    }

    return (
        <header className={styles.navbar}>
            {/* --- Logo Section --- */}
            <div className={styles.logo}>
                <img src="/logo.png" alt="Hotel Logo" />
                <div className={styles.logoText}>
                    <h1>RoyalStay</h1>
                    <p>Since 1989</p>
                </div>
            </div>

            {/* --- Navigation Links --- */}
            <nav>
                <ul>
                    <li>
                        <a
                            href="#"
                            className={activeLink === 'home' ? styles.active : ''}
                            onClick={() => handleLinkClick('home')}
                        >
                            Home
                        </a>
                    </li>
                    <li>
                        <a
                            href="#"
                            className={activeLink === 'about' ? styles.active : ''}
                            onClick={() => handleLinkClick('about')}
                        >
                            About
                        </a>
                    </li>
                    <li>
                        <a
                            href="#"
                            className={activeLink === 'rooms' ? styles.active : ''}
                            onClick={() => handleLinkClick('rooms')}
                        >
                            Rooms
                        </a>
                    </li>
                    <li>
                        <a
                            href="#"
                            className={activeLink === 'contact' ? styles.active : ''}
                            onClick={() => handleLinkClick('contact')}
                        >
                            Contact
                        </a>
                    </li>
                </ul>
            </nav>

            {/* --- Auth Buttons --- */}
            <div className={styles.authButtons}>
                <button className={styles.register}>Register</button>
                <button className={styles.login}>Login</button>
            </div>
        </header>
    )
}

export default Header
