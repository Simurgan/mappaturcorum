import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "./style.scss";
import L from "leaflet";
import Text from "@/views/components/text";

const MapPage = () => {
  return (
    <section className="section map-section">
      <div className="container">
        <div className="map-container">
          <MapContainer center={[39.83, 34.96]} zoom={6} scrollWheelZoom={true}>
            <TileLayer
              attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <Marker
              position={[37.0419171, 36.2287413]}
              icon={
                new L.Icon({
                  iconUrl: require("@/assets/icons/blue-marker.svg"),
                  iconRetinaUrl: require("@/assets/icons/blue-marker.svg"),
                  iconSize: new L.Point(20, 20),
                  className: "leaflet-div-icon",
                })
              }
              eventHandlers={{
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
                  Fakıuşağı, Osmaniye
                </Text>
              </Popup>
            </Marker>
            <Marker
              position={[38.247275, 36.8946894]}
              icon={
                new L.Icon({
                  iconUrl: require("@/assets/icons/blue-marker.svg"),
                  iconRetinaUrl: require("@/assets/icons/blue-marker.svg"),
                  iconSize: new L.Point(20, 20),
                  className: "leaflet-div-icon",
                })
              }
              eventHandlers={{
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
                  Afşin, Kahramanmaraş
                </Text>
              </Popup>
            </Marker>
          </MapContainer>
        </div>
        <div className="map-tools-container"></div>
      </div>
    </section>
  );
};
export default MapPage;
