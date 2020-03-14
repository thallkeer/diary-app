import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import Loader from "../Loader";
import { PurchasesArea } from "./PurchasesArea";
import { DesiresArea } from "./DesiresArea";
import { IdeasArea } from "./IdeasArea";
import { GoalsArea } from "./GoalsArea";
import { MonthPageContext } from "../../context";
import { Link } from "react-router-dom";
import strelka from "../../images/strelochkaa.png";
import { usePage } from "../../hooks/usePage";
import { PageType } from "../../context/actions/page-actions";

export const MonthPage: React.FC = () => {
  const pageState = usePage(PageType.MonthPage);

  const { loading, page } = pageState;

  if (loading || !page) return <Loader />;

  return (
    <MonthPageContext.Provider value={pageState}>
      <Container fluid className="mt-20 second-page-container text-center">
        <Link
          className="month-name"
          style={{
            position: "absolute",
            left: "0",
            pointerEvents: "all",
            cursor: "pointer",
            zIndex: 10
          }}
          to="/"
        >
          <img
            src={strelka}
            alt="return to calendar"
            className="mirrored-arrow"
            style={{ cursor: "pointer" }}
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
