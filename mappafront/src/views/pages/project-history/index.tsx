import Text from "@/views/components/text";
import "./style.scss";

const ProjectHistoryPage = () => {
  return (
    <section className="section project-history-section">
      <div className="container">
        <Text tag="h1" fs={36} fw={500} lh={125} color="burgundy">
          Project History
        </Text>
        <div className="project-history-content">
          <div className="content-group">
            <Text tag="h2" fs={24} fw={500} lh={125} color="burgundy">
              Beginning and Data Collecting Processes
            </Text>
            <div className="content">
              <Text tag="p" fs={16}>
                Mappa Anatolicorum is a student-initiative digital humanities
                project, originally conceived as an idea by Muzaffer Çakır, a
                history student at Boğaziçi University. Emirhan Kuşaksız,
                another history student at Boğaziçi University associated with
                him. With his participation it became more structured and new
                ideas were brought. Over time, with the participation of Gökçe
                Yılmaz and Şeyma Sarı who are the Turkish language and
                literature students at Boğaziçi University, the project evolved
                into an organized team, which officially began its work in March
                2023. This small, independent, and non-funded group initially
                focused on discussing their readings through weekly meetings
                based on selected readings.
              </Text>
              <Text tag="p" fs={16}>
                In the first phase, team members engaged in general
                methodological readings, covering a wide range of fields, from
                microhistory to social history, from cultural history to the
                history of Sufism. Throughout this process, fundamental
                methodological principles of the project were established, and
                team members gained methodological tools to analyze primary
                sources. For instance, separate methodological readings were
                conducted for menâkıbnâme (hagiographical) texts, and general
                theories from cultural history and microhistory were discussed.
              </Text>
              <Text tag="p" fs={16}>
                In the next phase, team members began reading secondary sources
                relevant to the field. Discussions based on these readings
                helped clarify the frameworks for classifying, interpreting, and
                conceptualizing the collected data, particularly within the
                context of Anatolia and the Late Middle Ages.
              </Text>
              <Text tag="p" fs={16}>
                Subsequently, the team proceeded to analyze primary sources from
                the period. Data extracted from these readings were collected,
                classified, and organized accordingly.
              </Text>
              <Text tag="p" fs={16}>
                Throughout all these phases—from methodological readings to
                primary source analysis—our team members consulted with
                professors, particularly those from the History Department and
                the Turkish Language and Literature Department at Boğaziçi
                University, as well as scholars from other universities. Major
                questions, such as reconstructing ethnicity in a pre-modern
                period, issues of religious conversion, the ordinariness of
                individuals in historical sources, and other specific
                case-related problems, were discussed and resolved with the
                guidance of these scholars. You can check our collaborations
                list.
              </Text>
            </div>
          </div>
          <div className="content-group">
            <Text tag="h2" fs={24} fw={500} lh={125} color="burgundy">
              Digitalizing
            </Text>
            <div className="content">
              <Text tag="p" fs={16}>
                January 2024 marked a significant turning point for Mappa
                Anatolicorum. The team began to use Nodegoat as the most
                suitable tool to model, structure, and visualize their data, and
                adopted it as their primary tool. The collected data was then
                digitalized within the models created by the project team using
                this tool. The team members transferred the collected data into
                the system and integrated new data, gradually building a
                comprehensive database.
              </Text>
              <Text tag="p" fs={16}>
                Meanwhile, new members joined the team. In August of the same
                year, newly joined researchers were introduced to the Nodegoat
                system by Muzaffer Çakır and Emirhan Kuşaksız. Shortly
                thereafter, one of these researchers, Mehmet Akif Top, a history
                student at Istanbul University, joined the coordination team in
                the project alongside Emirhan Kuşaksız and Muzaffer Çakır.
                Creation of this coordinating-team with 3 coordinators evolved
                the project to a more organized structure. At the same time,
                with the contributions of other researchers, İsmail Zahid
                Demirtaş, Gökçe Yılmaz, and Şeyma Sarı, data continued to be
                collected, grouped, and organized within the Nodegoat system.
                Every member of the project brought their own perceptions and
                research skills to the project at every point.
              </Text>
            </div>
          </div>
          <div className="content-group">
            <Text tag="h2" fs={24} fw={500} lh={125} color="burgundy">
              Technical Team
            </Text>
            <div className="content">
              <Text tag="p" fs={16}>
                In August 2024, Ömer Şükrü Uyduran and Mehmed Said Yolcu,
                computer engineering students at Boğaziçi University, along with
                Burak Kızılay, a software engineering master's student at Manisa
                Celal Bayar University, joined Mappa Anatolicorum. Together,
                they formed the developer team and voluntarily began working on
                the backend and frontend software for the digitalized data of
                the project. At the same time, Seher Doğan, a graphic design
                student at Hacettepe University, joined the project as a web
                designer.
              </Text>
            </div>
          </div>
          <div className="content-group">
            <Text tag="h2" fs={24} fw={500} lh={125} color="burgundy">
              Publishing the Project
            </Text>
            <div className="content">
              <Text tag="p" fs={16}>
                By February 2025, the project team had completed a significant
                stage of the work. On February 26, 2025, Mappa Anatolicorum was
                officially launched as an open-source platform at Boğaziçi
                University, with a presentation by the project founder, Muzaffer
                Çakır. Since then, researchers and enthusiasts have continued to
                utilize Mappa Anatolicorum.
              </Text>
            </div>
          </div>
          <div className="content-group">
            <Text tag="h2" fs={24} fw={500} lh={125} color="burgundy">
              Future
            </Text>
            <div className="content">
              <Text tag="p" fs={16}>
                Mappa Anatolicorum will continue to be updated with new
                historical data. Thus, this is not a final conclusion but rather
                a dynamic project that will incorporate new sources and data in
                the near future.
              </Text>
              <Text tag="p" fs={16}>
                Additionally, the coordinators of Mappa Anatolicorum are
                currently working on an academic article including the content,
                problems, solutions, theories, and contributions.
              </Text>
              <Text tag="p" fs={16}>
                The Mappa Anatolicorum team remains open to new projects and
                collaborations in the future, aiming to contribute to the
                development of digital humanities methodologies in Turkish
                historiography.
              </Text>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};
export default ProjectHistoryPage;
