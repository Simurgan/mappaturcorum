import { ReactNode } from "react";
import Text from "../../text";
import "./style.scss";

export type TableDataProps = {
  headers: ReactNode[];
  rows?: { cells: ReactNode[]; onClick?: () => void }[];
  hasRowHover?: boolean;
};

const TableData = ({ headers, rows, hasRowHover }: TableDataProps) => {
  return (
    <table className="table">
      <thead className="table-header">
        <tr className="table-columns">
          {headers.map((header, index) => (
            <th key={index} className="table-column">
              {/* <Text fs={14} fw={500} lh={125} color="burgundy"> */}
              {header}
              {/* </Text> */}
            </th>
          ))}
        </tr>
      </thead>

      <tbody className="table-body">
        {rows && rows.length > 0 ? (
          rows.map((row, rowIndex) => (
            <tr
              key={rowIndex}
              className={`data-row${hasRowHover ? " hasHoverEffect" : ""}`}
              onClick={row.onClick}
            >
              {row.cells.map((cell, index) => (
                <td key={index} className="data-row-item">
                  {cell}
                  {/* <Text fs={12} fw={500} lh={125} color="dark-gray">
                    {typeof row[key] === "object" && row[key] !== null
                      ? JSON.stringify(row[key])
                      : row[key]?.toString() || "-"}{" "}
                  </Text> */}
                </td>
              ))}
            </tr>
          ))
        ) : (
          <tr className="data-row">
            <td colSpan={headers.length} className="data-row-item no-data">
              <Text fs={12} fw={500} lh={125} color="gray">
                There is no such data
              </Text>
            </td>
          </tr>
        )}
      </tbody>
    </table>
  );
};

export default TableData;
