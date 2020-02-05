import axios from "axios";
import { config } from "../helpers/config";

const { baseApi, headers } = config;

export default axios.create({
  baseURL: baseApi,
  headers
});
