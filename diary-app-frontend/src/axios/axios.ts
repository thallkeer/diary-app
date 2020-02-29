import axios from "axios";
import { config } from "../helpers/config";
import { logoff } from "../services/users";
const { baseApi, headers } = config;

axios.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    console.log("in interceptor", error);

    if (error.response.status === 401) logoff();

    return error;
  }
);

export default axios.create({
  baseURL: baseApi,
  headers
});
