import TableData, { TableDataProps } from "./table-data";
import TablePagination, { TablePaginationProps } from "./table-pagination";
import "./style.scss";

interface TableProps {
  tableData: TableDataProps;
  paginationData?: TablePaginationProps;
}

const Table = ({ tableData, paginationData }: TableProps) => {
  return (
    <div className="table-container">
      <TableData {...tableData} />
      {paginationData && <TablePagination {...paginationData} />}
    </div>
  );
};

export default Table;
