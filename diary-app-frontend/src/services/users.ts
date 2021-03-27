import { IUser, IUserSettings } from "../models/entities";
import history from "../components/history";
import axios from "../axios/axios";

export interface IAuthenticatedUser {
	id: number;
	username: string;
	token: string;
}

interface IUserSettingsModel {
	user: IUser;
	settings: IUserSettings;
}

export type UserAuthModel = {
	username: string;
	password: string;
};

export const userService = {
	logoff(): void {
		localStorage.removeItem("user");
		history.push("/login");
	},

	async login(user: UserAuthModel): Promise<IUser> {
		const result = await axios
			.post<IAuthenticatedUser>(`users/authenticate`, user)
			.then((res) => res.data);

		return processLoginResponse(result);
	},

	async register(user: UserAuthModel): Promise<IUser> {
		const result = await axios
			.post<IAuthenticatedUser>(`users/register`, user)
			.then((res) => res.data);

		return processLoginResponse(result);
	},

	async getUserSettings(userId: number): Promise<IUserSettingsModel> {
		return axios
			.get<IUserSettingsModel>(`users/${userId}/settings`)
			.then((res) => res.data);
	},

	async updateUser(user: IUser) {
		return axios.put(`users`, user);
	},

	async updateUserSettings(settings: IUserSettings) {
		return axios.put(`userSettings`, settings);
	},
};

const processLoginResponse = (loginData: IAuthenticatedUser): IUser => {
	localStorage.setItem("user", JSON.stringify(loginData));
	return {
		id: loginData.id,
		username: loginData.username,
	};
};
