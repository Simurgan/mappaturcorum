import {
  UnordinaryGraphResponseDataItem,
  UnordinaryPageRequestBody,
  UnordinaryPageResponseData,
  UnordinaryPersonResponseType,
} from "@/models/unordinary-people";
import api from "./api";
import { AxiosResponse } from "axios";

const unordinaryApi = api.create({
  baseURL: api.defaults.baseURL + "unordinaryperson/",
});

export const getUnordinaryGraph = async () => {
  const response = await unordinaryApi({
    url: "graph/",
    method: "get",
  });

  return response as AxiosResponse<UnordinaryGraphResponseDataItem[]>;
};

export const getUnordinaryPage = async (data: UnordinaryPageRequestBody) => {
  const response = await unordinaryApi({
    url: "/page",
    method: "post",
    data: data,
  });

  return response as AxiosResponse<UnordinaryPageResponseData>;
};
export const getUnordinaryPerson = async (id: number) => {
  const response = await unordinaryApi({
    url: `/${id}`,
    method: "get",
  });

  return response as AxiosResponse<UnordinaryPersonResponseType>;
};
