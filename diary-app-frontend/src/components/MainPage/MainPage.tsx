import React, { FC, useState, useEffect, useCallback } from "react";
import Calendar from "../Calendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { ImportantThings } from "./ImportantThings";
import { ImportantEvents } from "./ImportantEvents";
import {
  IMainPageContext,
  IEventListContext,
  MainPageContext
} from "../../context";
import axios from "axios";
import Loader from "../Loader";

export const useMainPage = (): IMainPageContext => {
  const [pageState, _setPageState] = useState<IMainPageContext>({
    loading: false,
    events: null,
    page: null,
    setPageState: () => {}
  });

  const setPageState = useCallback((pageState: IMainPageContext): void => {
    _setPageState({ ...pageState });
  }, []);

  return {
    ...pageState,
    setPageState
  };
};

export const MainPage: FC = () => {
  const pageState = useMainPage();
  const { setPageState, page, loading, events } = pageState;

  useEffect(() => {
    setPageState({
      ...pageState,
      loading: true
    });
    axios
      .get(
        "https://localhost:44320/api/mainpage/50e6cb71-3195-400d-aead-f0c80c460090/2020/1"
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
