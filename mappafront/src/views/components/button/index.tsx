import { ReactNode } from "react";
import "./style.scss";
import { Link } from "react-router-dom";

interface ButtonProps {
  style?: "primary" | "secondary" | "square" | "transparent";
  href?: string;
  disabled?: boolean;
  onClick?: () => void;
  classNames?: string;
  type?: "button" | "submit" | "reset";
  children: ReactNode;
}

const Button = ({
  style = "primary",
  href,
  disabled = false,
  onClick,
  classNames,
  type = "button",
  children,
}: ButtonProps) => {
  const classes = `button button-${style}${disabled ? " disabled" : ""}${
    classNames ? " " + classNames : ""
  }`;

  if (href) {
    return (
      <Link to={href} className={`anchor ${disabled ? "anchor-disabled" : ""}`}>
        <div className={classes}>{children}</div>
      </Link>
    );
  }

  return (
    <button
      disabled={disabled}
      onClick={onClick}
      type={type}
      className={classes}
    >
      {children}
    </button>
  );
};
export default Button;
