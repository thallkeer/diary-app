import React from "react";
import { Row, Col } from "react-bootstrap";
import { ImportanceList } from "../Lists/ImportanceList";
import { IDesiresArea } from "../../models";

interface IProps {
  desiresArea: IDesiresArea;
}

export const DesiresArea: React.FC<IProps> = ({ desiresArea }) => {
  return (
    <>
      <h1 className="mt-40">{desiresArea.header}</h1>
      <Row>
        {desiresArea.desiresLists.map(dl => (
          <Col md={4} key={dl.id}>
            <ImportanceList
              eventList={dl}
              fillToNumber={6}
              className="mt-10 month-lists-header"
            />
          </Col>
        ))}
      </Row>
    </>
  );
};
