import {
  getUnordinaryPage,
  getUnordinaryPerson,
} from "@/actions/unordinary-people";
import {
  UnordinaryPageResponseDataItem,
  UnordinaryPersonResponseType,
} from "@/models/unordinary-people";
import Button from "@/views/components/button";
import Table from "@/views/components/table";
import Text from "@/views/components/text";
import { useEffect, useState } from "react";

import "./style.scss";
import ReactModal from "react-modal";

const UnordinaryPeoplePage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedPersonId, setSelectedPersonId] = useState<number>();
  const [modalData, setModalData] = useState<UnordinaryPersonResponseType>();
  const [tableData, setTableData] =
    useState<UnordinaryPageResponseDataItem[]>();
  const [tablePage, setTablePage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>();

  async function openModal(id: number) {
    setSelectedPersonId(id);
    setIsOpen(true);
  }

  function closeModal() {
    setIsOpen(false);
  }

  const setInitialData = async () => {
    const response = await getUnordinaryPage({
      pageSize: 20,
      pageNumber: 1,
    });

    if (response.status === 200) {
      setTableData(response.data.data);
      setTotalPage(response.data.totalPages);
    }
  };

  useEffect(() => {
    const fetchSelectedData = async () => {
      const response = await getUnordinaryPerson(Number(selectedPersonId!));
      if (response.status === 200) {
        setModalData(response.data);
      }
    };

    if (selectedPersonId !== undefined) {
      fetchSelectedData();
    }
  }, [selectedPersonId]);

  const updateData = async () => {
    const response = await getUnordinaryPage({
      pageNumber: tablePage,
      pageSize: 20,
    });

    if (response.status === 200) {
      setTableData(response.data.data);
      setTotalPage(response.data.totalPages);
    }
  };

  const headerData = ["Name", "Alternate Name", "Ethnicity", "Religion"];

  useEffect(() => {
    setInitialData();
  }, []);

  useEffect(() => {
    updateData();
  }, [tablePage]);

  return (
    <section className="section unordinary-section">
      <div className="page-head">
        <div className="container">
          <Text fs={36} fw={700} lh={125} color="papirus">
            Notable People
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
      <div className="container">
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
                  /* unordinary.gender || */
                  unordinary.ethnicity?.name,
                  unordinary.religion?.name,
                  /* unordinary.profession.name || */
                ];
                return {
                  cells: cellTexts.map((cellText) => (
                    <Text fs={12} fw={500} lh={125} color="dark-gray">
                      {cellText! || "-"}
                    </Text>
                  )),
                  onClick: () => openModal(unordinary.id),
                };
              }),
            }}
          />
        </div>
      </div>

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
            <img src={require("@/assets/icons/close-icon.svg")} />
          </Button>

          {/* Başlık */}
          {/* <Text fs={18} fw={500} color="black">
            Ordinary Person
          </Text> */}

          <div className="content">
            <Text fs={28} fw={700} lh={140} color="burgundy">
              {modalData?.name || "No Name Provided"}
            </Text>

            <div className="content-info">
              {modalData && Object.keys(modalData).length > 0 ? (
                Object.entries(modalData)
                  .filter(([key]) => key.toLowerCase() !== "id")
                  .map(([key, value], index) => {
                    let displayValue = "-";

                    if (Array.isArray(value)) {
                      displayValue =
                        value.length > 0
                          ? value
                              .map((item: any) => item.name || item.toString())
                              .join(", ")
                          : "-";
                    } else if (typeof value === "object" && value !== null) {
                      displayValue = value.name || "-";
                    } else {
                      displayValue = value?.toString() || "-";
                    }

                    return (
                      <div key={index} className="info-row">
                        <Text fs={16} fw={500} lh={125} classNames="data-field">
                          {key.toString()}
                        </Text>
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {displayValue}
                        </Text>
                      </div>
                    );
                  })
              ) : (
                <Text fs={14} fw={400} lh={125} color="gray">
                  No data available
                </Text>
              )}
            </div>
          </div>
        </div>
      </ReactModal>
    </section>
  );
};
export default UnordinaryPeoplePage;
