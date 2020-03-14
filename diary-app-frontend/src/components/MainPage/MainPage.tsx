import React, { FC } from "react";
import Calendar from "../Calendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { ImportantThings } from "./ImportantThings";
import { ImportantEvents } from "./ImportantEvents";
import { MainPageState } from "./MainPageState";

export const MainPage: FC = () => {
  return (
    <MainPageState>
      <Container fluid className="mt-20">
        <Row>
          <Col md="3" className="text-center">
            <ImportantThings />
            <ImportantEvents />
          </Col>
          <Col md="9">
            <Calendar />
          </Col>
        </Row>
      </Container>
    </MainPageState>
  );
};
