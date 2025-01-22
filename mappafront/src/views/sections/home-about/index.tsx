import Text from "@/views/components/text";
import "./style.scss";
import { Link } from "react-router-dom";
import { Urls } from "@/routers/routes";

const HomeAboutSection = () => {
  return (
    <section className="section home-about-section">
      <div className="container">
        <Text tag="h2" fw={700} fs={32} lh={125} isCenter={true} color="black">
          About the Project
        </Text>
        <Text tag="p" fw={300} fs={24} lh={125} isCenter={true} color="gray">
          Mappa Anatolicorum is a digital humanities project for digitally
          recreating the ordinary people of 1200-1450 Anatolia and their
          ordinary lives with the map (GIS) and social network analysis. What
          else? Mappa Anatolicum provides a rich data base not only for ordinary
          people, but also for the sources mentioning about the 1200-1450
          Anatolia, and their elaborate and complex relations with the
          settlements in Anatolia.
        </Text>
        <Link to={Urls.About}>Read more...</Link>
      </div>
    </section>
  );
};
export default HomeAboutSection;
