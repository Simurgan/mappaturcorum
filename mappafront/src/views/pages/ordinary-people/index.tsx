import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";
import Table from "@/views/components/table";

import { useEffect, useState } from "react";
import MappaModal from "@/views/components/modal";
import { getOrdinaryPage } from "@/actions/ordinary";
import { OrdinaryPageResponseDataItem } from "@/models/ordinary-people";

const OrdinaryPeoplePage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedData, setSelectedData] = useState();
  const [tableData, setTableData] = useState<OrdinaryPageResponseDataItem[]>();
  const [tablePage, setTablePage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>();

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
    });

    if (response.status === 200) {
      setTableData(response.data.data);
      setTotalPage(response.data.totalPages);
    }
  };

  const headerData = [
    "Name",
    "Alternate Name",
    "Ethnonym",
    "Religion",
    "Profession",
    "Gender",
  ];

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
          paginationData={
            totalPage
              ? {
                  currentPage: tablePage,
                  setPage: setTablePage,
                  totalPage: totalPage,
                }
              : undefined
          }
          tableData={{
            hasRowHover: true,
            headers: headerData.map((cell) => (
              <Text fs={14} fw={500} lh={125} color="burgundy">
                {cell}
              </Text>
            )),
            rows: tableData?.map((ordinary) => {
              const cellTexts = [
                ordinary.name,
                ordinary.alternateName,
                ordinary.ethnicity?.name,
                ordinary.religion?.name,
                ordinary.profession?.name,
                ordinary.gender?.name,
              ];
              return {
                cells: cellTexts.map((cellText) => (
                  <Text fs={12} fw={500} lh={125} color="dark-gray">
                    {cellText}
                  </Text>
                )),
                onClick: () => openModal({}),
              };
            }),
          }}
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
