import React from "react";
import { /* BrowserRouter as*/ Router, Route, Switch } from "react-router-dom";
//import Header from "../components/Header";
import Container from "react-bootstrap/Container";
import history from "../history";
import { OtherApp } from "../../OtherApp";
import NotFound from "../NotFound";
import { App } from "../../App";

export default function AppRouter() {
  return (
    <Container fluid>
      <Router history={history}>
        <Switch>
          <Route path="/" exact={true} component={App} />
          <Route path="/month" exact={true} component={App} />
          <Route path="/other" exact={true} component={OtherApp} />
          <Route component={NotFound} />
        </Switch>
      </Router>
    </Container>
  );
}
