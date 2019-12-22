import React from "react";
import { TodoList } from "./components/TodoList/TodoList";
import { Container, Row, Col } from "react-bootstrap";
import { ImportanceList } from "./components/ImportanceList";

export const OtherApp: React.FC = () => {
  return (
    <Container
      fluid
      className="second-page-container text-center"
      style={{ border: "1px solid red" }}
    >
      <Row>
        <Col md={6}>
          <h1>Покупки</h1>
          <Row>
            <Col md={5}>
              <TodoList header="Для дома" />
            </Col>
            <Col md={2}>{""}</Col>
            <Col md={5}>
              <TodoList header="Одежда" />
            </Col>
          </Row>
          <h1>В этом месяце я хочу</h1>
          <Row>
            <Col md={4}>
              <ImportanceList header="Прочитать" />
            </Col>
            <Col md={4}>
              <ImportanceList header="Посмотреть" />
            </Col>
            <Col md={4}>
              <ImportanceList header="Посетить" />
            </Col>
          </Row>
        </Col>
        <Col md={6}>
          <h1>Идеи этого месяца</h1>
          <Row>
            <Col md={12}>
              <ImportanceList style={{ marginTop: "52px" }} header="TEST" />
            </Col>
          </Row>
          <h1>Цели на этот месяц</h1>
          <Row>
            <Col md={4}>
              <span
                style={{
                  borderBottom: "1px solid black",
                  fontFamily: "AmaticSC-Regular",
                  fontWeight: "bold",
                  fontSize: "24px"
                }}
              >
                Делать зарядку по утрам
              </span>
            </Col>
            <Col md={8}>
              <div>Тут будет календарик</div>
            </Col>
          </Row>
        </Col>
      </Row>
    </Container>
  );
};
