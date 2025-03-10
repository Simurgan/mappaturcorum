import {
  OrdinaryGraphResponseDataItem,
  OrdinaryPageRequestBody,
  OrdinaryPageResponseData,
  SingleOrdinaryObject,
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

export const getOrdinaryGraph = async () => {
  const response = await ordinaryApi({
    url: "graph/",
    method: "get",
  });

  return response as AxiosResponse<OrdinaryGraphResponseDataItem[]>;
};

export const getOrdinary = async (ordinaryId: number) => {
  const response = await ordinaryApi({
    url: `${ordinaryId}`,
    method: "get",
  });

  return response as AxiosResponse<SingleOrdinaryObject>;
};
