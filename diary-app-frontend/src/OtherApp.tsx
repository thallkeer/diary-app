import React, { useEffect, useState } from "react";
import { TodoList } from "./components/Lists/TodoList";
import { Container, Row, Col } from "react-bootstrap";
import { ImportanceList } from "./components/Lists/ImportanceList";
import Calendar from "./components/Calendar/Calendar";
import HabitsTracker from "./components/HabitsTracker";
import { IMonthPage } from "./models";
import axios from "axios";
import Loader from "./components/Loader";

export const OtherApp: React.FC = () => {
  const [monthPage, setMonthPage] = useState<IMonthPage>(null);

  useEffect(() => {
    axios
      .get(
        "https://localhost:44320/api/monthpage/50e6cb71-3195-400d-aead-f0c80c460090/2020/1"
      )
      .then(res => setMonthPage(res.data));
  }, []);

  return monthPage === null ? (
    <Loader />
  ) : (
    <Container
      fluid
      className="second-page-container text-center"
      style={{ marginTop: "20px" }}
    >
      <Row>
        <Col md={6}>
          <h1>{monthPage.purchasesArea.header}</h1>
          {monthPage.purchasesArea.purchasesLists.map(pl => (
            <Row>
              <Col md={5}>
                <TodoList todoList={pl} />
              </Col>
              <Col md={2}>{""}</Col>
            </Row>
          ))}
          <h1 style={{ marginTop: "40px" }}>{monthPage.desiresArea.header}</h1>
          <Row>
            {monthPage.desiresArea.desiresLists.map(dl => (
              <Col md={4}>
                <ImportanceList eventList={dl} />
              </Col>
            ))}
          </Row>
        </Col>
        <Col md={6}>
          <h1>{monthPage.ideasArea.header}</h1>
          <Row>
            <Col md={12}>
              <ImportanceList
                style={{ marginTop: "52px" }}
                eventList={monthPage.ideasArea.ideasList}
              />
            </Col>
          </Row>
          <h1>{monthPage.goalsArea.header}</h1>
          {monthPage.goalsArea.goalsLists.map(gl => (
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
                  {gl.goalName}
                </span>
              </Col>
              <Col md={8}>
                <HabitsTracker />
              </Col>
            </Row>
          ))}
        </Col>
      </Row>
    </Container>
  );
};
