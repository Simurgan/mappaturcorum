import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";
import Table from "@/views/components/table";

import { useEffect, useState } from "react";
import MappaModal from "@/views/components/modal";
import { getOrdinary, getOrdinaryPage } from "@/actions/ordinary-people";
import {
  OrdinaryPageResponseDataItem,
  SingleOrdinaryObject,
} from "@/models/ordinary-people";
import ReactModal from "react-modal";

const OrdinaryPeoplePage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedData, setSelectedData] = useState<SingleOrdinaryObject>();
  const [tableData, setTableData] = useState<OrdinaryPageResponseDataItem[]>();
  const [tablePage, setTablePage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>();

  function openModal(data: OrdinaryPageResponseDataItem) {
    setIsOpen(true);
    getSingleOrdinary(data.id);
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
      console.log(response.data.data);
      console.log("here");
    }
  };

  const updateData = async () => {
    const response = await getOrdinaryPage({
      pageNumber: tablePage,
      pageSize: 10,
    });

    if (response.status === 200) {
      setTableData(response.data.data);
      setTotalPage(response.data.totalPages);
    }
  };

  const getSingleOrdinary = async (ordinaryId: number) => {
    const response = await getOrdinary(ordinaryId);

    if (response.status === 200) {
      setSelectedData(response.data);
    }
  };

  const headerData = [
    "Name",
    "Alternate Name",
    "Religion",
    "Former Religion",
    "Ethnicity",
    "Profession",
    "Gender",
    "Sources",
    "Interactions With Unordinary",
    "Location",
  ];

  useEffect(() => {
    setInitialData();
  }, []);

  useEffect(() => {
    updateData();
  }, [tablePage]);

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
                ordinary.religion?.name,
                ordinary.formerReligion?.name,
                ordinary.ethnicity?.name,
                ordinary.profession?.name,
                ordinary.gender?.name,
                ordinary.sources.map((source) => source.name).join(", "),
                ordinary.interactionsWithUnordinary
                  .map((unordinary) => unordinary.name)
                  .join(", "),
                ordinary.location?.name,
              ];
              return {
                cells: cellTexts.map((cellText) => (
                  <Text fs={12} fw={500} lh={125} color="dark-gray">
                    {cellText}
                  </Text>
                )),
                onClick: () => openModal(ordinary),
              };
            }),
          }}
        />
      </div>
      {/* <MappaModal
        isOpen={modalIsOpen}
        closeModal={closeModal}
        data={selectedData}
      /> */}
      <ReactModal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Example Modal"
        overlayClassName="custom-overlay"
        className="custom-mappa-modal"
      >
        <div className="modal-content">
          {/* Kapatma Butonu */}
          <Button onClick={closeModal} classNames="modal-close-button">
            <Text fs={36} fw={400} color="burgundy">
              X
            </Text>
          </Button>

          {/* Başlık */}
          {/* <Text fs={18} fw={500} color="black">
            Ordinary Person
          </Text> */}

          <div className="content">
            <Text fs={28} fw={700} lh={140} color="burgundy">
              {selectedData?.name || "No Name Provided"}
            </Text>

            {/* <div className="content-info">
              {data && Object.keys(data).length > 0 ? (
                Object.keys(data).map((key, index) => (
                  <div key={index} className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      {key.toString()}
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {formatDataField(data, key)}
                    </Text>
                  </div>
                ))
              ) : (
                <Text fs={14} fw={400} lh={125} color="gray">
                  No data available
                </Text>
              )}
            </div> */}
          </div>
        </div>
      </ReactModal>
    </section>
  );
};
export default OrdinaryPeoplePage;
