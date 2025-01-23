import Text from "@/views/components/text";
import { Link } from "react-router-dom";

import mailIcon from "@/assets/icons/mail-icon.svg";
import phoneIcon from "@/assets/icons/phone-icon.svg";

import "./style.scss";

const ContactPage = () => {
  return (
    <div className="section">
      <div className="container">
        <div className="contact-header">
          <Text fs={36} fw={700} lh={125} color="burgundy">
            Contact
          </Text>
          <div className="contact-info">
            <img src={mailIcon} width={24} height={18} />
            <Link
              to={"mailto:info@mappaanatolicum.com"}
              className="contact-email"
            >
              <Text fs={24} fw={400} lh={125} color="burgundy">
                info@mappaanatolicum.com
              </Text>
            </Link>
          </div>
          <div className="contact-info">
            <img src={phoneIcon} width={24} height={18} />
            <Text fs={24} fw={400} lh={125} color="burgundy">
              +90 123 456 78 90
            </Text>
          </div>
        </div>
        <div className="contact-form">
          <Text fs={28} fw={700} lh={125} color="burgundy">
            Contact Us
          </Text>
          <div className="form-input-container">
            <Text fs={12} fw={400} lh={125} color="burgundy">
              Name
            </Text>

            <input className="form-input" id="name" />
          </div>
          <div className="form-input-container">
            <label htmlFor="email">
              <Text fs={12} fw={400} lh={125} color="burgundy">
                Email
              </Text>
            </label>
            <input className="form-input" id="email" />
          </div>
          <div className="form-input-container">
            <label htmlFor="message">
              <Text fs={12} fw={400} lh={125} color="burgundy">
                Message
              </Text>
            </label>
            <textarea className="form-input text-area" id="message" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ContactPage;
