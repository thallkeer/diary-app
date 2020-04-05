import React, { useState, useCallback } from "react";
import { /* BrowserRouter as*/ Router, Route, Switch } from "react-router-dom";
//import Header from "../components/Header";
import Container from "react-bootstrap/Container";
import { MonthPage } from "./components/MonthPage/MonthPage";
import NotFound from "./components/NotFound";
import { MainPage } from "./components/MainPage/MainPage";
import history from "./components/history";
import { Login } from "./components/Users/Login";
import { PrivateRoute } from "./components/Router/PrivateRoute";
import { AppContext, IGlobalContext } from "./context";

export default function App() {
  const curDate = new Date();
  const setAppState = useCallback((newState: IGlobalContext): void => {
    const { year, month, user } = newState;
    localStorage.setItem("user", JSON.stringify(user));
    _setAppState({ ...newState });
  }, []);

  const [appState, _setAppState] = useState<IGlobalContext>({
    month: curDate.getMonth() + 1,
    year: curDate.getFullYear(),
    user: JSON.parse(localStorage.getItem("user")),
    setAppState: setAppState,
  });

  const getFromStorage = (key: string) => {
    let item = localStorage.getItem(key);
    if (item) return Number(item);
    return null;
  };

  return (
    <Container fluid>
      <AppContext.Provider value={appState}>
        <Router history={history}>
          <Switch>
            <PrivateRoute path="/" exact={true} component={MainPage} />
            <Route path="/login" component={Login} />
            <PrivateRoute path="/main" exact={true} component={MainPage} />
            <PrivateRoute path="/month" exact={true} component={MonthPage} />
            <Route component={NotFound} />
          </Switch>
        </Router>
      </AppContext.Provider>
    </Container>
  );
}
