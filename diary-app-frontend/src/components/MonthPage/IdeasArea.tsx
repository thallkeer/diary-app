import React from "react";
import { IIdeasArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import { PageAreaResult } from "../../hooks/usePageArea";
import { EventListState } from "../Lists/EventListState";
import { MonthArea } from "./MonthAreaHOC";

const IdeasArea: React.FC = () => {
  const ideasArea = (areaProps: PageAreaResult<IIdeasArea>) => (
    <Row>
      <Col md={12}>
        <EventListState initList={areaProps.pageAreaState.area.ideasList}>
          <EventList className="mt-10 no-list-header" renderHeader={false} />
        </EventListState>
      </Col>
    </Row>
  );

  return <MonthArea areaName="ideasArea" areaBody={ideasArea} />;
};

export default IdeasArea;
