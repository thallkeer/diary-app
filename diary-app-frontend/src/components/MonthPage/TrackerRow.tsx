import React, { useContext, useState } from "react";
import { IHabitsTracker, IMonthPage } from "../../models";
import { Row, Col } from "react-bootstrap";
import { HabitsTracker } from "../HabitsTracker";
import ListHeaderInput from "../Lists/ListHeaderInput";
import { HabitsTrackerContext } from "../../context";
import { Thunks as trackerThunks } from "../../actions/habitTracker-actions";

export const TrackerRow: React.FC<{
  tracker: IHabitsTracker;
  index: number;
  page: IMonthPage;
}> = ({ index, tracker, page }) => {
  const [state, setState] = useState(tracker);
  const { dispatch } = useContext(HabitsTrackerContext);

  const handleBlur = () => {
    dispatch(trackerThunks.addOrUpdateTracker(state));
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    console.log(state, e.target.value);
    e.preventDefault();
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
          handleChange={handleChange}
        />
      </h3>
    </Col>,
    <Col key={index + 2} md={8}>
      <HabitsTracker tracker={tracker} page={page} />
    </Col>
  ];

  return (
    <Row className="mt-20">
      {index % 2 === 0 ? components : components.reverse()}
    </Row>
  );
};
