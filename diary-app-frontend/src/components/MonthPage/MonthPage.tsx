import React, { useEffect } from "react";
import { Container, Row, Col } from "react-bootstrap";
import axios from "axios";
import Loader from "../Loader";
import { PurchasesArea } from "./PurchasesArea";
import { DesiresArea } from "./DesiresArea";
import { IdeasArea } from "./IdeasArea";
import { GoalsArea } from "./GoalsArea";
import { usePage } from "../../hooks/usePage";
import { IMonthPageContext, MonthPageContext } from "../../context";

export const MonthPage: React.FC = () => {
  const pageState = usePage<IMonthPageContext>({
    loading: false,
    page: null,
    setPageState: () => {}
  });
  const { loading, page, setPageState } = pageState;

  useEffect(() => {
    setPageState({ ...pageState, loading: true });
    axios
      .get(
        "https://localhost:44320/api/monthpage/50e6cb71-3195-400d-aead-f0c80c460090/2020/1"
      )
      .then(res =>
        setPageState({ ...pageState, page: res.data, loading: false })
      );
  }, []);

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
