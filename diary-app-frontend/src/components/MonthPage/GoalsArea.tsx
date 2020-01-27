import React from "react";
import { IGoalsArea, IHabitsTracker } from "../../models";
import { Row, Col } from "react-bootstrap";
import { HabitsTracker } from "../HabitsTracker";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";
import { AddListBtn } from "../AddListBtn";
import { getRandomId } from "../../utils";

export const GoalsArea: React.FC = () => {
  const { areaState, setAreaState, page } = usePageArea<IGoalsArea>(
    "goalsArea"
  );

  if (!areaState || areaState.loading) return <Loader />;

  const addHabitsTracker = () => {
    const tracker: IHabitsTracker = {
      id: 0,
      goalName: "Цель на месяц",
      selectedDays: []
    };

    setAreaState({
      ...areaState,
      area: {
        ...areaState.area,
        goalsLists: [...areaState.area.goalsLists, tracker]
      }
    });
  };

  const renderRow = (tracker: IHabitsTracker, index: number) => {
    let components = [
      <Col md={4} key={getRandomId()}>
        <span className="goal-name">{tracker.goalName}</span>
      </Col>,
      <Col md={8} key={getRandomId()}>
        <HabitsTracker tracker={tracker} page={page} />
      </Col>
    ];

    return (
      <Row
        key={tracker.id !== 0 ? tracker.id : getRandomId()}
        className="mt-20"
      >
        {index % 2 === 0 ? components : components.reverse()}
      </Row>
    );
  };

  return (
    <>
      <h1>{areaState.area.header}</h1>
      {areaState.area.goalsLists.map((gl, i) => renderRow(gl, i))}
      <Row className="mt-20">
        <AddListBtn onClick={addHabitsTracker} />
      </Row>
    </>
  );
};
