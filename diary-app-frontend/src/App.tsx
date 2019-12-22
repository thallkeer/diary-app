import React, { FC } from "react";
import { TodoList } from "./components/TodoList/TodoList";
import { ImportanceList } from "./components/ImportanceList";
import Calendar from "./components/Calendar";
//import Calendar from "./components/OtherCalendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { AppState } from "./contexts/app-state";

export const App: FC = () => {
  return (
    <AppState>
      <Container fluid style={{ marginTop: "20px" }}>
        <Row>
          <Col md="3" className="text-center">
            <TodoList header={"Важные дела"} />
            <ImportanceList header={"Важные события"} />
          </Col>
          <Col md="9">
            <Calendar />
          </Col>
        </Row>
      </Container>
    </AppState>
  );
};
