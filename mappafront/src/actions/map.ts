import { AxiosResponse } from "axios";
import api from "./api";
import { CityMapResponseDataItem } from "@/models/map";

const mapApi = api.create({
  baseURL: api.defaults.baseURL + "city/",
});

export const getCityMap = async () => {
  const response = await mapApi({
    url: "map/",
    method: "get",
  });

  return response as AxiosResponse<CityMapResponseDataItem[]>;
};
