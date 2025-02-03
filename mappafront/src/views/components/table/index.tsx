import Text from "../text";
import "./style.scss";

interface TableData {
  [key: string]: any;
}

interface TableProps {
  headers: string[];
  data: TableData[];
}

const Table = ({ headers, data }: TableProps) => {
  return (
    <div className="table-container">
      <table className="table">
        <thead>
          <tr className="table-header">
            {headers.map((item: string) => (
              <th className="header-item">
                <Text fs={16} fw={700} lh={125}>
                  {item}
                </Text>
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {data.map((item, index) => (
            <tr key={index} className="table-row">
              {Object.keys(item).map((itemKey, subIndex) => (
                <td key={subIndex} className="row-item">
                  <Text fs={16} fw={400} lh={125}>
                    {item[itemKey]}
                  </Text>
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Table;
