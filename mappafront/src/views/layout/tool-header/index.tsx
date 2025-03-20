import negativeLogo from "@/assets/images/mappa-logo-negative.png";
import { Urls } from "@/routers/routes";
import {Link, NavLink} from "react-router-dom";
import "./style.scss";
import Text from "@/views/components/text";

const ToolHeader = () => {
  return (
    <header className="section tool-header-section">
      <div className="container">
        <div className="logo-container">
          <Link to={Urls.Home}>
          <img
            src={negativeLogo}
            alt="Mapp Anatolicorum logo"
            className="negative-logo"
          />
          </Link>
        </div>
        <nav className="tools-nav flex gap-[88px] text-secondary font-medium text-[18px]">
          <NavLink to={Urls.Home} className="nav-item">
            <Text fw={500} fs={18} lh={125} color="papirus">
              Home
            </Text>
          </NavLink>
          <NavLink to={Urls.About} className="nav-item">
            <Text fw={500} fs={18} lh={125} color="papirus">
              About
            </Text>
          </NavLink>
        </nav>
      </div>
    </header>
  );
};
export default ToolHeader;
