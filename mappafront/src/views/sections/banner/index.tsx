import bgImage from "@/assets/images/katalan-atlasi.jpg";

const BannerSection = () => {
  return (
    <section
      style={{
        backgroundImage: `url(${bgImage})`,
      }}
      // className="bg-[length:100%_100%]"
      className="section bg-cover bg-center bg-gradient-to-l from-white"
    >
      <div className="container">
        <div className="banner-text">
          <h1 className="welcome-text text-primary text-[40px] font-light">
            Welcome to <br />{" "}
            <span className="font-bold">MAPPA ANATOLICORUM</span> <br />
            Explore the 1200-1450 Anatolia
          </h1>
        </div>
      </div>
    </section>
  );
};
export default BannerSection;
