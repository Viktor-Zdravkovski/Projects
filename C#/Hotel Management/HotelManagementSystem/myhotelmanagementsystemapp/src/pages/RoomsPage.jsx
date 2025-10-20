import React, { useState, useEffect } from "react";
import { FaChevronDown } from "react-icons/fa";
import Header from "../sections/HeaderSection/Header.jsx";
import Footer from "../sections/FooterSection/Footer.jsx";
import styles from "../styles/RoomsPage.module.css"; // separate CSS

const roomsData = [
    { id: 1, title: "Family Room", price: 450, wifi: true, img: "../src/assets/room1.png" },
    { id: 2, title: "Deluxe Suite", price: 520, wifi: false, img: "../src/assets/room1.png" },
    { id: 3, title: "Single Room", price: 300, wifi: true, img: "../src/assets/room1.png" },
    { id: 4, title: "Presidential Suite", price: 900, wifi: true, img: "../src/assets/room1.png" },
];

export default function RoomsPage() {
    const [wifiFilter, setWifiFilter] = useState(false);

    // Filter rooms based on WiFi toggle
    const filteredRooms = wifiFilter ? roomsData.filter(room => room.wifi) : roomsData;

    return (
        <>
            <Header />

            {/* Top hero section */}
            <section className={styles.topSection}>
                <div className={styles.textLeft}>
                    <h1>Perfectly <span>Matched</span> Rooms</h1>
                    <p>
                        Choose from our carefully designed rooms to suit every taste and
                        preference. Comfort, style, and convenience are guaranteed.
                    </p>
                </div>
            </section>

            {/* Filters */}
            <section className={styles.filters}>
                <label className={styles.toggle}>
                    <span>No WiFi</span>
                    <input
                        type="checkbox"
                        checked={wifiFilter}
                        onChange={() => setWifiFilter(!wifiFilter)}
                    />
                    <span className={styles.slider}></span>
                    <span>Include WiFi</span>
                </label>
                <hr />
            </section>

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

// export default function RoomsPage() {
//     const [rooms, setRooms] = useState([]);
//     const [wifiFilter, setWifiFilter] = useState(false);
//     const [loading, setLoading] = useState(true);

//     // Fetch rooms from API/Database
//     useEffect(() => {
//         async function fetchRooms() {
//             try {
//                 const response = await fetch("/api/rooms"); // adjust your API endpoint
//                 const data = await response.json();
//                 setRooms(data);
//             } catch (error) {
//                 console.error("Error fetching rooms:", error);
//             } finally {
//                 setLoading(false);
//             }
//         }
//         fetchRooms();
//     }, []);

//     // Filter rooms by WiFi
//     const filteredRooms = wifiFilter ? rooms.filter(room => room.wifi) : rooms;

//     return (
//         <>
//             <Header />

//             {/* Top Hero Section */}
//             <section className={styles.topSection}>
//                 <div className={styles.textLeft}>
//                     <h1>Perfectly Matched Rooms</h1>
//                     <p>
//                         Choose from our carefully designed rooms to suit every taste and
//                         preference. Comfort, style, and convenience are guaranteed.
//                     </p>
//                 </div>
//                 <div className={styles.arrowRight}>
//                     <FaChevronDown size={30} />
//                 </div>
//             </section>

//             {/* Filters */}
//             <section className={styles.filters}>
//                 <label className={styles.toggle}>
//                     <span>No WiFi</span>
//                     <input
//                         type="checkbox"
//                         checked={wifiFilter}
//                         onChange={() => setWifiFilter(!wifiFilter)}
//                     />
//                     <span className={styles.slider}></span>
//                     <span>Include WiFi</span>
//                 </label>
//                 <hr />
//             </section>

//             {/* Rooms Grid */}
//             <section className={styles.roomsGrid}>
//                 {loading ? (
//                     <p>Loading rooms...</p>
//                 ) : filteredRooms.length === 0 ? (
//                     <p>No rooms match your filters.</p>
//                 ) : (
//                     filteredRooms.map(room => (
//                         <div key={room.id} className={styles.roomCard}>
//                             <img src={room.img} alt={room.title} />
//                             <div className={styles.roomInfo}>
//                                 <h3>{room.title}</h3>
//                                 <hr />
//                                 <p>${room.price} / night</p>
//                             </div>
//                         </div>
//                     ))
//                 )}
//             </section>

//             <Footer />
//         </>
//     );
// }