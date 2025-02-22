import api from "./api";

const ordinaryApi = api.create({
  baseURL: api.defaults.baseURL + "ordinaryperson/",
});

export const getOrdinaryPage = async () => {
  const response = await ordinaryApi({
    url: "page/",
    method: "get",
    data: 
  })
}
