import * as React from "react";
import TodoList from "./components/TodoList/TodoList";
import ImportanceList from "./components/ImportanceList";
import Calendar from "./components/Calendar";
//import Calendar from "./components/OtherCalendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";

class App extends React.Component {
  render() {
    return (
      <Container>
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
    );
  }
}

export default App;
