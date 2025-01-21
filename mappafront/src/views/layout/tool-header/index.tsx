import negativeLogo from "@/assets/images/mappa-logo-negative.png";
import { Urls } from "@/routers/routes";
import { NavLink } from "react-router-dom";

const ToolHeader = () => {
  return (
    <header className="bg-primary py-[8px]">
      <div className="max-w-[1440px] px-[80px] flex justify-between items-center">
        <div className="logo-container">
          <img
            src={negativeLogo}
            alt="Mapp Anatolicorum logo"
            className="w-[92px] h-[36px]"
          />
        </div>
        <nav className="tools-nav flex gap-[88px] text-secondary font-medium text-[18px]">
          <NavLink to={Urls.Home}>Home</NavLink>
          <NavLink to={Urls.About}>About</NavLink>
        </nav>
      </div>
    </header>
  );
};
export default ToolHeader;
