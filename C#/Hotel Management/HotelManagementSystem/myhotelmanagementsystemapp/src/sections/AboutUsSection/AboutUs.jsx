import React from "react";
import styles from "../../styles/AboutUs.module.css";

function AboutUs() {
    return (
        <section id="aboutUs" className={styles.about}>
            <div className={styles.inner}>
                <div className={styles.leftImg}>
                    <img src="../src/assets/parrots.jpg" />
                </div>

                <div className={styles.rightText}>
                    <h2 className={styles.title}>
                        Welcome to our charming Bed & Breakfast, located in a picturesque
                        area away from the hustle and bustle of the city.
                    </h2>

                    <p className={styles.paragraph}>
                        Nestled in the heart of nature, our cozy retreat offers the perfect
                        escape from the noise and rush of everyday life. Every morning begins
                        with the aroma of freshly baked bread, brewed coffee, and the gentle
                        sounds of birds singing outside your window.
                    </p>

                    <div className={styles.signature}>
                        <img src="../src/assets/signature.png" alt="Signature" />
                    </div>

                    <p className={styles.menuLink}>VIEW OUR MENU</p>
                </div>
            </div>
        </section>
    );
}

export default AboutUs;