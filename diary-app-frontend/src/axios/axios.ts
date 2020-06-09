import axios from "axios";
import { config } from "../utils/config";
import { logoff } from "../services/users";
const { baseApi, headers } = config;

const axiosInstance = axios.create({
  baseURL: baseApi,
  headers,
});

axiosInstance.interceptors.response.use(
  (response) => {
    console.log("in interceptorr", response);
    return response;
  },
  (error) => {
    console.log("in interceptorrr", error, "-", error.response);

    if (error.response && error.response.status === 401) logoff();

    return error;
  }
);

export default axiosInstance;
