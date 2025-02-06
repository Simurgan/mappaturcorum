import bgImage from "@/assets/images/katalan-atlasi.jpg";
import Text from "@/views/components/text";
import "./style.scss";

const BannerSection = () => {
  return (
    <section
      style={{
        background: `linear-gradient(
          90deg,
          rgba(255, 251, 240, 0.8) 6.5%,
          rgba(255, 251, 240, 0.5) 29%,
          rgba(255, 251, 240, 0) 54.5%
        ), url(${bgImage})`,
        backgroundSize: "150%",
        backgroundPosition: "15% 30%",
      }}
      // className="bg-[length:100%_100%]"
      className="section banner-section"
    >
      <div className="container">
        <div className="banner-text">
          <Text tag="h1" fs={40} fw={300} lh={125} color="burgundy">
            Welcome to <br />
            <Text tag="span" fw={700} fs={40} lh={125} color="burgundy">
              MAPPA ANATOLICORUM
            </Text>
            <br />
            Explore the 1200-1450 Anatolia
          </Text>
        </div>
      </div>
    </section>
  );
};
export default BannerSection;
