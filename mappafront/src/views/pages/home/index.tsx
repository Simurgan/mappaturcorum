import AboutCardsSection from "@/views/sections/about-cards";
import BannerSection from "@/views/sections/banner";
import DataCardsSection from "@/views/sections/data-cards";
import HomeAboutSection from "@/views/sections/home-about";
import ToolCardsSection from "@/views/sections/tool-cards";

const HomePage = () => {
  return (
    <section className="page-content">
      <BannerSection />
      <HomeAboutSection />
      <DataCardsSection />
      <ToolCardsSection />
      <AboutCardsSection />
    </section>
  );
};
export default HomePage;
