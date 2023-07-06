import { useContext } from "react";
import "./index.css";
import { GlobalContext } from "../../Context";
import { CustomDatePicker } from "../datePicker/CustomDatePicker";

export const Sidebar = () => {
  const { items } = useContext(GlobalContext);
  return (
    <nav className="sidebar">
      <img src="../public/vet.png" className="sidebar-logo" />
      <h3 className="sidebar-message">Welcome Back Dr. Cano</h3>
      <span className="sidebar-bookings-number">
        You have {items ? items.length : "no"} appointment requests
      </span>
      <CustomDatePicker />
    </nav>
  );
};
