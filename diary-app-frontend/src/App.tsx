import React, { FC } from "react";
import { TodoList } from "./components/Lists/TodoList";
import { ImportanceList } from "./components/Lists/ImportanceList";
import Calendar from "./components/Calendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { AppState } from "./context/app-state";
import OtherCalendar from "./components/OtherCalendar/Calendar";

export const App: FC = () => {
  return (
    <AppState>
      <Container fluid style={{ marginTop: "20px" }}>
        <Row>
          <Col md="3" className="text-center">
            <TodoList header={"Важные дела"} />
            <ImportanceList header={"Важные события"} withDate readonly />
          </Col>
          <Col md="9">
            {/* <OtherCalendar /> */}
            <Calendar />
          </Col>
        </Row>
      </Container>
    </AppState>
  );
};
