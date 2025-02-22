import axios from "axios";

// Dynamically set backend URL
const backendUrl = import.meta.env.VITE_BACKEND_URL ?? "__VITE_BACKEND_URL__";
console.log(backendUrl);
console.log("backend url");

axios.defaults.baseURL = `${backendUrl}/`;

const api = axios;
export default api;
