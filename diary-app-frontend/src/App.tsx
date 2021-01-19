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

const MainPage = lazy(() => import("./components/MainPage/MainPage"));
const MonthPage = lazy(() => import("./components/MonthPage/MonthPage"));
const Login = lazy(() => import("./components/Users/Login"));

export default function App() {
	// const curDate = new Date();
	// const setAppState = useCallback((newState: IAppState): void => {
	//   const { year, month, user } = newState;
	//   localStorage.setItem("user", JSON.stringify(user));
	//   _setAppState({ ...newState });
	// }, []);

	// const [appState, _setAppState] = useState<IAppState>({
	//   month: curDate.getMonth() + 1,
	//   year: curDate.getFullYear(),
	//   user: JSON.parse(localStorage.getItem("user")),
	//   setAppState: setAppState,
	// });

	// const getFromStorage = (key: string) => {
	//   let item = localStorage.getItem(key);
	//   if (item) return Number(item);
	//   return null;
	// };

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
							<Route component={NotFound} />
						</Switch>
					</Suspense>
					<ToastContainer position="bottom-right" />
				</Container>
			</Provider>
		</Router>
	);
}
