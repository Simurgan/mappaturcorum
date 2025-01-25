import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "./style.scss";
import L from "leaflet";

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
              position={[37.0691417, 36.2660925]}
              // icon={
              //   new L.Icon({
              //     iconUrl: require(""),
              //     iconRetinaUrl: "",
              //     iconSize: new L.Point(60, 75),
              //     className: "leaflet-div-icon",
              //   })
              // }
            >
              <Popup>
                A pretty CSS3 popup. <br /> Easily customizable.
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
