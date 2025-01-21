import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

import MappaLogo from "@/assets/images/mappa-logo.png";
import searchIcon from "@/assets/icons/search.svg";
import { Urls } from "@/routers/routes";
import { NavLink } from "react-router-dom";

const Header = () => {
  return (
    <header className="border-t-[25px] border-[#B22B24] h-[167px] bg-[#FFFAEA] w-full pt-[40px] relative shadow-lg">
      <div className="flex flex-col max-w-[1440px] px-[80px] mx-auto pb-[5px]">
        <div className="header-content flex w-full justify-between">
          <div>
            <img src={MappaLogo} width={205} height={86} />
          </div>
          <div className="flex gap-8">
            <div className="relative h-[30px]">
              <Input
                placeholder="Search"
                className="border-[#B22B24] rounded-3xl h-[30px]"
              />
              <img
                src={searchIcon}
                width={20}
                height={20}
                className="absolute bottom-[4px] right-3"
              />
            </div>
            <div className="flex gap-8">
              <Button
                variant={"default"}
                className="w-[110px] h-[30px]  text-white rounded-3xl"
              >
                Sign Up
              </Button>
            </div>
          </div>
        </div>
        <div className="nav-container flex justify-end">
          <nav className="flex gap-[41px] text-[#B22B24] text-[21px] font-medium leading-[24px]">
            <NavLink to={Urls.Home}>Home</NavLink>
            <NavLink to={Urls.About}>About</NavLink>
            <NavLink to={Urls.Members}>People</NavLink>
            <NavLink to={Urls.Publications}>Publications</NavLink>
            <NavLink to={Urls.Contact}>Contant</NavLink>
          </nav>
        </div>
      </div>
    </header>
  );
};
export default Header;
