import { getWrittenSources } from "@/actions/written-source";
import Button from "@/views/components/button";
import MappaModal from "@/views/components/modal";
import Table from "@/views/components/table";
import Text from "@/views/components/text";
import { useEffect, useState } from "react";

import "./style.scss";
import {
  SubObjectPair,
  WrittenSourceResponseItemType,
} from "@/models/written-source";

const SourcesPage = () => {
  const [modalIsOpen, setIsOpen] = useState<boolean>(false);
  const [selectedData, setSelectedData] =
    useState<WrittenSourceResponseItemType>();
  const [tableData, setTableData] = useState<WrittenSourceResponseItemType[]>();
  console.log("🚀 ~ SourcesPage ~ tableData:", tableData);
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
    const response = await getWrittenSources({
      pageSize: 10,
      pageNumber: 1,
    });

    if (response.status === 200) {
      setTableData(response.data.data);
      setTotalPage(response.data.totalPages);
    }
  };

  const updateData = async () => {
    const response = await getWrittenSources({
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
    "Ordinary Persons",
    "Unordinary Persons",
    "Alternate Names",
    "Author",
    "Year Written",
    "Genre",
    "Language",
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
            Sources
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
            rows: tableData?.map((source: WrittenSourceResponseItemType) => {
              const cellTexts = [
                source.name,
                source.ordinaryPersons.map(
                  (person: SubObjectPair) => person.name
                ),
                source.unordinaryPersons.map(
                  (person: SubObjectPair) => person.name
                ),
                source.alternateNames?.map((name: string) => name).join(", "),
                source.author,
                source.yearWritten?.map((year: number) => year.toString()),
                source.genre?.name,
                source.language?.name,
              ];
              return {
                cells: cellTexts.map((cellText) => (
                  <Text fs={12} fw={500} lh={125} color="dark-gray">
                    {cellText! || "-"}
                  </Text>
                )),
                onClick: () => openModal(source),
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
export default SourcesPage;
