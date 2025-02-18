import ReactModal from "react-modal";

import "./style.scss";
import Text from "../text";
import Button from "../button";

interface MappaModalProps {
  isOpen: boolean;
  closeModal: () => void;
  data?: { [key: string]: any };
}

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
            {data?.name || "No Name Provided"}
          </Text>

          <div className="content-info">
            {data && Object.keys(data).length > 0 ? (
              Object.keys(data).map((key, index) => (
                <div key={index} className="info-row">
                  <Text fs={16} fw={500} lh={125} classNames="data-field">
                    {key}
                  </Text>
                  <Text fs={14} fw={400} lh={125} classNames="data">
                    {typeof data[key] === "object"
                      ? JSON.stringify(data[key])
                      : data[key]?.toString() || "-"}{" "}
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
