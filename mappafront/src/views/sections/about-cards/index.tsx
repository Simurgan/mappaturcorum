import { NavLink } from "react-router-dom";
import "./style.scss";
import { Urls } from "@/routers/routes";
import Text from "@/views/components/text";

const AboutCardsSection = () => {
  return (
    <section className="section about-cards-section">
      <div className="container">
        <NavLink to={Urls.ProjectHistory} className="about-nav-item">
          <div className="about-card">
            <div className="img-container">
              <img
                src={require("@/assets/images/project-history.jpg")}
                className="about-img"
              />
            </div>
            <div className="text-container">
              <Text fw={500} fs={28} lh={125} color="black">
                Project History
              </Text>
              <Text fw={400} fs={22} lh={125} color="dark-gray">
                Explore the story of Mappa Anatolicorum...
              </Text>
            </div>
          </div>
        </NavLink>
        <NavLink to={"https://x.com/manatolicorum"} className="about-nav-item">
          <div className="about-card">
            <div className="img-container">
              <img
                src={require("@/assets/images/announcements.jpg")}
                className="about-img"
              />
            </div>
            <div className="text-container">
              <Text fw={500} fs={28} lh={125} color="black">
                Announcements
              </Text>
              <Text fw={400} fs={22} lh={125} color="dark-gray">
                Be notified about the announcements by Mappa Anatolicorum,
                including new member hirings for the future projects!
              </Text>
            </div>
          </div>
        </NavLink>
      </div>
    </section>
  );
};
export default AboutCardsSection;
