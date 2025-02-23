export type CityMapResponseDataItem = {
  id: number;
  name: string;
  asciiName?: string;
  latitude: number;
  longitude: number;
  numberOfLocationOf: number;
  numberOfSourcesWrittenInTheCity: number;
  numberOfSourcesMentioningTheCity: number;
  numberOfBackgroundCityOf: number;
  numberOfBirthPlaceOf: number;
  numberOfDeathPlaceOf: number;
};
