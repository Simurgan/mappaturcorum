import AboutCardsSection from "@/views/sections/about-cards";
import BannerSection from "@/views/sections/banner";
import DataCardsSection from "@/views/sections/data-cards";
import HomeAboutSection from "@/views/sections/home-about";
import ToolCardsSection from "@/views/sections/tool-cards";
import "./style.scss";

const HomePage = () => {
  return (
    <section
      className="home-page-content"
      style={{
        background: `linear-gradient(
                180deg,
              #FFF9E9 85%,
              rgba(255, 249, 233, 0.650485) 92%,
              rgba(255, 249, 233, 0) 105%
              ), url(${require("@/assets/images/motif.jpg")})`,
        backgroundRepeat: "no-repeat",
        backgroundSize: "contain",
        backgroundPosition: "0 60%",
      }}
    >
      <BannerSection />
      <HomeAboutSection />
      <DataCardsSection />
      <ToolCardsSection />
      <AboutCardsSection />
    </section>
  );
};
export default HomePage;
