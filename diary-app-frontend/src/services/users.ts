import { IUser, IUserSettings } from "../models/entities";
import history from "../components/history";
import axios from "../axios/axios";

interface ILoginResponse {
	id: number;
	username: string;
	token: string;
}

interface UserSettingsModel {
	user: IUser;
	settings: IUserSettings;
}

export const userService = {
	logoff() {
		localStorage.removeItem("user");
		history.push("/login");
	},

	async login(user: IUser) {
		const result = await axios
			.post<ILoginResponse>(`users/authenticate`, user)
			.then((res) => res.data);

		return processLoginResponse(result);
	},

	async register(user: IUser) {
		const result = await axios
			.post<ILoginResponse>(`users/register`, user)
			.then((res) => res.data);

		return processLoginResponse(result);
	},

	async getUserSettings(userId: number) {
		return axios
			.get<UserSettingsModel>(`users/${userId}/settings`)
			.then((res) => res.data);
	},

	async updateUser(user: IUser) {
		return axios.put(`users`, user);
	},

	async updateUserSettings(settings: IUserSettings) {
		return axios.put(`userSettings`, settings);
	},
};

const processLoginResponse = (loginData: ILoginResponse) => {
	const userResponse: IUser = {
		...loginData,
	};
	localStorage.setItem("user", JSON.stringify(userResponse));
	return userResponse;
};
