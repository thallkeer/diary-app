import { IUser } from "../models";
import { config } from "../utils/config";
import history from "../components/history";
import axios from "axios";

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

	login(user: IUser) {
		return axios
			.post<ILoginResponse>(`${baseApi}users/authenticate`, user)
			.then((res) => res.data);
	},

	register(user: IUser) {
		return axios
			.post<ILoginResponse>(`${baseApi}users/register`, user)
			.then((res) => res.data);
	},
};
