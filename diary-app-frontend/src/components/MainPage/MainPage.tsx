import React, { FC, useState, useEffect } from "react";
import { TodoList } from "../Lists/TodoList";
import { ImportanceList } from "../Lists/ImportanceList";
import Calendar from "../Calendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { AppState } from "../../context/app-state";
import { IMainPage } from "../../models";
import axios from "axios";
import Loader from "../Loader";

export const MainPage: FC = () => {
  const [mainPage, setMainPage] = useState<IMainPage>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    axios
      .get(
        "https://localhost:44320/api/mainpage/50e6cb71-3195-400d-aead-f0c80c460090/2020/1"
      )
      .then(res => {
        setMainPage(res.data);
      })
      .finally(() => setLoading(false));
  }, []);

  return (
    <AppState>
      <Container fluid style={{ marginTop: "20px" }}>
        <Row>
          <Col md="3" className="text-center">
            {mainPage && (
              <>
                <TodoList
                  todoList={mainPage.thingsTodo}
                  loading={loading}
                  fillToNumber={6}
                />
                <ImportanceList
                  eventList={mainPage.importantEvents}
                  withDate
                  readonly
                  loading={loading}
                  fillToNumber={6}
                />
              </>
            )}
          </Col>
          <Col md="9">
            <Calendar />
          </Col>
        </Row>
      </Container>
    </AppState>
  );
};
