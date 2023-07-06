import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "./index.css";
import { useContext } from "react";
import { GlobalContext } from "../../Context";
export const CustomDatePicker = () => {
  const { date, setDate } = useContext(GlobalContext);
  const { open, setOpen } = useContext(GlobalContext);

  return (
    <div className="date-picker-modal">
      {open && (
        <button
          style={{
            border: "none",
            backgroundColor: "transparent",
            justifyContent: "flex-end",
            color: "grey",
            cursor: "pointer",
          }}
          onClick={() => setOpen(false)}
        >
          x
        </button>
      )}
      <DatePicker
        className={"hidden-input"}
        selected={date}
        open={open}
        showTimeSelect
        onChange={(date) => {
          setDate(date);
        }}
        onClickOutside={() => setOpen(false)}
        showPopperArrow={false}
        dateFormat="MMMM d, yyyy h:mm aa"
      />
    </div>
  );
};
