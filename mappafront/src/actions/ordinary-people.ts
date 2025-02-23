import {
  OrdinaryPageRequestBody,
  OrdinaryPageResponseData,
} from "@/models/ordinary-people";
import api from "./api";
import { AxiosResponse } from "axios";

const ordinaryApi = api.create({
  baseURL: api.defaults.baseURL + "ordinaryperson/",
});

export const getOrdinaryPage = async (data: OrdinaryPageRequestBody) => {
  const response = await ordinaryApi({
    url: "page/",
    method: "post",
    data: data,
  });

  return response as AxiosResponse<OrdinaryPageResponseData>;
};
