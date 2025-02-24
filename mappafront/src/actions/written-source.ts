import { AxiosResponse } from "axios";
import api from "./api";
import {
  WrittenSourceRequestBody,
  WrittenSourceResponseType,
} from "@/models/written-source";

const writtenSourceApi = api.create({
  baseURL: api.defaults.baseURL + "WrittenSource",
});

export const getWrittenSources = async (data: WrittenSourceRequestBody) => {
  const response = await writtenSourceApi({
    url: "/page",
    method: "post",
    data: data,
  });

  return response as AxiosResponse<WrittenSourceResponseType>;
};
