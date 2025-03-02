import api from "./api";
import { AxiosResponse } from "axios";
import {
  AllWrittenSourcesResponseDataItem,
  WrittenSourceRequestBody,
  WrittenSourceResponseType,
} from "@/models/written-source";

const writtenSourceApi = api.create({
  baseURL: api.defaults.baseURL + "writtensource/",
});

export const getAllWrittenSources = async () => {
  const response = await writtenSourceApi({
    method: "get",
  });

  return response as AxiosResponse<AllWrittenSourcesResponseDataItem[]>;
};

export const getWrittenSources = async (data: WrittenSourceRequestBody) => {
  const response = await writtenSourceApi({
    url: "/page",
    method: "post",
    data: data,
  });

  return response as AxiosResponse<WrittenSourceResponseType>;
};
