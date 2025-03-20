import Text from "@/views/components/text";
import "./style.scss";
import Button from "@/views/components/button";
import Table from "@/views/components/table";

import { useState } from "react";
import { getOrdinaryPage } from "@/actions/ordinary-people";
import { useNavigate } from "react-router-dom";
import { Urls } from "@/routers/routes";
import useDataFetcher from "@/hooks/data-fetcher";
import { ClipLoader } from "react-spinners";
import OrdinaryPersonModal from "./ordinary-person-modal";

const OrdinaryPeoplePage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedOrdinaryId, setSelectedOrdinaryId] = useState<number>();
  const [tablePage, setTablePage] = useState<number>(1);
  const [ordinaryPageResponse, isLoading, isError] = useDataFetcher(
    () =>
      getOrdinaryPage({
        pageSize: 20,
        pageNumber: tablePage,
      }),
    [tablePage]
  );

  const navigate = useNavigate();

  const openModal = (ordinaryId: number) => {
    setSelectedOrdinaryId(ordinaryId);
    setIsOpen(true);
  };

  const closeModal = () => {
    setIsOpen(false);
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
    "Interactions With Notable",
    "Location",
  ];

  return (
    <section className="section ordinary-section">
      <div className="page-head">
        <div className="container">
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
      <div className="container">
        <div className="content">
          {isError ? (
            <Text>Sorry, something went wrong!</Text>
          ) : isLoading ? (
            <ClipLoader />
          ) : (
            ordinaryPageResponse && (
              <Table
                paginationData={
                  ordinaryPageResponse.data.totalPages
                    ? {
                        currentPage: ordinaryPageResponse.data.pageNumber,
                        setPage: setTablePage,
                        totalPage: ordinaryPageResponse.data.totalPages,
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
                  rows: ordinaryPageResponse.data.data.map((ordinary) => {
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
                      onClick: () => openModal(ordinary.id),
                    };
                  }),
                }}
              />
            )
          )}
        </div>
        <OrdinaryPersonModal
          isOpen={modalIsOpen}
          closeModal={closeModal}
          ordinaryId={selectedOrdinaryId}
        />
      </div>
    </section>
  );
};
export default OrdinaryPeoplePage;
