import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";
import Table from "@/views/components/table";
import {
  ordinaryTableData,
  ordinaryTableHeaders,
} from "@/helpers/data/ordinary-people";

import { useEffect, useState } from "react";
import MappaModal from "@/views/components/modal";
import { getOrdinaryPage } from "@/actions/ordinary";

const OrdinaryPeoplePage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedData, setSelectedData] = useState();

  function openModal(data: any) {
    setIsOpen(true);
    setSelectedData(data);
  }

  function closeModal() {
    setIsOpen(false);
  }

  const setInitialData = async () => {
    const response = await getOrdinaryPage({
      pageSize: 10,
      pageNumber: 1,
      filter: {},
    });

    console.log(response);
    console.log("response");
  };

  useEffect(() => {
    setInitialData();
  }, []);

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
          openModal={(data) => openModal(data)}
          tableHeaders={ordinaryTableHeaders}
          tableData={ordinaryTableData}
        />
      </div>
      <MappaModal
        isOpen={modalIsOpen}
        closeModal={closeModal}
        data={selectedData}
      />
    </section>
  );
};
export default OrdinaryPeoplePage;
