import React, { FC, useState, useEffect, createContext } from "react";
import Calendar from "../Calendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { ImportantThings } from "./ImportantThings";
import { ImportantEvents } from "./ImportantEvents";
import { IMainPageState } from "../../context";
import axios from "axios";
import Loader from "../Loader";

export const MainPageContext = createContext<IMainPageState>(null);

export const MainPage: FC = () => {
  const [state, setState] = useState<IMainPageState>();
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    axios
      .get(
        "https://localhost:44320/api/mainpage/50e6cb71-3195-400d-aead-f0c80c460090/2020/1"
      )
      .then(res => {
        setState({ ...state, page: res.data });
        setLoading(false);
      });
  }, []);

  if (loading || !state) return <Loader />;

  console.log(state);

  const renderCalendar = () => {
    if (state.events && state.events)
      return <Calendar eventsState={state.events} />;
    return <Loader />;
  };

  return (
    <MainPageContext.Provider value={state}>
      <Container fluid className="mt-20">
        <Row>
          <Col md="3" className="text-center">
            <ImportantThings />
            <ImportantEvents setEventsState={setState} />
          </Col>
          <Col md="9">{renderCalendar()}</Col>
        </Row>
      </Container>
    </MainPageContext.Provider>
  );
};
