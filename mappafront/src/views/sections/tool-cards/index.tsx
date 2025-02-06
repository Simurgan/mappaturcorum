import { NavLink } from "react-router-dom";
import "./style.scss";
import { Urls } from "@/routers/routes";
import Text from "@/views/components/text";

const ToolCardsSection = () => {
  return (
    <section className="section tool-cards-section">
      <div className="container">
        <NavLink to={Urls.Map} className="tool-nav-item">
          <div
            className="tool-card"
            style={{
              background: `linear-gradient(
                90deg,
                rgba(68, 20, 0, 0.2),
                rgba(68, 20, 0, 0.2)
              ), url(${require("@/assets/images/katalan-atlasi.jpg")})`,
              backgroundSize: "cover",
              backgroundPosition: "center",
            }}
          >
            <div className="card-frame">
              <div className="card-title-container">
                <Text
                  fw={500}
                  fs={24}
                  lh={125}
                  isCenter={true}
                  color="burgundy"
                >
                  Map
                </Text>
              </div>
            </div>
          </div>
        </NavLink>
        <NavLink to={Urls.SocialNetwork} className="tool-nav-item">
          <div
            className="tool-card"
            style={{
              background: `linear-gradient(
                90deg,
                rgba(68, 20, 0, 0.2),
                rgba(68, 20, 0, 0.2)
              ), url(${require("@/assets/images/graph.jpg")})`,
              backgroundSize: "cover",
              backgroundPosition: "center",
            }}
          >
            <div className="card-frame">
              <div className="card-title-container">
                <Text
                  fw={500}
                  fs={24}
                  lh={125}
                  isCenter={true}
                  color="burgundy"
                >
                  Social Network
                </Text>
              </div>
            </div>
          </div>
        </NavLink>
      </div>
    </section>
  );
};
export default ToolCardsSection;
