import React from 'react'
import styles from '../../styles/Rooms.module.css'

function Rooms() {
    return (
        <section className={styles.roomsSection} id="rooms">
            <h2 className={styles.sectionTitle}>Rooms & Suites</h2>

            <div className={styles.roomsGrid}>
                {/* Left Room */}
                <div className={styles.roomCard}>
                    <div className={styles.imageWrapper}>
                        <img src="./src/assets/room1.png" alt="Family Room" />
                        <div className={styles.topBadge}>Top Choice</div>
                        <div className={styles.overlay}>
                            <p>62m²</p>
                            <p>Beds: 2 single, 1 sofa</p>
                            <div className={styles.arrowCircle}>
                                <span className={styles.arrow}>&rarr;</span>
                            </div>
                        </div>
                    </div>

                    <div className={styles.roomInfo}>
                        <div className={styles.line}></div>
                        <h3 className={styles.roomTitle}>Family Room</h3>
                        <p className={styles.price}>$450 / night</p>
                    </div>
                </div>

                {/* Right Room */}
                <div className={styles.roomCard}>
                    <div className={styles.imageWrapper}>
                        <img src="./src/assets/room2.png" alt="Deluxe Suite" />
                        <div className={styles.overlay}>
                            <p>70m²</p>
                            <p>Beds: 1 king, 1 sofa</p>
                            <div className={styles.arrowCircle}>
                                <span className={styles.arrow}>&rarr;</span>
                            </div>
                        </div>
                    </div>

                    <div className={styles.roomInfo}>
                        <div className={styles.line}></div>
                        <h3 className={styles.roomTitle}>Deluxe Suite</h3>
                        <p className={styles.price}>$520 / night</p>
                    </div>
                </div>
            </div>

            {/* Testimonials */}
            <div className={styles.testimonials}>
                <button className={styles.arrowBtn}>&larr;</button>
                <div className={styles.testimonialCard}>
                    <p className={styles.text}>
                        “The rooms were spotless, the service excellent, and the view incredible.”
                    </p>
                    <h4 className={styles.author}>— Maria P.</h4>
                    <hr />
                    <img src="./src/assets/RoomsLogo.png" alt="Logo" className={styles.logo} />
                    <p className={styles.reviews}>From 200+ reviews</p>
                </div>
                <button className={styles.arrowBtn}>&rarr;</button>
            </div>
        </section>

    );
}

export default Rooms;