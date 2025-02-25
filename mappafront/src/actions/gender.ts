import api from "./api";
import { AxiosResponse } from "axios";
import { AllGendersResponseDataItem } from "@/models/gender";

const genderApi = api.create({
  baseURL: api.defaults.baseURL + "gender/",
});

export const getAllGenders = async () => {
  const response = await genderApi({
    method: "get",
  });

  return response as AxiosResponse<AllGendersResponseDataItem[]>;
};
