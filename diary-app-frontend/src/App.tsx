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
import { Navbar, Nav } from "react-bootstrap";
import { logoff } from "./services/users";
import { TransferDataForm } from "./components/Dialogs/TransferDataForm";

export default function App() {
  const curDate = new Date();
  const appState = useAppState({
    month: curDate.getMonth() + 1,
    year: curDate.getFullYear(),
    user: JSON.parse(localStorage.getItem("user"))
  });
  console.log(appState);

  const onLogoff = () => {
    appState.setAppState({ ...appState, user: null });
    logoff();
  };

  return (
    <>
      <Navbar bg="light" collapseOnSelect>
        <Navbar.Brand href="/main">Diary App</Navbar.Brand>
        <Navbar.Toggle />
        <Navbar.Collapse className="justify-content-end">
          {appState.user && (
            <Nav>
              <Nav.Link
                href="/transferData"
                style={{ fontFamily: "Roboto", fontSize: "1.2rem" }}
              >
                Перенести списки на следующий месяц
              </Nav.Link>
              <Nav.Link
                onClick={onLogoff}
                style={{ fontFamily: "Roboto", fontSize: "1.2rem" }}
              >
                Выйти
              </Nav.Link>
            </Nav>
          )}
        </Navbar.Collapse>
      </Navbar>
      <Container fluid>
        <AppContext.Provider value={appState}>
          <Router history={history}>
            <Switch>
              <PrivateRoute path="/" exact={true} component={MainPage} />
              <Route path="/login" component={Login} />
              <PrivateRoute path="/main" exact={true} component={MainPage} />
              <PrivateRoute path="/month" exact={true} component={MonthPage} />
              <PrivateRoute
                path="/transferData"
                exact={true}
                component={TransferDataForm}
              />
              <Route component={NotFound} />
            </Switch>
          </Router>
        </AppContext.Provider>
      </Container>
    </>
  );
}
