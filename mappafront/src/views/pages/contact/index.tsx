import Text from "@/views/components/text";
import "./style.scss";
import { Link } from "react-router-dom";

const ContactPage = () => {
  return (
    <div className="section">
      <div className="container">
        <div className="contact-header">
          <Text fs={36} fw={700} lh={125} color="burgundy">
            Contact
          </Text>
          <Link
            to={"mailto:info@mappaanatolicum.com"}
            className="contact-email"
          >
            <Text fs={24} fw={400} lh={125} color="burgundy">
              info@mappaanatolicum.com
            </Text>
          </Link>
          <Text fs={24} fw={400} lh={125} color="burgundy">
            +90 123 456 78 90
          </Text>
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
            <textarea className="form-input" id="message" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ContactPage;
