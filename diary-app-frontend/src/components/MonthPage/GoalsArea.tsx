import React, { useReducer, createContext } from "react";
import { IGoalsArea, IHabitsTracker } from "../../models";
import { Row } from "react-bootstrap";
import { PageAreaResult } from "../../hooks/usePageArea";
import { AddListBtn } from "../AddListBtn";
import { trackersReducer } from "../../context/reducers/trackers";
import {
  HabitTrackerThunks,
  Thunks as trackerThunks,
} from "../../context/actions/habitTracker-actions";
import { TrackerRow } from "./TrackerRow";
import { GoalsAreaContext } from "../../context";
import { MonthArea } from "./MonthAreaHOC";

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

  {
    /* <h1 className="mt-40 area-header">{header}</h1> */
  }

  return (
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
  );
};

const GoalsArea: React.FC = () => {
  return (
    <MonthArea
      areaName="goalsArea"
      areaBody={(areaProps: PageAreaResult<IGoalsArea>) => (
        <Area goalsArea={areaProps.pageAreaState.area} />
      )}
      className="mt-40"
    />
  );
};

export default GoalsArea;
