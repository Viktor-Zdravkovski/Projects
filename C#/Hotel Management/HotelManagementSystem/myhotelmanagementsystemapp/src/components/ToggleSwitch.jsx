import React from "react";
import styles from "../styles/ToggleSwitch.module.css";

const ToggleSwitch = ({
    checked,
    onChange,
    icon,
    onColor = "#4caf50",
    offColor = "#f44336",
}) => {
    return (
        <label
            className={styles.toggle}
            style={{ backgroundColor: checked ? onColor : offColor }}
        >
            <input type="checkbox" checked={checked} onChange={onChange} />
            <span className={styles.slider}>
                <div className={styles.iconWrapper}>{icon}</div>
            </span>
        </label>
    );
};

export default ToggleSwitch;
