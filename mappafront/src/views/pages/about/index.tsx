import Text from "@/views/components/text";
import "./style.scss";

const AboutPage = () => {
  return (
    <section className="section about-section">
      <div className="container">
        <Text tag="h1" fs={36} fw={500} lh={125} color="burgundy">
          About Mappa Anatolicorum
        </Text>
        <div className="about-content">
          <div className="content-section">
            <Text tag="p" fs={16}>
              Mappa Anatolicorum is a digital humanities project dedicated to
              reconstructing the ordinary life of Late Medieval Anatolia1
              (1200-1450). Starting its journey in March 2023, the project aims
              to provide an extensive, well-structured database indexing the
              ordinary people, primary sources, and settlements of the period.
              Mappa Anatolicorum presents its data-base, which is built by
              data-mining of the historical primary sources of the field of
              study of the project, through social network analysis and
              geographical information systems (GIS). A key goal of the project
              is to offer reliable, open-source data to researchers and
              enthusiasts interested in the social fabric of Medieval Anatolia.
            </Text>
            <Text tag="p" fs={16}>
              Our small yet dedicated team of undergraduate voluntary
              researchers carefully read primary works of the era, collect,
              classify, and organizy the data, making them easily accessible
              through dynamic and interactive GIS map and social network
              analysis graphs. These tools allow users to conduct rapid
              searching, understanding, bibliography searches and analyze
              intricate relationships between ordinary people, and between
              sources and settlements of the era.
            </Text>
            <Text tag="p" fs={16}>
              We are thrilled to share this project with the academic community
              and the public, fostering new research opportunities and deeper
              insights into Anatolia’s rich medieval history. We do not mark an
              ultimate end to the project by making it open source but we will
              be editing and expanding the database as much as possible
              throughout time. By providing an open-access platform, Mappa
              Anatolicorum aspires to be a valuable resource for those seeking
              to understand the past through digital innovation.
            </Text>
          </div>
        </div>
      </div>
    </section>
  );
};
export default AboutPage;
