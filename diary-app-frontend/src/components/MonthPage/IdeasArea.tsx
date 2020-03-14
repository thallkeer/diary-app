import React from "react";
import { IIdeasArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";
import { EventListState } from "../Lists/EventListState";

export const IdeasArea: React.FC = () => {
  const { areaState, page } = usePageArea<IIdeasArea>("ideasArea");

  if (!page || !areaState || areaState.loading) return <Loader />;

  return (
    <>
      <h1>{areaState.area.header}</h1>
      <Row>
        <Col md={12}>
          <EventListState initList={areaState.area.ideasList}>
            <EventList className="mt-10 no-list-header" />
          </EventListState>
        </Col>
      </Row>
    </>
  );
};
