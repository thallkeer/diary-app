import React, { FC, useEffect } from "react";
import Calendar from "../Calendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { ImportantThings } from "./ImportantThings";
import { ImportantEvents } from "./ImportantEvents";
import { MainPageContext, IMainPageContext } from "../../context";
import axios from "axios";
import Loader from "../Loader";
import { usePage } from "../../hooks/usePage";

export const MainPage: FC = () => {
  const pageState = usePage<IMainPageContext>({
    loading: false,
    events: null,
    page: null,
    setPageState: () => {}
  });
  const { setPageState, page, loading, events } = pageState;

  useEffect(() => {
    setPageState({
      ...pageState,
      loading: true
    });
    axios
      .get(
        "https://localhost:44320/api/mainpage/48fdadb0-0092-48a5-add6-24d6e263e588/2020/1"
      )
      .then(res => {
        setPageState({ ...pageState, page: res.data, loading: false });
      });
  }, []);

  if (loading || !page) return <Loader />;

  return (
    <MainPageContext.Provider value={pageState}>
      <Container fluid className="mt-20">
        <Row>
          <Col md="3" className="text-center">
            <ImportantThings />
            <ImportantEvents />
          </Col>
          <Col md="9">
            {events ? <Calendar eventsState={events} /> : <Loader />}
          </Col>
        </Row>
      </Container>
    </MainPageContext.Provider>
  );
};
