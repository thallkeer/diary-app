import * as React from "react";
import TodoList from "./components/TodoList/TodoList";
import ImportanceList from "./components/ImportanceList";
import Calendar from "./components/Calendar";
import { Container, Row, Col } from "react-bootstrap";

class OtherApp extends React.Component {
  render() {
    return (
      <Container style={{ border: "1px solid red" }}>
        <Row>
          <Col style={{ border: "1px solid red;" }} md={{ span: 2, offset: 2 }}>
            Покупки
          </Col>
          <Col style={{ border: "1px solid red;" }} md={{ span: 4, offset: 8 }}>
            Идеи этого месяца
          </Col>
        </Row>
        <Row>
          <Col md="3" className="text-center text-uppercase">
            <TodoList header={"Для дома"} />
          </Col>
        </Row>
        {/* <Row>
          <Col md="3" className="text-center">
            <ImportanceList header={"Важные события"} />
          </Col>
          <Col md="9">
            <Calendar />
          </Col>
        </Row> */}
      </Container>
    );
  }
}

export default OtherApp;
