import { useEffect, useState } from "react";
import L from "leaflet";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import { getCityMap } from "@/actions/map";
import { CityMapResponseDataItem } from "@/models/map";
import Text from "@/views/components/text";
import "./style.scss";
import ReactModal from "react-modal";
import Button from "@/views/components/button";
import Table from "@/views/components/table";
import {
  OrdinaryPageResponseDataItem,
  SubObjectPair,
} from "@/models/ordinary-people";
import { WrittenSourceResponseItemType } from "@/models/written-source";
import { getOrdinaryPage } from "@/actions/ordinary-people";

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

const headerData = [
  "Name",
  "Alternate Name",
  "Ethnicity",
  "Religion",
  "Profession",
  "Gender",
];

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
  const [selectedMarker, setSelectedMarker] =
    useState<CityMapResponseDataItem>();
  const [modalIsOpen, setIsOpen] = useState(false);
  const [tableData, setTableData] = useState<
    OrdinaryPageResponseDataItem[] | WrittenSourceResponseItemType[]
  >();
  const [tablePage, setTablePage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>();

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

  function openModal(marker: CityMapResponseDataItem) {
    setSelectedMarker(marker);
    setTablePage(1);
    setIsOpen(true);
  }

  function afterOpenModal() {}

  function closeModal() {
    setIsOpen(false);
  }

  const getTableOrdinaryContent = async (cityId: number, page: number) => {
    const response = await getOrdinaryPage({
      pageSize: 20,
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
    if (selectedMarker && tablePage !== undefined) {
      getTableOrdinaryContent(selectedMarker.id, tablePage);
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
              tableData={{
                hasRowHover: true,
                headers: headerData.map((cell) => (
                  <Text fs={14} fw={500} lh={125} color="burgundy">
                    {cell}
                  </Text>
                )),
                rows: tableData?.map((item) => {
                  const ordinaryCellText = [
                    item.name,
                    "alternateName" in item ? item.alternateName : "",
                    "ethnicity" in item ? item.ethnicity?.name : "",
                    "religion" in item ? item.religion?.name : "",
                    "profession" in item ? item.profession?.name : "",
                    "gender" in item ? item.gender?.name : "",
                  ];
                  const writtenCellText = [
                    item.name,
                    "ordinaryPersons" in item
                      ? item.ordinaryPersons.map(
                          (person: SubObjectPair) => person.name
                        )
                      : "",
                    "unordinaryPersons" in item
                      ? item.unordinaryPersons.map(
                          (person: SubObjectPair) => person.name
                        )
                      : "",
                    "alternateNames" in item
                      ? item.alternateNames
                          ?.map((name: string) => name)
                          .join(", ")
                      : "",
                    "author" in item ? item.author : "",
                    "yearWritten" in item
                      ? item.yearWritten?.map((year: number) => year.toString())
                      : "",
                    "genre" in item ? item.genre?.name : "",
                    "language" in item ? item.language?.name : "",
                  ];
                  let cellText = !filters.ordinaryPeople
                    ? writtenCellText
                    : ordinaryCellText;

                  return {
                    cells: cellText.map((cellText) => (
                      <Text fs={12} fw={500} lh={125} color="dark-gray">
                        {cellText}
                      </Text>
                    )),
                    // onClick: () => openModal({}),
                  };
                }),
              }}
              paginationData={
                totalPage && tablePage
                  ? {
                      currentPage: tablePage,
                      setPage: setTablePage,
                      totalPage: totalPage,
                    }
                  : undefined
              }
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
