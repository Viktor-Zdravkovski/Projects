import { useEffect, useRef, useState } from "react";
import { FaChevronDown } from "react-icons/fa";
import { ChevronDown } from "lucide-react";
import Header from "../sections/HeaderSection/Header.jsx";
import Footer from "../sections/FooterSection/Footer.jsx";
import styles from "./../styles/AboutUsPage.module.css";

export default function AboutUsPage() {
    const [showContent, setShowContent] = useState(false);

    return (
        <div className={styles.aboutContainer}>
            {/* === TOP GREEN HERO SECTION === */}
            <div className={styles.aboutHero}>
                <Header />

                <div className={styles.aboutHeroTitle}>
                    <h1>Welcome to our <span>luxury</span> hotel</h1>
                    <p>
                        We’re a passionate team dedicated to providing exceptional service
                        and unforgettable experiences to our customers.
                    </p>
                </div>

                {/* Down Arrow stays on right */}
                <div
                    className={styles.aboutArrow}
                    onClick={() => {
                        setShowContent(true);
                        setTimeout(
                            () =>
                                window.scrollTo({
                                    top: window.innerHeight,
                                    behavior: "smooth",
                                }),
                            200
                        );
                    }}
                >
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth={2}
                        stroke="white"
                        className={styles.arrowIcon}
                    >
                        <path strokeLinecap="round" strokeLinejoin="round" d="M19 9l-7 7-7-7" />
                    </svg>
                </div>
            </div>

            {/* === MAIN CONTENT AFTER ARROW === */}
            {showContent && (
                <>
                    {/* About Section: Image left, text right, stats row below */}
                    <div className={styles.aboutSection}>
                        <div className={styles.aboutContent}>
                            <div className={styles.aboutImage}>
                                <img src="../src/assets/parrots.jpg" />
                            </div>

                            <div className={styles.aboutText}>
                                <h2>Our Story</h2>
                                <p>
                                    Founded on passion and dedication, we’ve built our reputation
                                    through hard work and love for what we do. Over the years, we’ve
                                    grown from a small local team into a trusted name that represents
                                    quality and care. Every customer matters, every detail counts,
                                    and every experience drives us forward.
                                </p>
                                <p>
                                    Driven by curiosity and commitment, we started with a simple vision: to create something meaningful.
                                    From humble beginnings, our team has grown into a brand recognized for excellence and attention to detail.
                                    We value every client, embrace every challenge, and let each experience shape the way we move forward.
                                </p>
                                <img
                                    src="../src/assets/signature.png"
                                    alt="Signature"
                                    className={styles.signature}
                                />
                            </div>
                        </div>

                        <div className={styles.aboutStats}>
                            <div className={styles.aboutStat}>
                                <h3>250+</h3>
                                <p>Reservations Over the Year</p>
                            </div>
                            <div className={styles.aboutStat}>
                                <h3>1500+</h3>
                                <p>Happy Customers</p>
                            </div>
                            <div className={styles.aboutStat}>
                                <h3>98%</h3>
                                <p>Customer Satisfaction Rate</p>
                            </div>
                        </div>
                    </div>

                    {/* Static Photo Section */}
                    <div className={styles.aboutPhoto}>
                        <img src="../src/assets/room1.png" alt="Team" />
                    </div>

                    {/* Team Section */}
                    <div className={styles.aboutTeam}>
                        <div className={styles.teamRow}>
                            {/* Team member 1 */}
                            <div className={styles.teamCard}>
                                <img src="../src/assets/HotelStaff.png" alt="Person 2" />
                                <div className={styles.teamInfo}>
                                    <h3 className={styles.teamName}>Dragana Jovanovic</h3>

                                    <div className={styles.teamContact}>
                                        <p className={styles.teamPhone}>Phone: +389 71 352 655</p>
                                        <hr className={styles.contactLine} />
                                    </div>

                                    <div className={styles.teamContact}>
                                        <p className={styles.teamEmail}>Email: dragana@noreply.com</p>
                                        <hr className={styles.contactLine} />
                                    </div>

                                    <p className={styles.teamDesc}>
                                        Maria is our customer relations expert, dedicated to making
                                        sure each guest feels valued and supported from start to finish.
                                    </p>
                                </div>
                            </div>

                            <div className={styles.teamCard}>
                                <img src="../src/assets/HotelStaff2.png" alt="Person 2" />
                                <div className={styles.teamInfo}>
                                    <h3 className={styles.teamName}>Marija Petrova</h3>

                                    <div className={styles.teamContact}>
                                        <p className={styles.teamPhone}>Phone: +389 71 987 654</p>
                                        <hr className={styles.contactLine} />
                                    </div>

                                    <div className={styles.teamContact}>
                                        <p className={styles.teamEmail}>Email: marija@noreply.com</p>
                                        <hr className={styles.contactLine} />
                                    </div>

                                    <p className={styles.teamDesc}>
                                        Maria is our customer relations expert, dedicated to making
                                        sure each guest feels valued and supported from start to finish.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <Footer />
                </>
            )}
        </div>
    );
}