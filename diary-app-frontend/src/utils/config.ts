import { IUser } from "../models/entities";

export const getHeaders = () => {
	// return authorization header with jwt token
	let user: IUser = JSON.parse(localStorage.getItem("user"));
	return (
		(user && {
			"Access-Control-Allow-Origin": "*",
			Authorization: "Bearer " + user.token,
		}) ||
		{}
	);
};

export const config = {
	baseApi: process.env.REACT_APP_API_URL + "/api/",
	headers: getHeaders(),
};
