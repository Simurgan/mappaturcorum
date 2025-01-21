import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

import MappaLogo from "@/assets/images/mappa-logo.png";
import searchIcon from "@/assets/icons/search.svg";
import LanguageSelector from "@/components/LanguageSelector";

const Header = () => {
  return (
    <header className="border-t-[25px] border-[#B22B24] h-[167px] bg-[#FFFAEA] absolute top-0 left-0 right-0">
      <div className="flex w-full justify-between px-[192px] pt-[40px]">
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
            <LanguageSelector />
          </div>
        </div>
      </div>
      <nav className="flex justify-end gap-[41px] pr-[192px] text-[#B22B24] text-[21px] font-medium leading-[24px]">
        <p>Home</p>
        <p>About</p>
        <p>People</p>
        <p>Publications</p>
        <p>Contant</p>
      </nav>
    </header>
  );
};
export default Header;
