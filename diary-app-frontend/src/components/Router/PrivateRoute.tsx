import React from "react";
import { useSelector } from "react-redux";
import { Route, Redirect } from "react-router-dom";
import { getAppInfo } from "../../selectors/app-selectors";
import { AppNavbar } from "../Navbars/AppNavbar";

export const PrivateRoute = ({ component: Component, path, ...rest }) => {
	const { user } = useSelector(getAppInfo);

	const routedComponent = (
		<Route
			{...rest}
			render={(props) =>
				user ? (
					<Component {...props} />
				) : (
					<Redirect
						to={{ pathname: "/login", state: { from: props.location } }}
					/>
				)
			}
		/>
	);

	return (
		<>
			<AppNavbar isOnMonthPage={path === "/month"} />
			{routedComponent}
		</>
	);
};
