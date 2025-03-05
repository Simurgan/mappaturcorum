import { Link } from "react-router-dom";
import "./style.scss";
import Text from "@/views/components/text";

const Footer = () => {
  return (
    <footer className="section footer-section">
      <div className="container">
        <div className="left-content flex gap-[195px]">
          <img
            src={require("@/assets/images/mappa-logo-negative.png")}
            className="logo-img"
            alt="Mappa Logo"
          />
          <address className="address-container">
            <Text fw={500} fs={20} lh={125} color="papirus">
              Contact Us
            </Text>
            <Link to="mailto:info@mappaanatolicum.com" className="mail-link">
              <Text fw={400} fs={14} lh={125} color="papirus">
                info@mappateam.com
              </Text>
            </Link>
          </address>
        </div>
        <div className="social-links">
          <Text fw={400} fs={12} lh={125} color="papirus">
            Follow us on
          </Text>
          <div className="social-icons flex items-center gap-[5px]">
            <Link
              to="https://www.linkedin.com/company/mappa-anatolicorum/"
              target="_blank"
            >
              <img
                src={require("@/assets/icons/linkedin-2.ico")}
                className="social-icon"
                alt="LinkedIn Logo"
              />
            </Link>
            <Link to="https://x.com/manatolicorum" target="_blank">
              <img
                src={require("@/assets/icons/twitter-x-logo.png")}
                className="social-icon"
                alt="LinkedIn Logo"
              />
            </Link>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
