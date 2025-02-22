import axios from "axios";

// Dynamically set backend URL
const backendUrl = import.meta.env.VITE_BACKEND_URL ?? "__VITE_BACKEND_URL__";

axios.defaults.baseURL = `${backendUrl}/`;

export const api = axios;
