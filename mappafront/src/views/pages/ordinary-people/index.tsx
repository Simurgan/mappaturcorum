import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";

const OrdinaryPeoplePage = () => {
  return (
    <section className="section ordinary-section">
      <div className="container">
        <div className="page-head">
          <Text fs={36} fw={700} lh={125} color="papirus">
            Unordinary People <br />
            <Text fs={22} fw={300} lh={125} color="papirus">
              in the context of their relationship with ordinary people
            </Text>
          </Text>

          <div className="page-head-actions-container">
            <Button classNames="action-button">
              <Text fs={20} fw={500} lh={125} color="burgundy">
                Map
              </Text>
            </Button>
            <Button classNames="action-button">
              <Text fs={20} fw={500} lh={125} color="burgundy">
                Social Network
              </Text>
            </Button>
            <input placeholder="Search on map.." className="action-input" />
          </div>
        </div>
      </div>
    </section>
  );
};
export default OrdinaryPeoplePage;
