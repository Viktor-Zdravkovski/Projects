import React, { useState } from "react";
import Header from "../sections/HeaderSection/Header.jsx";
import Footer from "../sections/FooterSection/Footer.jsx";
import styles from "../styles/ContactPage.module.css";
import aboutStyles from "../styles/AboutUsPage.module.css"

export default function ContactPage() {
    const [formData, setFormData] = useState({
        name: "",
        surname: "",
        email: "",
        phone: "",
        message: "",
    });

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log("Form data:", formData);
        // Here you can handle sending the form to your backend
        alert("Message sent!");
    };

    return (
        <>
            <Header />

            {/* Hero Section */}
            <section className={styles.hero}>
                <div className={styles.heroText}>
                    <h1 className={styles.title}>Contact Our <span>Team</span></h1>
                    <p className={styles.subtitle}>
                        We’re here to help.<br />
                        Reach out anytime.
                    </p>
                </div>
            </section>

            {/* Team Section */}
            <div className={aboutStyles.aboutTeam}>
                <div className={aboutStyles.teamRow}>
                    {/* Team member 1 */}
                    <div className={aboutStyles.teamCard}>
                        <img src="../src/assets/HotelStaff.png" alt="Person 2" />
                        <div className={aboutStyles.teamInfo}>
                            <h3 className={aboutStyles.teamName}>Dragana Jovanovic</h3>

                            <div className={aboutStyles.teamContact}>
                                <p className={aboutStyles.teamPhone}>Phone: +389 71 352 655</p>
                                <hr className={aboutStyles.contactLine} />
                            </div>

                            <div className={aboutStyles.teamContact}>
                                <p className={aboutStyles.teamEmail}>Email: dragana@noreply.com</p>
                                <hr className={aboutStyles.contactLine} />
                            </div>

                            <p className={aboutStyles.teamDesc}>
                                Maria is our customer relations expert, dedicated to making
                                sure each guest feels valued and supported from start to finish.
                            </p>
                        </div>
                    </div>

                    <div className={aboutStyles.teamCard}>
                        <img src="../src/assets/HotelStaff2.png" alt="Person 2" />
                        <div className={aboutStyles.teamInfo}>
                            <h3 className={aboutStyles.teamName}>Marija Petrova</h3>

                            <div className={aboutStyles.teamContact}>
                                <p className={aboutStyles.teamPhone}>Phone: +389 71 987 654</p>
                                <hr className={aboutStyles.contactLine} />
                            </div>

                            <div className={aboutStyles.teamContact}>
                                <p className={aboutStyles.teamEmail}>Email: marija@noreply.com</p>
                                <hr className={aboutStyles.contactLine} />
                            </div>

                            <p className={aboutStyles.teamDesc}>
                                Maria is our customer relations expert, dedicated to making
                                sure each guest feels valued and supported from start to finish.
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            {/* Contact Form */}
            <section className={styles.contactForm}>
                <h2>Send Us a Message</h2>
                <form onSubmit={handleSubmit}>
                    <div className={styles.formRow}>
                        <input
                            type="text"
                            name="name"
                            placeholder="Name"
                            value={formData.name}
                            onChange={handleChange}
                            required
                        />
                        <input
                            type="text"
                            name="surname"
                            placeholder="Surname"
                            value={formData.surname}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className={styles.formRow}>
                        <input
                            type="email"
                            name="email"
                            placeholder="Email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                        />
                        <input
                            type="tel"
                            name="phone"
                            placeholder="Phone"
                            value={formData.phone}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <textarea
                        name="message"
                        placeholder="Your Message"
                        value={formData.message}
                        onChange={handleChange}
                        required
                    ></textarea>
                    <button type="submit">Send Message</button>
                </form>
            </section>

            <Footer />
        </>
    );
}
