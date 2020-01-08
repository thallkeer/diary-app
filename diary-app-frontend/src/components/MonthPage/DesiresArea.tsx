import React from "react";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import { IDesiresArea } from "../../models";

export const DesiresArea: React.FC = () => {
  return (
    <>
      <h1 className="mt-40">{desiresArea.header}</h1>
      <Row>
        {desiresArea.desiresLists.map(dl => (
          <Col md={4} key={dl.id}>
            <EventList fillToNumber={6} className="mt-10 month-lists-header" />
          </Col>
        ))}
      </Row>
    </>
  );
};
