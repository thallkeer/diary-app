import React from "react";
import { /* BrowserRouter as*/ Router, Route, Switch } from "react-router-dom";
//import Header from "../components/Header";
import Container from "react-bootstrap/Container";
import { MonthPage } from "../MonthPage/MonthPage";
import NotFound from "../NotFound";
import { MainPage } from "../MainPage/MainPage";
import history from "../history";

export default function AppRouter() {
  return (
    <Container fluid>
      <Router history={history}>
        <Switch>
          <Route path="/" exact={true} component={MainPage} />
          <Route path="/main" exact={true} component={MainPage} />
          <Route path="/month" exact={true} component={MonthPage} />
          <Route component={NotFound} />
        </Switch>
      </Router>
    </Container>
  );
}
