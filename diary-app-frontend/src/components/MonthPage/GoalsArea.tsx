import React, { useContext } from "react";
import { IGoalsArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { HabitsTracker } from "../HabitsTracker";
import { MonthPageContext } from "../../context";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";

export const GoalsArea: React.FC = () => {
  const { areaState } = usePageArea<IGoalsArea>("goalsArea");

  if (!areaState || areaState.loading) return <Loader />;

  return (
    <>
      <h1>{areaState.area.header}</h1>
      {areaState.area.goalsLists.map(gl => (
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
            <HabitsTracker tracker={gl} />
          </Col>
        </Row>
      ))}
    </>
  );
};
