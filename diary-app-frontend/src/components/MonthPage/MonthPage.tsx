import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import Loader from "../Loader";
import { PurchasesArea } from "./PurchasesArea";
import { DesiresArea } from "./DesiresArea";
import { IdeasArea } from "./IdeasArea";
import { GoalsArea } from "./GoalsArea";
import { usePage, PageType } from "../../hooks/usePage";
import { IMonthPageContext, MonthPageContext } from "../../context";

export const MonthPage: React.FC = () => {
  const pageState = usePage<IMonthPageContext>(
    {
      loading: false,
      page: null
    },
    PageType.MonthPage
  );

  const { loading, page } = pageState;

  console.log(pageState);

  if (loading || !page) return <Loader />;

  return (
    <MonthPageContext.Provider value={pageState}>
      <Container fluid className="mt-20 second-page-container text-center">
        <Row>
          <Col md={6}>
            <PurchasesArea />
            <DesiresArea />
          </Col>
          <Col md={6}>
            <IdeasArea />
            <GoalsArea />
          </Col>
        </Row>
      </Container>
    </MonthPageContext.Provider>
  );
};
