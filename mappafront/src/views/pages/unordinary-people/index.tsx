import { getUnordinaryPage } from "@/actions/unordinary-people";
import { UnordinaryPageResponseDataItem } from "@/models/unordinary-people";
import Button from "@/views/components/button";
import MappaModal from "@/views/components/modal";
import Table from "@/views/components/table";
import Text from "@/views/components/text";
import { useEffect, useState } from "react";

import "./style.scss";

const UnunordinaryPeoplePage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedData, setSelectedData] =
    useState<UnordinaryPageResponseDataItem>();
  const [tableData, setTableData] =
    useState<UnordinaryPageResponseDataItem[]>();
  const [tablePage, setTablePage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>();

  function openModal(data: UnordinaryPageResponseDataItem) {
    setIsOpen(true);
    setSelectedData(data);
  }

  function closeModal() {
    setIsOpen(false);
  }

  const setInitialData = async () => {
    const response = await getUnordinaryPage({
      pageSize: 10,
      pageNumber: 1,
    });

    if (response.status === 200) {
      setTableData(response.data.data);
      setTotalPage(response.data.totalPages);
    }
  };

  const updateData = async () => {
    const response = await getUnordinaryPage({
      pageNumber: tablePage,
      pageSize: 10,
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
    "Death Year",
    "Death Place",
  ];

  useEffect(() => {
    setInitialData();
  }, []);

  useEffect(() => {
    updateData();
  }, [tablePage]);

  return (
    <section className="section unordinary-section">
      <div className="container">
        <div className="page-head">
          <Text fs={36} fw={700} lh={125} color="papirus">
            Ununordinary People
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
            rows: tableData?.map((unordinary) => {
              const cellTexts = [
                unordinary.name,
                unordinary.alternateName,
                unordinary.ethnicity?.name,
                unordinary.religion?.name,
                Array.isArray(unordinary.deathYear)
                  ? unordinary.deathYear[0]
                  : unordinary.deathYear,
                unordinary.deathPlace?.name,
              ];
              return {
                cells: cellTexts.map((cellText) => (
                  <Text fs={12} fw={500} lh={125} color="dark-gray">
                    {cellText! || "-"}
                  </Text>
                )),
                onClick: () => openModal(unordinary),
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
export default UnunordinaryPeoplePage;
