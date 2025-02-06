import Text from "@/views/components/text";
import "./style.scss";
import { Urls } from "@/routers/routes";
import { NavLink } from "react-router-dom";

const DataCardsSection = () => {
  return (
    <section className="section data-cards-section">
      <div className="container">
        <NavLink to={Urls.OrdinaryPeople} className="data-nav-item">
          <div
            className="data-card"
            style={{
              background: `url(${require("@/assets/images/ordinary-people.jpg")})`,
              backgroundSize: "cover",
              backgroundPosition: "center",
            }}
          >
            <div className="card-frame">
              <div className="card-title-container">
                <Text
                  fw={700}
                  fs={18}
                  lh={125}
                  isCenter={true}
                  color="burgundy"
                >
                  Ordinary People
                </Text>
              </div>
            </div>
          </div>
        </NavLink>
        <NavLink to={Urls.UnordinaryPeople} className="data-nav-item">
          <div
            className="data-card"
            style={{
              background: `url(${require("@/assets/images/unordinary-people.jpg")})`,
              backgroundSize: "cover",
              backgroundPosition: "center",
            }}
          >
            <div className="card-frame">
              <div className="card-title-container">
                <Text
                  fw={700}
                  fs={18}
                  lh={125}
                  isCenter={true}
                  color="burgundy"
                >
                  Unordinary People
                </Text>
              </div>
            </div>
          </div>
        </NavLink>
        <NavLink to={Urls.Sources} className="data-nav-item">
          <div
            className="data-card"
            style={{
              background: `url(${require("@/assets/images/sources.jpg")})`,
              backgroundSize: "cover",
              backgroundPosition: "center",
            }}
          >
            <div className="card-frame">
              <div className="card-title-container">
                <Text
                  fw={700}
                  fs={18}
                  lh={125}
                  isCenter={true}
                  color="burgundy"
                >
                  Sources
                </Text>
              </div>
            </div>
          </div>
        </NavLink>
      </div>
    </section>
  );
};
export default DataCardsSection;
