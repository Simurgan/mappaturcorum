import { AllEthnicitiesResponseDataItem } from "@/models/ethnicity";
import api from "./api";
import { AxiosResponse } from "axios";

const ethnicityApi = api.create({
  baseURL: api.defaults.baseURL + "ethnicity/",
});

export const getAllEthnicities = async () => {
  const response = await ethnicityApi({
    method: "get",
  });

  return response as AxiosResponse<AllEthnicitiesResponseDataItem[]>;
};
