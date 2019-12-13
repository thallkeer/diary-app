import React from "react";
import { TodoList } from "./components/TodoList/TodoList";
import { Container, Row, Col } from "react-bootstrap";
import { ImportanceList } from "./components/ImportanceList";

export const OtherApp: React.FC = () => {
  return (
    <Container
      className="second-page-container text-center"
      style={{ border: "1px solid red" }}
    >
      <Row>
        <Col>
          <h1>Покупки</h1>
        </Col>
        <Col>
          <h1>Идеи этого месяца</h1>
        </Col>
      </Row>
      <Row>
        <Col
          md={3}
          className="text-center text-uppercase"
          style={{ border: "1px solid red" }}
        >
          <TodoList header="Для дома" />
        </Col>
        <Col
          md={3}
          className="text-center text-uppercase todo-list-header"
          style={{ border: "1px solid red" }}
        >
          <TodoList header="Одежда" />
        </Col>
        <Col md={6} style={{ border: "1px solid red" }}>
          <ImportanceList header="" />
        </Col>
      </Row>
      <Row>
        <Col md={3} className="text-center text-uppercase">
          <TodoList header={"Красота и Здоровье"} />
        </Col>
        <Col md={3} className="text-center text-uppercase">
          <TodoList header={"Другое"} />
        </Col>
        <Col md={6}>
          <h1 className="text-center">Цели на этот месяц</h1>
          <Row>
            <Col>
              <span
                style={{
                  fontFamily: "AmaticSC-Regular",
                  borderBottom: "1px solid black",
                  fontSize: "24px",
                  fontWeight: "bold"
                }}
              >
                Делать зарядку по утрам
              </span>
            </Col>
            <Col>
              <div>Здесь будет календарик</div>
            </Col>
          </Row>
        </Col>
      </Row>
      <Row>
        <Col md={6}>
          <h1 className="text-center">В этом месяце я хочу</h1>
        </Col>
      </Row>
      <Row>
        <Col md={2}>
          <ImportanceList header="Прочитать" />
        </Col>
        <Col md={2}>
          <ImportanceList header="Посмотеть" />
        </Col>
        <Col md={2}>
          <ImportanceList header="Посетить" />
        </Col>
      </Row>
    </Container>
  );
};
