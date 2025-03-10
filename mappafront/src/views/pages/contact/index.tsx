import Text from "@/views/components/text";
import { Link } from "react-router-dom";

// import mailIcon from "@/assets/icons/mail-icon.svg";
// import phoneIcon from "@/assets/icons/phone-icon.svg";

import "./style.scss";

const ContactPage = () => {
  return (
    <section className="section contact-section">
      <div className="container">
        <div className="contact-header">
          <Text tag="h1" fs={36} fw={700} lh={125} color="burgundy">
            Contact
          </Text>
          <Text>
            Please contact us if you have any feedback that you want to inform
            us. We are looking forward to seeing your evaluations, comments,
            criticism, suggestions and so on.
          </Text>
          <Text>
            We are also open to receiveing requests from you to take place in
            our project, and for future project ideas as well. Please contact us
            if you would like to join us!
          </Text>
          <div className="contact-info">
            {/* <img src={mailIcon} width={24} height={18} /> */}
            <Link
              to={"mailto:info@mappaanatolicum.com"}
              className="contact-email"
            >
              <Text fs={24} fw={400} lh={125} color="burgundy">
                info@mappateam.com
              </Text>
            </Link>
            {/* <img src={phoneIcon} width={24} height={18} /> */}
            <Link to={"tel:+905511127399"}>
              <Text fs={24} fw={400} lh={125} color="burgundy">
                +90 551 112 7399
              </Text>
            </Link>
          </div>
        </div>
        {/* <div className="contact-form">
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
        </div> */}
      </div>
    </section>
  );
};

export default ContactPage;
