import React from "react";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import { IDesiresArea } from "../../models";
import { PageAreaResult } from "../../hooks/usePageArea";
import { EventListState } from "../Lists/EventListState";
import { MonthArea } from "./MonthAreaHOC";

const DesiresArea: React.FC = () => {
  const desiresArea = (areaProps: PageAreaResult<IDesiresArea>) => (
    <Row>
      {areaProps.pageAreaState.area.desiresLists.map((eventList) => (
        <Col md={4} key={eventList.id}>
          <EventListState initList={eventList}>
            <EventList
              className="mt-10 month-lists-header no-list-header-border"
              readonly={false}
            />
          </EventListState>
        </Col>
      ))}
    </Row>
  );

  return (
    <MonthArea
      areaName="desiresArea"
      areaBody={desiresArea}
      className="mt-40"
    />
  );
};

export default DesiresArea;
