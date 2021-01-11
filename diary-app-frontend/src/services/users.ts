import { IUser } from "../models/entities";
import { config } from "../utils/config";
import history from "../components/history";
import axios from "../axios/axios";

const { baseApi } = config;
interface ILoginResponse {
	id: number;
	username: string;
	token: string;
}

export const usersService = {
	logoff() {
		localStorage.removeItem("user");
		history.push("/login");
	},

	async login(user: IUser) {
		const result = await axios
			.post<ILoginResponse>(`${baseApi}users/authenticate`, user)
			.then((res) => res.data);

		return processLoginResponse(result);
	},

	async register(user: IUser) {
		const result = await axios
			.post<ILoginResponse>(`${baseApi}users/register`, user)
			.then((res) => res.data);

		return processLoginResponse(result);
	},
};

const processLoginResponse = (loginData: ILoginResponse) => {
	const userResponse: IUser = {
		...loginData,
	};
	localStorage.setItem("user", JSON.stringify(userResponse));
	return userResponse;
};
