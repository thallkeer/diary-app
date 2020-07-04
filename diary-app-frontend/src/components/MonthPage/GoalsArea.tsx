import React, { createContext, useContext } from "react";
import { IHabitsTracker } from "../../models";
import { Row } from "react-bootstrap";
import { AddListBtn } from "../AddListBtn";
import { TrackerRow } from "./TrackerRow";
import { GoalsAreaState, goalsAreaContext } from "./GoalsAreaState";
import Loader from "../Loader";

interface IHabitsTrackerState {
	tracker: IHabitsTracker;
}

export const HabitsTrackerContext = createContext<IHabitsTrackerState>({
	tracker: null,
});

const GoalsAreaLists = () => {
	const { goalsAreaState, addHabitsTracker } = useContext(goalsAreaContext);
	const { area, loading } = goalsAreaState;

	if (!area || loading) return <Loader />;

	return (
		<>
			<h1 className="mt-40 area-header">{area.header}</h1>
			{area.goalsLists.map((gl, i) => (
				<HabitsTrackerContext.Provider key={gl.id} value={{ tracker: gl }}>
					<TrackerRow key={gl.id} index={i} />
				</HabitsTrackerContext.Provider>
			))}
			<Row className="mt-20">
				<AddListBtn onClick={() => addHabitsTracker()} />
			</Row>
		</>
	);
};

const GoalsArea: React.FC = () => {
	return (
		<GoalsAreaState>
			<GoalsAreaLists />
		</GoalsAreaState>
	);
};

export default GoalsArea;
