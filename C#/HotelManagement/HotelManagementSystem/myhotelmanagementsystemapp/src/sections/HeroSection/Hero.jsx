import React from "react";
import { FaArrowRight, FaChevronDown } from "react-icons/fa";
import styles from "../../styles/Hero.module.css";

function Hero() {
  // smooth scroll function
  const scrollToAbout = () => {
    const aboutSection = document.getElementById("aboutUs");
    if (aboutSection) {
      aboutSection.scrollIntoView({ behavior: "smooth" });
    }
  };

  return (
    <section className={styles.hero}>
      {/* Overlay for dim effect */}
      <div className={styles.overlay}></div>

      {/* Lower-left title */}
      <div className={styles.heroText}>
        <h1 className={styles.title}>
          Welcome to our <span>RoyalStay</span> Hotel
        </h1>
        <p className={styles.subtitle}>
          Experience unparalleled luxury.
          <br />
          Relax, unwind, and enjoy every moment.
        </p>
      </div>

      {/* Lower-right CTA */}
      <a href="#rooms" className={styles.heroCta}>
        View All Rooms <FaArrowRight />
      </a>

      {/* Middle-bottom scroll arrow with smooth scroll */}
      <div className={styles.scrollDown} onClick={scrollToAbout}>
        <FaChevronDown />
      </div>
    </section>
  );
}

export default Hero;
