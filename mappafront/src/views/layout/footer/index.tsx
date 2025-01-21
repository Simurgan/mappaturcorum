import { Link } from "react-router-dom";

import linkedinLogo from "@/assets/icons/linkedin-logo.svg";
import MappaLogo from "@/assets/images/mappa-logo-negative.png";

const Footer = () => {
  return (
    <footer className="w-full  bg-primary h-[137px] text-white pt-[22px] pb-[50px]">
      <div className="w-full flex items-center justify-between px-[210px] max-w-[1440px]">
        <div className="flex gap-[195px]">
          <img src={MappaLogo} width={164} height={66} alt="Mappa Logo" />
          <address className="flex flex-col gap-[5px] not-italic">
            <p className="text-[20px] font-medium leading-6">Contact Us</p>
            <Link
              to="mailto:info@mappaanatolicum.com"
              className="text-[14px] font-normal leading-[18px]"
            >
              info@mappaanatolicum.com
            </Link>
          </address>
        </div>
        <div className="flex flex-col items-center justify-center">
          {/* <LanguageSelector /> */}
          <div className="flex gap-6">
            <p>Follow us on</p>
            <div className="flex items-center gap-[5px]">
              <Link
                to="https://www.linkedin.com"
                target="_blank"
                rel="noopener noreferrer"
              >
                <img
                  src={linkedinLogo}
                  width={15}
                  height={12}
                  alt="LinkedIn Logo"
                />
              </Link>
              <Link
                to="https://www.linkedin.com"
                target="_blank"
                rel="noopener noreferrer"
              >
                <img
                  src={linkedinLogo}
                  width={15}
                  height={12}
                  alt="LinkedIn Logo"
                />
              </Link>
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
