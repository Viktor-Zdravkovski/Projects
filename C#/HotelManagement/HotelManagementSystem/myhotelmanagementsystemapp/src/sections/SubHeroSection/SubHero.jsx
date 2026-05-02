import React from "react";
import { FaArrowRight, FaChevronDown } from "react-icons/fa";
import styles from "../../styles/SubHero.module.css";

function SubHero() {
    return (
        <section className={styles.subhero}>
            <div className={styles.suboverlay}></div>

            <div className={styles.subheroText}>
                <h2 className={styles.subtitle}>
                    Our rooms are your own <span>personal</span> sanctuary
                </h2>

                <p className={styles.subsubtitle}>
                    Each room is crafted for ultimate comfort and elegance. Enjoy spacious interiors, soft lighting, and a warm ambiance that feels like home.
                </p>

                <a href="#rooms" className={styles.subheroCta}>
                    Explore Our Rooms →
                </a>
            </div>
        </section>
    );
}

export default SubHero;
