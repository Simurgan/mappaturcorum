import { useEffect, useState } from "react";
import L from "leaflet";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import { getCityMap } from "@/actions/map";
import { CityMapResponseDataItem } from "@/models/map";
import Text from "@/views/components/text";
import "./style.scss";

type FilterConfig = {
  ordinaryPeople: {
    label: string;
    type: "boolean";
  };
  writtenSources: {
    label: string;
    type: "nested";
    children: {
      mentioned: {
        label: string;
        type: "boolean";
      };
      written: {
        label: string;
        type: "boolean";
      };
    };
  };
};

const filterConfig: FilterConfig = {
  ordinaryPeople: { label: "Ordinary People", type: "boolean" },
  writtenSources: {
    label: "Written Sources",
    type: "nested",
    children: {
      mentioned: {
        label: "Cities mentioned by the written sources",
        type: "boolean",
      },
      written: {
        label: "Cities that written sources were written in",
        type: "boolean",
      },
    },
  },
};

type Filters = {
  ordinaryPeople: boolean;
  writtenSources: {
    mentioned: boolean;
    written: boolean;
  };
};

const getIconSize = (count: number) => {
  if (count > 80) {
    return new L.Point(80, 80);
  } else if (count > 50) {
    return new L.Point(50, 50);
  } else if (count > 20) {
    return new L.Point(40, 40);
  } else if (count > 10) {
    return new L.Point(30, 30);
  } else {
    return new L.Point(count * 0.45 + 16, count * 0.45 + 16);
  }
};

const MapTest = () => {
  const initializeFilters = (): Filters => {
    return {
      ordinaryPeople: false,
      writtenSources: {
        mentioned: false,
        written: true,
      },
    };
  };

  const [cityData, setCityData] = useState<CityMapResponseDataItem[]>([]);
  const [filters, setFilters] = useState<Filters>(initializeFilters());
  const [markers, setMarkers] = useState<CityMapResponseDataItem[]>([]);

  const getInitialData = async () => {
    const response = await getCityMap();
    if (response.status === 200) {
      setCityData(response.data);
    }
  };

  useEffect(() => {
    getInitialData();
  }, []);

  const filteredData = (): CityMapResponseDataItem[] => {
    if (!cityData.length) return [];

    if (filters.ordinaryPeople) {
      return cityData.filter((city) => city.numberOfLocationOf > 0);
    }

    if (filters.writtenSources.mentioned) {
      return cityData.filter(
        (city) => city.numberOfSourcesMentioningTheCity > 0
      );
    }

    if (filters.writtenSources.written) {
      return cityData.filter(
        (city) => city.numberOfSourcesWrittenInTheCity > 0
      );
    }

    return [];
  };

  useEffect(() => {
    setMarkers(
      filteredData().map((city) => {
        return {
          ...city,
          latitude: city.longitude,
          longitude: city.latitude,
        };
      })
    );
  }, [cityData, filters]);

  const handleFilterChange = (key: keyof FilterConfig) => {
    setFilters((prev) => {
      if (key === "ordinaryPeople") {
        return {
          ordinaryPeople: true,
          writtenSources: { mentioned: false, written: false },
        };
      }

      return {
        ordinaryPeople: false,
        writtenSources: {
          mentioned:
            key === "writtenSources" ? true : prev.writtenSources.mentioned,
          written:
            key === "writtenSources" ? false : prev.writtenSources.written,
        },
      };
    });
  };

  const handleSubFilterChange = (subKey: keyof Filters["writtenSources"]) => {
    setFilters((prev) => ({
      ...prev,
      writtenSources: {
        mentioned: subKey === "mentioned",
        written: subKey === "written",
      },
    }));
  };

  return (
    <section className="section map-section">
      <div className="container">
        <div className="map-container">
          <MapContainer center={[39.83, 34.96]} zoom={6} scrollWheelZoom={true}>
            <TileLayer
              attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
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
                  mouseover: (event) => event.target.openPopup(),
                  mouseout: (event) => event.target.closePopup(),
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
        <div className="map-tools-container">
          <div className="map-filters-container">
            <div className="filter-group">
              {(Object.keys(filterConfig) as (keyof FilterConfig)[]).map(
                (key) => (
                  <div key={key}>
                    <div className="filter-item">
                      <input
                        type="radio"
                        className="radio-input"
                        name="mainFilter"
                        checked={
                          key === "ordinaryPeople"
                            ? filters.ordinaryPeople
                            : !filters.ordinaryPeople
                        }
                        onChange={() => handleFilterChange(key)}
                      />
                      <Text fs={18} fw={500} color="burgundy">
                        {filterConfig[key].label}
                      </Text>
                    </div>
                  </div>
                )
              )}
            </div>
            {!filters.ordinaryPeople && (
              <div className="filter-group">
                {(
                  Object.keys(
                    filterConfig.writtenSources.children
                  ) as (keyof Filters["writtenSources"])[]
                ).map((subKey) => (
                  <div key={subKey} className="filter-item">
                    <input
                      type="radio"
                      className="radio-input"
                      name="subFilter"
                      checked={filters.writtenSources[subKey]}
                      onChange={() => handleSubFilterChange(subKey)}
                    />
                    <Text fs={12} fw={400} color="black">
                      {filterConfig.writtenSources.children[subKey].label}
                    </Text>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>
      </div>
    </section>
  );
};

export default MapTest;
