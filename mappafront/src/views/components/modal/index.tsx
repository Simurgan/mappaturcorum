import ReactModal from "react-modal";

import "./style.scss";
import Text from "../text";
import Button from "../button";
import { OrdinaryPageResponseDataItem } from "@/models/ordinary-people";

interface MappaModalProps {
  isOpen: boolean;
  closeModal: () => void;
  data?: OrdinaryPageResponseDataItem | any;
}

const formatDataField = (data: OrdinaryPageResponseDataItem, key: string) => {
  const value = (data as any)[key];
  if (value === null) return "-";
  if (typeof value !== "object") return value.toString();
  if (Array.isArray(value) && value.length === 0) return "-";
  if (Array.isArray(value) && value.length !== 0) {
    if (typeof value[0] === "object")
      return value.map((item) => `${item.name}, `);
    else {
      return value[0] ? value[0]?.toString() : "-";
    }
  }

  return value.name || "-";
};

const MappaModal = ({ isOpen, closeModal, data }: MappaModalProps) => {
  return (
    <ReactModal
      isOpen={isOpen}
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
        <Text fs={18} fw={500} color="black">
          Overview
        </Text>

        <div className="content">
          <Text fs={28} fw={700} lh={140} color="burgundy">
            {data?.id || "No Name Provided"}
          </Text>

          <div className="content-info">
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
          </div>
        </div>
      </div>
    </ReactModal>
  );
};

export default MappaModal;
