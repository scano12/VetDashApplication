import { useContext } from "react";
import "./index.css";
import { GlobalContext } from "../../Context";
import { ListItem } from "../share/ListItem";

export const ConfirmedAppointments = () => {
  const { accepted } = useContext(GlobalContext);
  return (
    <>
      {accepted.length > 0 && (
        <div className="confirmed-appt">
          <h2 className="requests-header">Confirmed Appointments</h2>
          <List />
        </div>
      )}
    </>
  );
};

function List() {
  const { accepted } = useContext(GlobalContext);
  return (
    <ul className="requests-list">
      {accepted.map((o) => (
        <ListItem
          key={o.animal.animalId}
          confirmedAppointmentId={o.animal.animalId}
          uniqueId={o.animal.animalId}
          item={o}
        />
      ))}
    </ul>
  );
}
