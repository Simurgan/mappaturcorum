import bgImage from "@/assets/images/katalan-atlasi.jpg";

const BannerSection = () => {
  return (
    <section
      style={{ backgroundImage: `url(${bgImage})` }}
      // className="bg-[length:100%_100%]"
      className="section bg-cover bg-center"
    >
      <div className="container"></div>
    </section>
  );
};
export default BannerSection;
