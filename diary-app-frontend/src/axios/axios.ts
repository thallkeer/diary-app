import axios, { AxiosError } from "axios";
import { config, getHeaders } from "../utils/config";
import { userService } from "../services/users";
import { toast } from "react-toastify";
import history from "../components/history";

const axiosInstance = axios.create({
	baseURL: config.baseApi,
});

axiosInstance.interceptors.request.use(
	(config) => {
		config.headers = getHeaders();
		return config;
	},
	(error) => {
		return Promise.reject(error);
	}
);

axiosInstance.interceptors.response.use(
	(response) => response,
	(error: AxiosError) => {
		if (error.message === "Network Error" && !error.response) {
			toast.error("Network error - make sure API is running!");
		} else if (error.response) {
			const { status, data, config, headers } = error.response;
			// if (status === 404) {
			// 	history.push("/notfound");
			// }

			if (status === 401 && checkTokenIsExpired(headers)) {
				userService.logoff();
				toast.info("Your session has expired, please login again");
			}
			if (
				status === 400 &&
				config.method === "get" &&
				data.errors?.hasOwnProperty("id")
			) {
				history.push("/notfound");
			}
			if (status === 500) {
				toast.error("Server error - check the terminal for more info!");
			}
			throw error.response;
		}
	}
);

const checkTokenIsExpired = (headers: any) => {
	const authHeader: string = headers["www-authenticate"];

	return (
		authHeader !== undefined &&
		authHeader.includes(
			'Bearer error="invalid_token", error_description="The token expired at'
		)
	);
};

export default axiosInstance;
