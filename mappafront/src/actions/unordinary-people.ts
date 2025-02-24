import api from "./api";
import {
  UnordinaryPageRequestBody,
  UnordinaryPageResponseData,
} from "@/models/unordinary-people";
import { AxiosResponse } from "axios";

const unordinaryApi = api.create({
  baseURL: api.defaults.baseURL + "UnordinaryPerson",
});

export const getUnordinaryPage = async (data: UnordinaryPageRequestBody) => {
  const response = await unordinaryApi({
    url: "/page",
    method: "post",
    data: data,
  });

  return response as AxiosResponse<UnordinaryPageResponseData>;
};
