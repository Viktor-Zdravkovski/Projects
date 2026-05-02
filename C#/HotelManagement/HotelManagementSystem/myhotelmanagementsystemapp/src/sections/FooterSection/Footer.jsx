import React, { useEffect, useState } from "react";
import styles from "../../styles/Footer.module.css";
import { FaMapMarkerAlt, FaEnvelope, FaClock, FaPhoneAlt, FaArrowUp, FaFacebookF, FaTwitter, FaLinkedinIn, FaInstagram } from "react-icons/fa";

function Footer() {
    const [year, setYear] = useState(new Date().getFullYear());

    const scrollToTop = () => {
        window.scrollTo({ top: 0, behavior: "smooth" });
    };

    return (
        <footer className={styles.footer}>
            <div className={styles.footerContainer}>

                {/* Footer Top */}
                <div className={styles.footerTop}>
                    {/* Location Column */}
                    <div className={styles.footerColumn}>
                        <h3 className={styles.footerHeading}>Location</h3>
                        <div className={styles.contactInfo}>
                            <div className={styles.contactItem}>
                                <FaMapMarkerAlt className={styles.contactIcon} />
                                <p className={styles.address}>
                                    123 Business Avenue<br />
                                    Cityville, ST 12345<br />
                                    United States
                                </p>
                            </div>
                            <div className={styles.contactItem}>
                                <FaEnvelope className={styles.contactIcon} />
                                <a  href="mailto:info@company.com" className={styles.address}>info@company.com</a>
                            </div>
                            <div className={styles.contactItem}>
                                <FaClock className={styles.contactIcon} />
                                <p className={styles.address}>
                                    Mon-Fri: 9AM - 6PM<br />
                                    Sat: 10AM - 3PM
                                </p>
                            </div>
                        </div>
                    </div>

                    {/* Logo & Social Column */}
                    <div className={`${styles.footerColumn} ${styles.footerLogo}`}>
                        <img src="./assets/logo.png" alt="Company Logo" />
                        <div className={styles.socialLinks}>
                            <a href="https://www.facebook.com/" target="_blank" className={styles.socialLink}><FaFacebookF /></a>
                            <a href="#" className={styles.socialLink}><FaTwitter /></a>
                            <a href="#" className={styles.socialLink}><FaLinkedinIn /></a>
                            <a href="https://www.instagram.com/" target="_blank" className={styles.socialLink}><FaInstagram /></a>
                        </div>
                    </div>

                    {/* Quick Links Column */}
                    <div className={styles.footerColumn}>
                        <h3 className={styles.footerHeading}>Quick Links</h3>
                        <ul className={styles.footerLinks}>
                            <li><a href="#">Our Team</a></li>
                            <li><a href="#">Our Products</a></li>
                            <li><a href="#">Our History</a></li>
                            <li><a href="#">Our Clients</a></li>
                            <li><a href="#">Certificates</a></li>
                        </ul>
                    </div>
                </div>

                {/* Footer Bottom */}
                <div className={styles.footerBottom}>
                    <div className={styles.callToAction}>
                        <p className={styles.callText}>Have questions? We're happy to help!</p>
                        <a href="tel:123456789" className={styles.phoneNumber}>
                            <FaPhoneAlt style={{ marginRight: "8px" }} />
                            123-456-789
                        </a>
                    </div>

                    <div className={styles.backToTop} onClick={scrollToTop}>
                        <FaArrowUp />
                    </div>

                    <p className={styles.copyright}>© {year} Company Name. All Rights Reserved.</p>
                </div>

            </div>
        </footer>
    );
}

export default Footer;