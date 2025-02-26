import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";
import Table from "@/views/components/table";

import { useEffect, useState } from "react";
// import MappaModal from "@/views/components/modal";
import { getOrdinary, getOrdinaryPage } from "@/actions/ordinary-people";
import {
  OrdinaryPageResponseDataItem,
  SingleOrdinaryObject,
  SubObjectPair,
} from "@/models/ordinary-people";
import ReactModal from "react-modal";
import { useNavigate } from "react-router-dom";
import { Urls } from "@/routers/routes";

const OrdinaryPeoplePage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedData, setSelectedData] = useState<SingleOrdinaryObject>();
  const [tableData, setTableData] = useState<OrdinaryPageResponseDataItem[]>();
  const [tablePage, setTablePage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>();

  const navigate = useNavigate();

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
            <Button
              classNames="action-button"
              onClick={() => navigate(Urls.Map)}
            >
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

            <div className="content-info">
              {/* {data && Object.keys(data).length > 0 ? (
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
              )} */}

              {selectedData?.alternateName && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Alternate Name(s):
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.alternateName}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.ethnicity && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Ethnicity:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.ethnicity.name}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.religion && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Religion:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.religion.name}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.formerReligion && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Former Religion:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.formerReligion.name}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.profession && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Profession:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.profession.name}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.professionExplanation && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Explanation of Profession:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.professionExplanation}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.gender && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Gender:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.gender.name}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.interestingFeature && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Additional Notes:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.interestingFeature}
                    </Text>
                  </div>
                </>
              )}

              {((selectedData?.interactionsWithOrdinaryA &&
                selectedData.interactionsWithOrdinaryA.length > 0) ||
                (selectedData?.interactionsWithOrdinaryB &&
                  selectedData.interactionsWithOrdinaryB.length > 0)) && (
                <div className="info-row">
                  <>
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Interactions With Ordinary People:
                    </Text>
                    {() => {
                      let ordinaryInteractions: SubObjectPair[] = [];
                      if (selectedData.interactionsWithOrdinaryA) {
                        ordinaryInteractions = [
                          ...ordinaryInteractions,
                          ...selectedData.interactionsWithOrdinaryA,
                        ];
                      }
                      if (selectedData.interactionsWithOrdinaryB) {
                        ordinaryInteractions = [
                          ...ordinaryInteractions,
                          ...selectedData.interactionsWithOrdinaryB,
                        ];
                      }
                      return (
                        <Text fs={14} fw={400} lh={125} classNames="data">
                          {ordinaryInteractions
                            .map((inter) => inter.name)
                            .join(", ")}
                        </Text>
                      );
                    }}
                  </>
                </div>
              )}

              {selectedData?.interactionWithOrdinaryExplanation && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Interactions With Ordinary People (Explanation):
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.interactionWithOrdinaryExplanation}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.interactionsWithUnordinary &&
                selectedData.interactionsWithUnordinary.length > 0 && (
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Interactions With Unordinary People:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.interactionsWithUnordinary
                        .map((inter) => inter.name)
                        .join(", ")}
                    </Text>
                  </div>
                )}

              {selectedData?.interactionWithUnordinaryExplanation && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Interactions With Unordinary People (Explanation):
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.interactionWithUnordinaryExplanation}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.sources && selectedData.sources.length > 0 && (
                <div className="info-row">
                  <Text fs={16} fw={500} lh={125} classNames="data-field">
                    Source(s):
                  </Text>
                  <Text fs={14} fw={400} lh={125} classNames="data">
                    {selectedData.sources.map((inter) => inter.name).join(", ")}
                  </Text>
                </div>
              )}

              {selectedData?.biography && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Biography:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.biography}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.descriptionInTheSource && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Description In The Source(s):
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.descriptionInTheSource}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.explanationOfEthnicity && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Explanation of Ethnicity:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.explanationOfEthnicity}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.location && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Location:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.location.name}
                    </Text>
                  </div>
                </>
              )}

              {selectedData?.backgroundCity && (
                <>
                  <div className="info-row">
                    <Text fs={16} fw={500} lh={125} classNames="data-field">
                      Background Location:
                    </Text>
                    <Text fs={14} fw={400} lh={125} classNames="data">
                      {selectedData.backgroundCity.name}
                    </Text>
                  </div>
                </>
              )}
            </div>
          </div>
        </div>
      </ReactModal>
    </section>
  );
};
export default OrdinaryPeoplePage;
