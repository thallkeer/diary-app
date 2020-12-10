import axios, { AxiosError } from "axios";
import { config } from "../utils/config";
import { usersService } from "../services/users";
const { baseApi, headers } = config;

const axiosInstance = axios.create({
	baseURL: baseApi,
	headers,
});

axiosInstance.interceptors.response.use(
	(response) => response,
	(error: AxiosError) => {
		console.error("error in interceptor", error);

		if (error.response?.status === 401) usersService.logoff();

		return error;
	}
);

export default axiosInstance;
