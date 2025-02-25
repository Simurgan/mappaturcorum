import { AllReligionsResponseDataItem } from "@/models/religion";
import api from "./api";
import { AxiosResponse } from "axios";

const religionApi = api.create({
  baseURL: api.defaults.baseURL + "religion/",
});

export const getAllReligions = async () => {
  const response = await religionApi({
    method: "get",
  });

  return response as AxiosResponse<AllReligionsResponseDataItem[]>;
};
