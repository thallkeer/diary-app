import React, { useReducer } from "react";
import { IGoalsArea, IHabitsTracker } from "../../models";
import { Row } from "react-bootstrap";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";
import { AddListBtn } from "../AddListBtn";
import { IHabitTrackerContext, HabitsTrackerContext } from "../../context";
import { trackersReducer } from "../../context/trackers";
import {
  HabitTrackerThunks,
  Thunks as trackerThunks
} from "../../actions/habitTracker-actions";
import { TrackerRow } from "./TrackerRow";

export const GoalsArea: React.FC = () => {
  const { areaState, page } = usePageArea<IGoalsArea>("goalsArea");
  const initialValue: IHabitTrackerContext = {
    trackers: areaState
      ? areaState.area
        ? areaState.area.goalsLists
        : []
      : [],
    dispatch: null
  };
  const [trackersState, _dispatch] = useReducer(trackersReducer, initialValue);

  if (!areaState || areaState.loading) return <Loader />;

  const { addOrUpdateTracker } = trackerThunks;

  const addHabitsTracker = () => {
    const tracker: IHabitsTracker = {
      id: 0,
      goalName: "Цель на месяц",
      selectedDays: [],
      goalsAreaId: areaState.area.id
    };

    addOrUpdateTracker(tracker)(_dispatch);
  };

  return (
    <>
      <h1 className="mt-40">{areaState.area.header}</h1>
      <HabitsTrackerContext.Provider
        value={{
          trackers: trackersState.trackers,
          dispatch: (action: HabitTrackerThunks) => action(_dispatch)
        }}
      >
        {areaState.area.goalsLists.map((gl, i) => (
          <TrackerRow key={i} tracker={gl} index={i} page={page} />
        ))}
        <Row className="mt-20">
          <AddListBtn onClick={addHabitsTracker} />
        </Row>
      </HabitsTrackerContext.Provider>
    </>
  );
};
