import { useContext, useEffect, useState } from "react";
import { GlobalContext } from "../../Context";
import "./index.css";
import { ListItem } from "../share/ListItem";

export const AppointmentRequests = () => {
  const { items } = useContext(GlobalContext);
  return (
    <>
      {items.length > 0 ? (
        <div className="requests">
          <h2 className="requests-header">Appointment Requests</h2>
          <List />
        </div>
      ) : (
        <div className="requests">
          <h2 className="requests-header">Appointment Requests</h2>
          <span>No Requests</span>
        </div>
      )}
    </>
  );
};

function List() {
  const { accepted, setAccepted } = useContext(GlobalContext);
  const { items, setItems } = useContext(GlobalContext);
  const { setDisplayErrorModal } = useContext(GlobalContext);
  const { setDisplayErrorDuplicationModal } = useContext(GlobalContext);
  const { open, setOpen } = useContext(GlobalContext);
  const [selectedBooking, setSelectedBooking] = useState(null);

  useEffect(() => {
    if (selectedBooking && !open) {
      DeleteData(selectedBooking.appointmentId);
    }
  }, [open, selectedBooking]);

  const center = {
    justifyContent: "center",
  };

  function onAcceptBooking(item) {
    const pl = {
      appointmentId: item.appointmentId,
      appointmentType: item.appointmentType,
      requestedDateTimeOffset: item.requestedDateTimeOffset,
      user: {
        userId: item.user.userId,
        firstName: item.user.firstName,
        lastName: item.user.lastName,
        vetDataId: item.user.vetDataId,
      },
      animal: {
        animalId: item.animal.animalId,
        firstName: item.animal.firstName,
        species: item.animal.species,
        breed: item.animal.breed,
      },
    };
    PostData(pl, item);
  }

  async function PostData(pl, item) {
    try {
      const response = await fetch(
        "https://localhost:7001/api/vetAppointmentApi",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          mode: "cors",
          body: JSON.stringify(pl),
        }
      );
      let jsonData = await response.json();

      console.log(jsonData);

      if (jsonData.success) {
        setAccepted(jsonData.data);
        setItems(
          items.filter(
            (booking) => booking.appointmentId !== item.appointmentId
          )
        );
      } else {
        jsonData.message === "Duplication of appointment."
          ? setDisplayErrorDuplicationModal(true)
          : setDisplayErrorModal(true);
        console.log(jsonData.message);
      }
    } catch (error) {
      console.log(error);
    }
  }

  async function DeleteData(id) {
    try {
      const response = await fetch(
        `https://localhost:7001/api/vetAppointmentApi/${id}`,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
          mode: "cors",
        }
      );
      let jsonData = await response.json();

      console.log(jsonData);

      if (jsonData.success) {
        setItems(jsonData.data);
      } else {
        //Set new error modal
        console.log(jsonData.message);
      }
    } catch (error) {
      console.log(error);
    }
  }

  function onRescheduleBooking(item) {
    setOpen(true);
    setSelectedBooking(item);
  }

  return (
    <ul className="requests-list" style={accepted.length > 0 ? {} : center}>
      {items.map((item) => (
        <ListItem
          key={item.appointmentId}
          bookingId={item.appointmentId}
          uniqueId={item.appointmentId}
          item={item}
          accepted={accepted}
          onAccept={onAcceptBooking}
          onReschedule={() => onRescheduleBooking(item)}
        />
      ))}
    </ul>
  );
}
