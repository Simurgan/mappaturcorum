import { ReactNode } from "react";
import "./style.scss";

interface TextProps {
  tag?: "span" | "p" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6";
  fw?: "100" | "300" | "400" | "500" | "700" | "900";
  lgfw?: "100" | "300" | "400" | "500" | "700" | "900";
  mdfw?: "100" | "300" | "400" | "500" | "700" | "900";
  smfw?: "100" | "300" | "400" | "500" | "700" | "900";
  fs?:
    | "12"
    | "14"
    | "16"
    | "18"
    | "20"
    | "22"
    | "24"
    | "28"
    | "30"
    | "32"
    | "36"
    | "40";
  lgfs?:
    | "12"
    | "14"
    | "16"
    | "18"
    | "20"
    | "22"
    | "24"
    | "28"
    | "30"
    | "32"
    | "36"
    | "40";
  mdfs?:
    | "12"
    | "14"
    | "16"
    | "18"
    | "20"
    | "22"
    | "24"
    | "28"
    | "30"
    | "32"
    | "36"
    | "40";
  smfs?:
    | "12"
    | "14"
    | "16"
    | "18"
    | "20"
    | "22"
    | "24"
    | "28"
    | "30"
    | "32"
    | "36"
    | "40";
  lh?: "125" | "14";
  color?: "black" | "burgundy" | "papirus" | "dark-gray" | "gray";
  isCenter?: boolean;
  classNames?: string;
  children: ReactNode;
}

const Text = ({
  tag = "p",
  fw = "400",
  lgfw = "400",
  mdfw = "400",
  smfw = "400",
  fs = "16",
  lgfs = "14",
  mdfs = "16",
  smfs = "16",
  lh = "125",
  color,
  isCenter = false,
  classNames,
  children,
}: TextProps) => {
  const classes = `text fw${fw} lgfw${lgfw} mdfw${mdfw} smfw${smfw} fs${fs} lgfs${lgfs} mdfs${mdfs} smfs${smfs} lh${lh}${
    color ? " " + color : ""
  }${isCenter ? " center" : ""}${classNames ? " " + classNames : ""}`;

  switch (tag) {
    case "span": {
      return <span className={classes}>{children}</span>;
    }
    case "p": {
      return <p className={classes}>{children}</p>;
    }
    case "h1": {
      return <h1 className={classes}>{children}</h1>;
    }
    case "h2": {
      return <h2 className={classes}>{children}</h2>;
    }
    case "h3": {
      return <h3 className={classes}>{children}</h3>;
    }
    case "h4": {
      return <h4 className={classes}>{children}</h4>;
    }
    case "h5": {
      return <h5 className={classes}>{children}</h5>;
    }
    case "h6": {
      return <h6 className={classes}>{children}</h6>;
    }
  }
};
export default Text;
