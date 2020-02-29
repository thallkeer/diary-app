import React, { FC } from "react";
import Calendar from "../Calendar/Calendar";
import { Container, Row, Col } from "react-bootstrap";
import { ImportantThings } from "./ImportantThings";
import { ImportantEvents } from "./ImportantEvents";
import { MainPageContext, IMainPageContext } from "../../context";
import Loader from "../Loader";
import { usePage, PageType } from "../../hooks/usePage";

export const MainPage: FC = () => {
  const pageState = usePage<IMainPageContext>(
    {
      loading: false,
      events: null,
      page: null,
      setPageState: () => {}
    },
    PageType.MainPage
  );

  const { loading, page } = pageState;

  if (loading || !page) return <Loader />;

  console.log(page);

  return (
    <MainPageContext.Provider value={pageState}>
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
    </MainPageContext.Provider>
  );
};
