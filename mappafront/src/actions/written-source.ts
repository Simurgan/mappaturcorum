import api from "./api";
import { AxiosResponse } from "axios";
import { AllWrittenSourcesResponseDataItem } from "@/models/written-source";

const writtenSourceApi = api.create({
  baseURL: api.defaults.baseURL + "writtensource/",
});

export const getAllWrittenSources = async () => {
  const response = await writtenSourceApi({
    method: "get",
  });

  return response as AxiosResponse<AllWrittenSourcesResponseDataItem[]>;
};
