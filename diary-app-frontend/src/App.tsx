import React from "react";
import { /* BrowserRouter as*/ Router, Route, Switch } from "react-router-dom";
//import Header from "../components/Header";
import Container from "react-bootstrap/Container";
import { MonthPage } from "./components/MonthPage/MonthPage";
import NotFound from "./components/NotFound";
import { MainPage } from "./components/MainPage/MainPage";
import history from "./components/history";
import { Login } from "./components/Users/Login";
import { PrivateRoute } from "./components/Router/PrivateRoute";
import { AppContext } from "./context";
import { useAppState } from "./hooks/useAppState";

export default function App() {
  const curDate = new Date();
  const appState = useAppState({
    month: curDate.getMonth() + 1,
    year: curDate.getFullYear(),
    setAppState: () => {}
  });

  return (
    <Container fluid>
      <AppContext.Provider value={appState}>
        <Router history={history}>
          <Switch>
            <PrivateRoute path="/" exact={true} component={MainPage} />
            <Route path="/login" component={Login} />
            {/* <Route path="/register" component={RegisterPage} /> */}
            <PrivateRoute path="/main" exact={true} component={MainPage} />
            <PrivateRoute path="/month" exact={true} component={MonthPage} />
            <Route component={NotFound} />
          </Switch>
        </Router>
      </AppContext.Provider>
    </Container>
  );
}
