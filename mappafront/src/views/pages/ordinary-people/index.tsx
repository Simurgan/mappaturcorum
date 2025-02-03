import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";
import Table from "@/views/components/table";

const OrdinaryPeoplePage = () => {
  return (
    <section className="section ordinary-section">
      <div className="container">
        <div className="page-head">
          <Text fs={36} fw={700} lh={125} color="papirus">
            Ordinary People
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
      <div className="content">
        <div className="table-container">
          <Table
            headers={["ID", "Name"]}
            data={[
              { id: 1, name: "Burak", school: "Celal Bayar University" },
              { id: 2, name: "Ömer", school: "Boğaziçi University" },
              { id: 3, name: "Burak", school: "Celal Bayar University" },
              { id: 4, name: "Ömer", school: "Boğaziçi University" },
              { id: 5, name: "Burak", school: "Celal Bayar University" },
              { id: 6, name: "Ömer", school: "Boğaziçi University" },
              { id: 7, name: "Burak", school: "Celal Bayar University" },
              { id: 8, name: "Ömer", school: "Boğaziçi University" },
            ]}
          />
        </div>
      </div>
    </section>
  );
};
export default OrdinaryPeoplePage;
