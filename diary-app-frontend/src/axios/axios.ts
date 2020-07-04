import axios, { AxiosError } from "axios";
import { config } from "../utils/config";
import { logoff } from "../services/users";
const { baseApi, headers } = config;

const axiosInstance = axios.create({
	baseURL: baseApi,
	headers,
});

axiosInstance.interceptors.response.use(
	(response) => {
		//console.log("response in interceptor", response);
		return response;
	},
	(error: AxiosError) => {
		console.log("error in interceptor", error.toJSON());

		if (error.response && error.response.status === 401) logoff();

		return error;
	}
);

export default axiosInstance;
