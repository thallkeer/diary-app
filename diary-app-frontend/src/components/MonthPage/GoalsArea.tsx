import React from "react";
import { IGoalsArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import HabitsTracker from "../HabitsTracker";

interface IProps {
  goalsArea: IGoalsArea;
}

export const GoalsArea: React.FC<IProps> = ({ goalsArea }) => {
  return (
    <>
      <h1>{goalsArea.header}</h1>
      {goalsArea.goalsLists.map(gl => (
        <Row key={gl.id}>
          <Col md={4}>
            <span
              style={{
                borderBottom: "1px solid black",
                fontFamily: "AmaticSC-Regular",
                fontWeight: "bold",
                fontSize: "24px"
              }}
            >
              {gl.goalName}
            </span>
          </Col>
          <Col md={8}>
            <HabitsTracker />
          </Col>
        </Row>
      ))}
    </>
  );
};
