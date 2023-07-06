import { Button } from "./Button";
import "./index.css";
import PropTypes from "prop-types";
export const ErrorModal = (props) => {
  return (
    <div className="error-modal">
      <h1>{props.headerOneText}</h1>
      {props.headerTwoText && <h2>Please Re-Schedule</h2>}
      <Button onClick={() => props.onOkClick()} text={"OK"} />
    </div>
  );
};

ErrorModal.propTypes = {
  onOkClick: PropTypes.func,
  headerOneText: PropTypes.string,
  headerTwoText: PropTypes.string,
};
