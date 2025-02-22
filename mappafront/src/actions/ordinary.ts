import { OrdinaryPageRequestBody } from "@/models/ordinary-people";
import api from "./api";

const ordinaryApi = api.create({
  baseURL: api.defaults.baseURL + "ordinaryperson/",
});

export const getOrdinaryPage = async (data: OrdinaryPageRequestBody) => {
  const response = await ordinaryApi({
    url: "page/",
    method: "get",
    data: data,
  });

  return response;
};
