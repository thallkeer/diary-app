import React, { Suspense, lazy } from "react";
import { /* BrowserRouter as*/ Router, Route, Switch } from "react-router-dom";
//import Header from "../components/Header";
import Container from "react-bootstrap/Container";
import NotFound from "./components/NotFound";
import history from "./components/history";
import { PrivateRoute } from "./components/Router/PrivateRoute";
import Loader from "./components/Loader";
import { Provider } from "react-redux";
import store from "./store/store";
import { ToastContainer } from "react-toastify";
import { UserSettings } from "components/Users/UserSettings";

const MainPage = lazy(() => import("./components/MainPage/MainPage"));
const MonthPage = lazy(() => import("./components/MonthPage/MonthPage"));
const Login = lazy(() => import("./components/Users/Login"));

export default function App() {
	return (
		<Router history={history}>
			<Provider store={store}>
				<Container fluid>
					<Suspense fallback={<Loader />}>
						<Switch>
							<PrivateRoute path="/" exact={true} component={MainPage} />
							<Route path="/login" component={Login} />
							<PrivateRoute path="/main" exact={true} component={MainPage} />
							<PrivateRoute path="/month" exact={true} component={MonthPage} />
							<PrivateRoute
								path="/settings"
								exact={true}
								component={UserSettings}
							/>
							<Route component={NotFound} />
						</Switch>
					</Suspense>
					<ToastContainer position="bottom-right" />
				</Container>
			</Provider>
		</Router>
	);
}
