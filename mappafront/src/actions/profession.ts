import api from "./api";
import { AxiosResponse } from "axios";
import { AllProfessionsResponseDataItem } from "@/models/profession";

const professionApi = api.create({
  baseURL: api.defaults.baseURL + "profession/",
});

export const getAllProfessions = async () => {
  const response = await professionApi({
    method: "get",
  });

  return response as AxiosResponse<AllProfessionsResponseDataItem[]>;
};
