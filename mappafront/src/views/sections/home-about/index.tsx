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
          Mappa Anatolicorum is a digital humanities project dedicated to
          reconstructing the ordinary life of Late Medieval Anatolia1
          (1200-1450). Starting its journey in March 2023, the project aims to
          provide an extensive, well-structured database indexing the ordinary
          people, primary sources, and settlements of the period. Mappa
          Anatolicorum presents its data-base, which is built by data-mining of
          the historical primary sources of the field of study of the project,
          through social network analysis and geographical information systems
          (GIS). A key goal of the project is to offer reliable, open-source
          data to researchers and enthusiasts interested in the social fabric of
          Medieval Anatolia.
        </Text>

        <Link to={Urls.About} className="read-more">
          <Text fw={300} fs={24} lh={125} isCenter={true}>
            Read more...
          </Text>
        </Link>
      </div>
    </section>
  );
};
export default HomeAboutSection;
