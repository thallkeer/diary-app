import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import Loader from "../Loader";
import { PurchasesArea } from "./PurchasesArea";
import { DesiresArea } from "./DesiresArea";
import { IdeasArea } from "./IdeasArea";
import { GoalsArea } from "./GoalsArea";
import { usePage, PageType } from "../../hooks/usePage";
import { IMonthPageContext, MonthPageContext } from "../../context";
import { Link } from "react-router-dom";
import strelka from "../../images/strelochkaa.png";

export const MonthPage: React.FC = () => {
  const pageState = usePage<IMonthPageContext>(
    {
      loading: false,
      page: null
    },
    PageType.MonthPage
  );

  const { loading, page } = pageState;

  if (loading || !page) return <Loader />;

  return (
    <MonthPageContext.Provider value={pageState}>
      <Container fluid className="mt-20 second-page-container text-center">
        <Link
          className="month-name"
          style={{ border: "1px solid green", cursor: "pointer" }}
          to="/"
        >
          <img
            src={strelka}
            alt="return to calendar"
            className="mirrored-arrow"
            style={{ border: "1px solid red", cursor: "pointer" }}
            width="30"
            height="30"
          />
        </Link>
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
