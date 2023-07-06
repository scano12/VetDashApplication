import PropTypes from "prop-types";
import "./index.css";
import { GlobalContext } from "../../Context";
import { useContext } from "react";
import { Button } from "./Button";

export const ListItem = (props) => {
  const { accepted } = useContext(GlobalContext);

  const disPlayBooking = props.bookingId === props.uniqueId ? true : false;

  const width = {
    width: "35rem",
  };

  return (
    <li
      className="list-item"
      style={
        accepted.length > 0 && !disPlayBooking
          ? { width: "32rem", gridTemplateRows: "auto" }
          : width
      }
    >
      <div
        className="list-item--avatar"
        style={accepted.length > 0 && !disPlayBooking ? { top: "6rem" } : {}}
      >
        <img
          src="../../../public/pets.png"
          alt="avatar"
          style={{ width: "70%" }}
        />
      </div>

      <div className="list-item--info">
        <div className="list-item--details">
          <span>{props.item.animal.firstName}</span>
          <div className="list-item-animal--details">
            {props.item.animal.breed && (
              <img src="../../../public/paws.png" style={{ width: "2rem" }} />
            )}
            <span>{props.item.animal.breed}</span>
          </div>
          <div>
            <span className="list-item-user--details">
              <img src="../../../public/owner.png" style={{ width: "2rem" }} />
              {props.item.user.firstName} {props.item.user.lastName}
            </span>
          </div>
          <div className="list-item--appointment">
            <img
              src="../../../public/appointment.png"
              style={{ width: "2rem" }}
            />
            <span>{props.item.appointmentType}</span>
          </div>
          <div className="list-item-user--time">
            <img src="../../../public/clock.png" style={{ width: "2rem" }} />

            {disPlayBooking && (
              <span>{props.item.requestedDateTimeOffset}</span>
            )}
            {!disPlayBooking && (
              <span style={{ fontSize: "12.5px" }}>
                {props.item.appointmentTime}
              </span>
            )}
          </div>
        </div>
      </div>

      <>
        {disPlayBooking && (
          <div className="list-item-footer">
            <div className="list-item-footer--btns">
              <Button
                onClick={() => props.onAccept(props.item)}
                text={"Accept"}
              />
              <Button
                onClick={() => props.onReschedule()}
                text={"Reschedule"}
              />
            </div>
          </div>
        )}
      </>
    </li>
  );
};

ListItem.propTypes = {
  uniqueId: PropTypes.number,
  bookingId: PropTypes.number,
  confirmedAppointmentId: PropTypes.number,
  item: PropTypes.object,
  onAccept: PropTypes.func,
  onReschedule: PropTypes.func,
};
