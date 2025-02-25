import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "./style.scss";
import L from "leaflet";
import Text from "@/views/components/text";
import React, { useEffect, useMemo, useState } from "react";
import Button from "@/views/components/button";
import ReactModal from "react-modal";
import { getCityMap } from "@/actions/map";
import { CityMapResponseDataItem } from "@/models/map";
import Table from "@/views/components/table";
import { OrdinaryPageResponseDataItem } from "@/models/ordinary-people";
import { getOrdinaryPage } from "@/actions/ordinary-people";

ReactModal.setAppElement("#root"); // For blocking not working modal styles in some browsers.

const MAIN_FILTERS = [
  {
    id: "written",
    label: "Written Sources",
    options: ["Bahsettiği Yerler", "Yazıldığı Yerler"],
  },
  {
    id: "ordinary",
    label: "Ordinary Peoples",
    options: ["Hikayeler", "Tanıklıklar"],
  },
] as const;

type MainFilter = (typeof MAIN_FILTERS)[number]["id"];
type SubFilter = string;

const MapPage = () => {
  const [selectedMainFilter, setSelectedMainFilter] =
    useState<MainFilter | null>(MAIN_FILTERS[0].id);
  const [selectedSubFilter, setSelectedSubFilter] = useState<SubFilter | null>(
    null
  );

  const subFilters = useMemo(() => {
    return (
      MAIN_FILTERS.find((filter) => filter.id === selectedMainFilter)
        ?.options || []
    );
  }, [selectedMainFilter]);

  const handleMainFilterChange = (value: MainFilter) => {
    setSelectedMainFilter(value);
    setSelectedSubFilter(null);
  };

  const handleSubFilterChange = (value: SubFilter) => {
    setSelectedSubFilter(value);
  };

  const [modalIsOpen, setIsOpen] = React.useState(false);
  const [selectedMarker, setSelectedMarker] =
    useState<CityMapResponseDataItem>();
  const [cityData, setCityData] = useState<CityMapResponseDataItem[]>([]);
  const [markers, setMarkers] = useState<CityMapResponseDataItem[]>([]);
  const [tableData, setTableData] = useState<OrdinaryPageResponseDataItem[]>();
  const [tablePage, setTablePage] = useState<number>();
  const [totalPage, setTotalPage] = useState<number>();

  function openModal(marker: CityMapResponseDataItem) {
    setSelectedMarker(marker);
    setIsOpen(true);
  }

  function afterOpenModal() {}

  function closeModal() {
    setIsOpen(false);
  }

  const getInitialData = async () => {
    const response = await getCityMap();

    if (response.status === 200) {
      setCityData(response.data);
    }
  };

  useEffect(() => {
    getInitialData();
  }, []);

  function filterCityData() {
    if (selectedMainFilter === MAIN_FILTERS[0].id) {
      if (selectedSubFilter === "Bahsettiği Yerler") {
        return cityData.filter(
          (city) => city.numberOfSourcesMentioningTheCity > 0
        );
      }

      return cityData.filter(
        (city) => city.numberOfSourcesWrittenInTheCity > 0
      );
    } else {
      return cityData.filter((city) => city.numberOfLocationOf > 0);
    }
  }

  useEffect(() => {
    const filteredData = filterCityData().map((city) => {
      return {
        ...city,
        latitude: city.longitude,
        longitude: city.latitude,
      };
    });

    setMarkers(filteredData);
  }, [cityData, selectedMainFilter, selectedSubFilter]);

  useEffect(() => {
    console.log(markers);
    console.log("markers");
  }, [markers]);

  const getTableContent = async (cityId: number, page: number) => {
    const response = await getOrdinaryPage({
      pageSize: 10,
      pageNumber: page,
      filter: {
        location: [cityId],
      },
    });

    if (response.status === 200) {
      setTableData(response.data.data);
      setTotalPage(response.data.totalPages);
    }
  };

  useEffect(() => {
    if (selectedMarker) {
      setTablePage(1);
    }
  }, [selectedMarker]);

  const headerData = [
    "Name",
    "Alternate Name",
    "Ethnonym",
    "Religion",
    "Profession",
    "Gender",
  ];

  useEffect(() => {
    if (selectedMarker && tablePage) {
      getTableContent(selectedMarker?.id, tablePage);
    }
  }, [tablePage, selectedMarker]);

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
            <div className="modal-header">
              <Text fs={16} fw={400} lh={125}>
                {selectedMarker?.name}
              </Text>
              <Button onClick={closeModal} classNames="modal-close-button">
                <Text fs={12} fw={700}>
                  X
                </Text>
              </Button>
            </div>
            <Table
              paginationData={
                totalPage && tablePage
                  ? {
                      currentPage: tablePage,
                      setPage: setTablePage,
                      totalPage: totalPage,
                    }
                  : undefined
              }
              tableData={{
                hasRowHover: true,
                headers: headerData.map((cell) => (
                  <Text fs={14} fw={500} lh={125} color="burgundy">
                    {cell}
                  </Text>
                )),
                rows: tableData?.map((ordinary) => {
                  const cellTexts = [
                    ordinary.name,
                    ordinary.alternateName,
                    ordinary.ethnicity?.name,
                    ordinary.religion?.name,
                    ordinary.profession?.name,
                    ordinary.gender?.name,
                  ];
                  return {
                    cells: cellTexts.map((cellText) => (
                      <Text fs={12} fw={500} lh={125} color="dark-gray">
                        {cellText}
                      </Text>
                    )),
                    // onClick: () => openModal({}),
                  };
                }),
              }}
            />
          </div>
        </ReactModal>
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
                  click: () => openModal(item),
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
        <div className="map-tools-container">
          <div className="map-filters-container">
            <div className="filter-group">
              {MAIN_FILTERS.map((filter, index) => (
                <div key={filter.id} className="filter-item">
                  <input
                    type="checkbox"
                    className="checkbox-input"
                    checked={filter.id === selectedMainFilter}
                    onChange={() =>
                      handleMainFilterChange(MAIN_FILTERS[index].id)
                    }
                  />
                  <Text fs={14} fw={400} lh={125} color="burgundy">
                    {filter.label}
                  </Text>
                </div>
              ))}
            </div>
            {subFilters.length > 0 && (
              <div className="filter-group">
                {subFilters.map((option) => (
                  <div key={option} className="filter-item">
                    <input
                      type="checkbox"
                      className="checkbox-input"
                      checked={option === selectedSubFilter}
                      onChange={() => handleSubFilterChange(option)}
                    />
                    <Text fs={14} fw={400} lh={125} color="burgundy">
                      {option}
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
export default MapPage;
