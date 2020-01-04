import React from "react";
import { IIdeasArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { ImportanceList } from "../Lists/ImportanceList";

interface IProps {
  ideasArea: IIdeasArea;
}

export const IdeasArea: React.FC<IProps> = ({ ideasArea }) => {
  return (
    <>
      <h1>{ideasArea.header}</h1>
      <Row>
        <Col md={12}>
          <ImportanceList
            eventList={ideasArea.ideasList}
            className="mt-10 no-list-header"
            fillToNumber={6}
          />
        </Col>
      </Row>
    </>
  );
};
