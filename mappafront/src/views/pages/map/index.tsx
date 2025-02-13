import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "./style.scss";
import L from "leaflet";
import Text from "@/views/components/text";
import React, { useState } from "react";
import Button from "@/views/components/button";
import ReactModal from "react-modal";

ReactModal.setAppElement("#root"); // For blocking not working modal styles in some browsers.

const MapPage = () => {
  const [modalIsOpen, setIsOpen] = React.useState(false);
  const [selectedMarker, setSelectedMarker] = useState<string>();

  const markers = [
    { id: 1, name: "İstanbul", latitude: 41.0082, longitude: 28.9784 },
    { id: 2, name: "Ankara", latitude: 39.9208, longitude: 32.8541 },
    { id: 3, name: "İzmir", latitude: 38.4192, longitude: 27.1287 },
    { id: 4, name: "Bursa", latitude: 40.1826, longitude: 29.0669 },
    { id: 5, name: "Antalya", latitude: 36.8841, longitude: 30.7056 },
    { id: 6, name: "Adana", latitude: 37.0, longitude: 35.3213 },
    { id: 7, name: "Gaziantep", latitude: 37.0662, longitude: 37.3833 },
    { id: 8, name: "Konya", latitude: 37.8713, longitude: 32.4846 },
    { id: 9, name: "Eskişehir", latitude: 39.7767, longitude: 30.5206 },
    { id: 10, name: "Trabzon", latitude: 41.0053, longitude: 39.726 },
    { id: 11, name: "Kayseri", latitude: 38.7312, longitude: 35.4787 },
    { id: 12, name: "Mersin", latitude: 36.8121, longitude: 34.6415 },
    { id: 13, name: "Samsun", latitude: 41.2867, longitude: 36.33 },
    { id: 14, name: "Erzurum", latitude: 39.9055, longitude: 41.2658 },
    { id: 15, name: "Van", latitude: 38.5012, longitude: 43.3727 },
    { id: 16, name: "Diyarbakır", latitude: 37.9158, longitude: 40.2189 },
    { id: 17, name: "Balıkesir", latitude: 39.6484, longitude: 27.8826 },
    { id: 18, name: "Malatya", latitude: 38.3555, longitude: 38.3096 },
    { id: 19, name: "Aydın", latitude: 37.8444, longitude: 27.8458 },
    { id: 20, name: "Şanlıurfa", latitude: 37.1674, longitude: 38.7955 },
  ];

  function openModal(markerValue: string) {
    setSelectedMarker(markerValue);
    setIsOpen(true);
  }

  function afterOpenModal() {}

  function closeModal() {
    setIsOpen(false);
  }

  return (
    <section className="section map-section">
      <div className="container">
        <ReactModal
          isOpen={modalIsOpen}
          onAfterOpen={afterOpenModal}
          onRequestClose={closeModal}
          contentLabel="Example Modal"
          overlayClassName="custom-overlay"
          className="custom-modal"
        >
          <div className="modal-content">
            <Text fs={16} fw={400} lh={125}>
              {selectedMarker}
            </Text>
            <Button onClick={closeModal} classNames="modal-close-button">
              <Text fs={12} fw={700}>
                X
              </Text>
            </Button>
          </div>
        </ReactModal>
        <div className="map-container">
          <MapContainer
            center={[39.83, 34.96]}
            zoom={6}
            scrollWheelZoom={true}
            // maxBounds={[
            //   [30, 20],
            //   [50, 50],
            // ]}
          >
            <TileLayer
              // attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              // url="https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png"
              url="https://maps-for-free.com/layer/relief/z{z}/row{y}/{z}_{x}-{y}.jpg"
              maxZoom={11}
            />
            <TileLayer
              // attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              // url="https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png"
              url="https://maps-for-free.com/layer/water/z{z}/row{y}/{z}_{x}-{y}.gif"
              maxZoom={11}
            />
            {markers.map((item) => (
              <Marker
                key={item.id}
                position={[item.latitude, item.longitude]}
                icon={
                  new L.Icon({
                    iconUrl: require("@/assets/icons/blue-marker.svg"),
                    iconRetinaUrl: require("@/assets/icons/blue-marker.svg"),
                    iconSize: new L.Point(20, 20),
                    className: "leaflet-div-icon",
                  })
                }
                eventHandlers={{
                  click: () => openModal(item.name),
                  mouseover: (event) => {
                    event.target.openPopup();
                  },
                  mouseout: (event) => {
                    event.target.closePopup();
                  },
                }}
              >
                <Popup>
                  <Text tag="p" fw={300} fs={12} lh={125} color="black">
                    {item.name}
                  </Text>
                </Popup>
              </Marker>
            ))}
          </MapContainer>
        </div>
        <div className="map-tools-container"></div>
      </div>
    </section>
  );
};
export default MapPage;
