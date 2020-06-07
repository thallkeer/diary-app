import React, { useContext } from "react";
import { Row, Col } from "react-bootstrap";
import { HabitsTracker } from "../HabitsTracker/HabitsTracker";
import ListHeaderInput from "../Lists/ListHeaderInput";
import { HabitsTrackerContext } from "./GoalsArea";
import { GoalsAreaContext } from "../../context";
import { DeleteBtn } from "../Lists/DeleteBtn";

export const TrackerRow: React.FC<{
  index: number;
}> = ({ index }) => {
  const { tracker } = useContext(HabitsTrackerContext);
  const { addOrUpdate, deleteTracker } = useContext(GoalsAreaContext);

  const handleBlur = (title: string) => {
    addOrUpdate({ ...tracker, goalName: title });
  };

  let components = [
    <Col key={index + 1} md={4}>
      <h3 className="tracker-header" style={{ marginTop: "15px" }}>
        <ListHeaderInput value={tracker.goalName} handleBlur={handleBlur} />
        <DeleteBtn onDelete={() => deleteTracker(tracker)} />
      </h3>
    </Col>,
    <Col key={index + 2} md={8}>
      <HabitsTracker />
    </Col>,
  ];

  return (
    <Row className="mt-20">
      {index % 2 === 0 ? components : components.reverse()}
    </Row>
  );
};
