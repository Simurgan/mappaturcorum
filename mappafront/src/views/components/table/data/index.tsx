import Text from "../../text";
import "./style.scss";

interface TableDataProps {
  headers: string[];
  data: { [key: string]: any }[];
  openModal: (data: any) => void;
}

const TableData = ({ headers, data, openModal }: TableDataProps) => {
  return (
    <table className="table">
      <thead className="table-header">
        <tr className="table-columns">
          {headers.map((header, index) => (
            <th key={index} className="table-column">
              <Text fs={14} fw={500} lh={125} color="burgundy">
                {header}
              </Text>
            </th>
          ))}
        </tr>
      </thead>

      <tbody className="table-body">
        {data.length > 0 ? (
          data.map((row, rowIndex) => (
            <tr
              key={rowIndex}
              className="data-row"
              onClick={() => openModal(row)}
            >
              {Object.keys(row).map((key) => (
                <td key={key} className="data-row-item">
                  <Text fs={12} fw={500} lh={125} color="dark-gray">
                    {typeof row[key] === "object" && row[key] !== null
                      ? JSON.stringify(row[key])
                      : row[key]?.toString() || "-"}{" "}
                  </Text>
                </td>
              ))}
            </tr>
          ))
        ) : (
          <tr className="data-row">
            <td colSpan={headers.length} className="data-row-item no-data">
              <Text fs={12} fw={500} lh={125} color="gray">
                No data available
              </Text>
            </td>
          </tr>
        )}
      </tbody>
    </table>
  );
};

export default TableData;
