import React from "react";
import styles from "./Hero.module.css";
import { FaArrowRight, FaChevronDown } from "react-icons/fa";

function Hero() {
  return (
    <section className={styles.hero}>
      <div className={styles.overlay}></div>

      {/* Lower-left title */}
      <div className={styles.heroText}>
        <h1 className={styles.title}>Welcome to our <span>RoyalStay</span> Hotel</h1>
        <p className={styles.subtitle}>
          Experience unparalleled luxury.<br />
          Relax, unwind, and enjoy every moment.
        </p>
      </div>

      {/* Lower-right CTA */}
      <a href="#rooms" className={styles.heroCta}>
        View All Rooms <FaArrowRight />
      </a>

      {/* Middle-bottom scroll arrow */}
      <div className={styles.scrollDown}>
        <FaChevronDown />
      </div>
    </section>
  );
}

export default Hero;
