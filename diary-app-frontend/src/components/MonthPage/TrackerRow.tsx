import React, { useState } from "react";
import { IHabitsTracker } from "../../models";
import { Row, Col } from "react-bootstrap";
import { HabitsTracker } from "../HabitsTracker";
import ListHeaderInput from "../Lists/ListHeaderInput";

export const TrackerRow: React.FC<{
  tracker: IHabitsTracker;
  index: number;
  onAddUpdate: (tracker: IHabitsTracker) => void;
}> = ({ index, tracker, onAddUpdate }) => {
  const [state, setState] = useState(tracker);

  const handleBlur = () => {
    onAddUpdate(state);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    //e.preventDefault();
    setState({
      ...state,
      goalName: e.target.value
    });
  };

  let components = [
    <Col key={index + 1} md={4}>
      <h3 style={{ marginTop: "15px" }}>
        <ListHeaderInput
          value={state.goalName}
          handleBlur={handleBlur}
          // handleChange={handleChange}
        />
      </h3>
    </Col>,
    <Col key={index + 2} md={8}>
      <HabitsTracker tracker={tracker} updateTracker={onAddUpdate} />
    </Col>
  ];

  return (
    <Row className="mt-20">
      {index % 2 === 0 ? components : components.reverse()}
    </Row>
  );
};
