import React, { useState, useEffect } from "react";
import ToggleSwitch from "../components/ToggleSwitch.jsx";
import Header from "../sections/HeaderSection/Header.jsx";
import Footer from "../sections/FooterSection/Footer.jsx";
import { FaWifi, FaTv, FaSnowflake, FaPhone } from "react-icons/fa";
import { GiHanger } from 'react-icons/gi';
import styles from "../styles/RoomsPage.module.css"; // separate CSS

const roomsData = [
    { id: 1, title: "Family Room", price: 450, wifi: true, img: "../src/assets/room1.png" },
    { id: 2, title: "Deluxe Suite", price: 520, wifi: false, img: "../src/assets/room1.png" },
    { id: 3, title: "Single Room", price: 300, wifi: true, img: "../src/assets/room1.png" },
    { id: 4, title: "Presidential Suite", price: 900, wifi: true, img: "../src/assets/room1.png" },
];

export default function RoomsPage() {
    const [wifiFilter, setWifiFilter] = useState(false);
    const [tvFilter, setTvFilter] = useState(false);
    const [acFilter, setAcFilter] = useState(false);
    const [phoneFilter, setPhoneFilter] = useState(false);
    const [hangerFilter, setHangerFilter] = useState(false);



    // Filter rooms based on selected filters
    const filteredRooms = roomsData.filter(room => {
        if (wifiFilter && !room.wifi) return false;
        if (tvFilter && !room.tv) return false;
        if (acFilter && !room.ac) return false;
        if (phoneFilter && !room.phone) return false;
        if (hangerFilter && !room.hanger) return false; // use hangerFilter
        return true;
    });

    return (
        <>
            <Header />

            {/* Top hero section */}
            <section className={styles.topSection}>
                <div className={styles.textLeft}>
                    <h1>
                        Perfectly <span>Matched</span> Rooms
                    </h1>
                    <p>
                        Choose from our carefully designed rooms to suit every taste and
                        preference. Comfort, style, and convenience are guaranteed.
                    </p>
                </div>
            </section>

            {/* Filters */}
            <section className={styles.filters}>
                <div className={styles.switchGroup}>
                    <ToggleSwitch
                        checked={wifiFilter}
                        onChange={() => setWifiFilter(!wifiFilter)}
                        icon={<FaWifi />}
                    />
                    <ToggleSwitch
                        checked={tvFilter}
                        onChange={() => setTvFilter(!tvFilter)}
                        icon={<FaTv />}
                    />
                    <ToggleSwitch
                        checked={acFilter}
                        onChange={() => setAcFilter(!acFilter)}
                        icon={<FaSnowflake />}
                    />
                    <ToggleSwitch
                        checked={phoneFilter}
                        onChange={() => setPhoneFilter(!phoneFilter)}
                        icon={<FaPhone />}
                    />
                    <ToggleSwitch
                        checked={hangerFilter}
                        onChange={() => setHangerFilter(!hangerFilter)}
                        icon={<GiHanger />}
                    />
                </div>
                <hr />
            </section>

            {/* Rooms */}
            <div className={styles.roomsGridBackground}>
                <section className={styles.roomsGrid}>
                    {filteredRooms.map(room => (
                        <div key={room.id} className={styles.roomCard}>
                            <div className={styles.roomImage}>
                                <img src={room.img} alt={room.title} />
                            </div>
                            <div className={styles.roomInfo}>
                                <h3>{room.title}</h3>
                                <p>${room.price} / night</p>
                                <hr className={styles.roomLine} />
                            </div>
                        </div>
                    ))}
                </section>
            </div>

            <Footer />
        </>
    );
}