import { IAuthenticatedUser } from "services/users";

export const getHeaders = () => {
	const user: IAuthenticatedUser = JSON.parse(localStorage.getItem("user"));
	return (
		(user && {
			"Access-Control-Allow-Origin": "*",
			Authorization: "Bearer " + user.token,
		}) ||
		{}
	);
};

export const config = {
	baseApi: `${process.env.REACT_APP_API_URL}/api/`,
};
