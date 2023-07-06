import { useContext } from "react";
import { AppointmentRequests } from "./AppointmentRequests";
import { ConfirmedAppointments } from "./ConfirmedAppointments";
import "./index.css";
import { GlobalContext } from "../../Context";

export const Main = () => {
  const { accepted } = useContext(GlobalContext);

  const center = {
    padding: "5rem",
  };

  return (
    <main className="main" style={accepted.length > 0 ? {} : center}>
      <AppointmentRequests />
      <ConfirmedAppointments />
    </main>
  );
};
