import React, { useEffect, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { IMonthPage } from "../../models";
import axios from "axios";
import Loader from "../Loader";
import { PurchasesArea } from "./PurchasesArea";
import { DesiresArea } from "./DesiresArea";
import { IdeasArea } from "./IdeasArea";
import { GoalsArea } from "./GoalsArea";

export const MonthPage: React.FC = () => {
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
          <PurchasesArea purchasesArea={monthPage.purchasesArea} />
          <DesiresArea desiresArea={monthPage.desiresArea} />
        </Col>
        <Col md={6}>
          <IdeasArea ideasArea={monthPage.ideasArea} />
          <GoalsArea goalsArea={monthPage.goalsArea} />
        </Col>
      </Row>
    </Container>
  );
};
