import PropTypes from "prop-types";

export const Button = (props) => {
  return (
    <button className="list-item-btn" onClick={() => props.onClick()}>
      {props.text}
    </button>
  );
};

Button.propTypes = {
  onClick: PropTypes.func,
  text: PropTypes.string,
};
