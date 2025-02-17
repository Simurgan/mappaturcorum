import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";
import Table from "@/views/components/table";
import {
  ordinaryTableData,
  ordinaryTableHeaders,
} from "@/helpers/data/ordinary-people";

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
        <Table
          tableHeaders={ordinaryTableHeaders}
          tableData={ordinaryTableData}
        />
      </div>
    </section>
  );
};
export default OrdinaryPeoplePage;
