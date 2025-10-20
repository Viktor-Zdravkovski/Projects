import React, { useState } from "react";
import Header from "../sections/HeaderSection/Header.jsx";
import Footer from "../sections/FooterSection/Footer.jsx";
import styles from "../styles/ContactPage.module.css";

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
                    <div className={styles.arrow}>↓</div>
                </div>
            </section>

            {/* Team Section */}
            <section className={styles.team}>
                <div className={styles.member}>
                    <img src="/assets/evelyn.jpg" alt="Evelyn Smith" />
                    <div className={styles.info}>
                        <h4>Evelyn Smith</h4>
                        <p>Phone: 123-456-789</p>
                        <p>Email: evelyn@hotel.com</p>
                        <p>Experienced manager ensuring guests enjoy a perfect stay.</p>
                    </div>
                </div>
                <div className={styles.member}>
                    <img src="/assets/josephine.jpg" alt="Josephine Albertino" />
                    <div className={styles.info}>
                        <h4>Josephine Albertino</h4>
                        <p>Phone: 987-654-321</p>
                        <p>Email: josephine@hotel.com</p>
                        <p>Expert concierge helping visitors explore the best local spots.</p>
                    </div>
                </div>
            </section>

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
