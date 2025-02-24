import { UnordinaryGraphResponseDataItem } from "@/models/unordinary-people";
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
