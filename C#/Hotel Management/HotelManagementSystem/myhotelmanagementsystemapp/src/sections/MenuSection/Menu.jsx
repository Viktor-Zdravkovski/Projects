import React, { useState } from "react";
import styles from "../../styles/Menu.module.css";
import { FaArrowRight, FaChevronDown } from "react-icons/fa";
import { menuData } from "../../data/MenuData.js";
import plateLeft from "../../assets/foodplate1.jpg";
import plateRight from "../../assets/foodplate1.jpg";

function MenuSection() {
    const [activeTab, setActiveTab] = useState("Starters");

    return (
        <section className={styles.menuSection}>
            {/* Decorative images */}
            <img src={plateRight} alt="Plate Right" className={styles.foodImgRight} />
            <img src={plateLeft} alt="Plate Left" className={styles.foodImgLeft} />

            {/* Main title */}
            <h2 className={styles.title}>Taste cuisines from all over the world</h2>

            <div className={styles.menuContent}>
                {/* Tabs */}
                <div className={styles.tabs}>
                    {Object.keys(menuData).map((tab) => (
                        <div
                            key={tab}
                            className={`${styles.tab} ${activeTab === tab ? styles.activeTab : ""}`}
                            onClick={() => setActiveTab(tab)}
                        >
                            {tab}
                        </div>
                    ))}
                </div>

                {/* Menu items */}
                <div className={styles.items}>
                    {menuData[activeTab].map((item, index) => (
                        <div key={index} className={styles.menuItem}>
                            <div className={styles.itemHeader}>
                                <p className={styles.itemName}>{item.name}</p>
                                <p className={styles.itemPrice}>{item.price}</p>
                            </div>

                            <div className={styles.separator}></div>

                            <p className={styles.itemDescription}>{item.description}</p>
                            <p className={styles.itemIngredients}>
                                <strong>Ingredients:</strong> {item.ingredients}
                            </p>
                        </div>
                    ))}
                </div>
            </div>

            {/* Mini title at bottom */}
            <button className={styles.miniCTA}>
                Our Restaurant <FaArrowRight />
            </button>
        </section>
    );
}


export default MenuSection;