import usePagination from "@/helpers/hooks/usePagination";
import TableData from "./data";
import TablePagination from "./pagination";
import "./style.scss";

interface TableProps {
  tableHeaders: string[];
  tableData: Object[];
}

const Table = ({ tableHeaders, tableData }: TableProps) => {
  const {
    currentPage,
    totalPages,
    nextPage,
    prevPage,
    goToPage,
    paginatedData,
  } = usePagination({
    _data: tableData,
    itemsPerPage: 100,
  });

  return (
    <div className="table-container">
      <TableData headers={tableHeaders} data={paginatedData} />
      <TablePagination
        currentPage={currentPage}
        totalPage={totalPages}
        nextStep={nextPage}
        prevStep={prevPage}
        goStep={goToPage}
      />
    </div>
  );
};

export default Table;
