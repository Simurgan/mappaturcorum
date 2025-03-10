import MappaLogo from "@/assets/images/mappa-logo.png";
// import searchIcon from "@/assets/icons/search.svg";
import { Urls } from "@/routers/routes";
import { Link, NavLink } from "react-router-dom";
import "./style.scss";
import Text from "@/views/components/text";

const Header = () => {
  return (
    <header className="section header-section">
      <div className="container">
        {/* <div className="header-content"> */}
        <div className="logo-container">
          <Link to={Urls.Home}>
            <img src={MappaLogo} className="logo-image" />
          </Link>
        </div>
        {/* <div className="header-actions">
            <div className="search-container">
              <input placeholder="Search" className="search-input" />
              <img src={searchIcon} className="search-icon" />
            </div>
            <Button style={"primary"}>Sign Up</Button>
          </div> */}
        {/* </div> */}
        <nav className="nav">
          <NavLink to={Urls.Home} className={"nav-item"}>
            <Text tag="p" fw={500} fs={18} lh={125} color="burgundy">
              Home
            </Text>
          </NavLink>
          <NavLink to={Urls.About} className={"nav-item"}>
            <Text tag="p" fw={500} fs={18} lh={125} color="burgundy">
              About
            </Text>
          </NavLink>
          <NavLink to={Urls.ProjectHistory} className={"nav-item"}>
            <Text tag="p" fw={500} fs={18} lh={125} color="burgundy">
              Project History
            </Text>
          </NavLink>
          <NavLink to={Urls.Collaborations} className={"nav-item"}>
            <Text tag="p" fw={500} fs={18} lh={125} color="burgundy">
              Collaborations
            </Text>
          </NavLink>
          <NavLink to={Urls.Members} className={"nav-item"}>
            <Text tag="p" fw={500} fs={18} lh={125} color="burgundy">
              Members
            </Text>
          </NavLink>
          {/* <NavLink to={Urls.Publications} className={"nav-item"}>
            <Text tag="p" fw={500} fs={18} lh={125} color="burgundy">
              Publications
            </Text>
          </NavLink> */}
          <NavLink to={Urls.Contact} className={"nav-item"}>
            <Text tag="p" fw={500} fs={18} lh={125} color="burgundy">
              Contact
            </Text>
          </NavLink>
        </nav>
      </div>
    </header>
  );
};
export default Header;
