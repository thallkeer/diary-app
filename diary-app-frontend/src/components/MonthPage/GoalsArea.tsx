import React, { useReducer, createContext } from "react";
import { IGoalsArea, IHabitsTracker } from "../../models";
import { Row } from "react-bootstrap";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";
import { AddListBtn } from "../AddListBtn";
import { trackersReducer } from "../../context/trackers";
import {
  HabitTrackerThunks,
  Thunks as trackerThunks,
} from "../../context/actions/habitTracker-actions";
import { TrackerRow } from "./TrackerRow";
import { GoalsAreaContext } from "../../context";

interface IHabitsTrackerState {
  tracker: IHabitsTracker;
}

export const HabitsTrackerContext = createContext<IHabitsTrackerState>(null);

const TrackerState: React.FC<{ index: number; tracker: IHabitsTracker }> = ({
  index,
  tracker,
}) => {
  return (
    <HabitsTrackerContext.Provider value={{ tracker }}>
      <TrackerRow key={index} index={index} />
    </HabitsTrackerContext.Provider>
  );
};

const Area: React.FC<{ goalsArea: IGoalsArea }> = ({ goalsArea }) => {
  const [trackersState, _dispatch] = useReducer(trackersReducer, goalsArea);
  const dispatch = (action: HabitTrackerThunks) => action(_dispatch);
  const { addOrUpdateTracker, deleteTracker } = trackerThunks;
  const { id, header, goalsLists } = trackersState;

  const addHabitsTracker = (tracker?: IHabitsTracker) => {
    if (!tracker) {
      tracker = {
        id: 0,
        goalName: "Цель на месяц",
        selectedDays: [],
        goalsAreaId: id,
      };
    }
    dispatch(addOrUpdateTracker(tracker));
  };

  const deleteHabitsTracker = (tracker: IHabitsTracker) => {
    dispatch(deleteTracker(tracker));
  };

  return (
    <>
      <h1 className="mt-40">{header}</h1>
      <GoalsAreaContext.Provider
        value={{
          ...trackersState,
          dispatch,
          deleteTracker: deleteHabitsTracker,
          addOrUpdate: addHabitsTracker,
        }}
      >
        {goalsLists.map((gl, i) => (
          <TrackerState index={i} key={i} tracker={gl} />
        ))}
        <Row className="mt-20">
          <AddListBtn onClick={() => addHabitsTracker()} />
        </Row>
      </GoalsAreaContext.Provider>
    </>
  );
};

export const GoalsArea: React.FC = () => {
  const { areaState } = usePageArea<IGoalsArea>("goalsArea");

  if (!areaState || areaState.loading || !areaState.area) return <Loader />;

  return <Area goalsArea={areaState.area} />;
};
