import React, { createContext } from "react";
import { IHabitsTracker } from "../../models";
import {
	goalsAreaReducer,
	IGoalsAreaState,
} from "../../context/reducers/pageArea/goalsArea";
import {
	addOrUpdateTracker,
	deleteTracker,
	goalsAreaActions,
} from "../../context/actions/goalsArea-actions";
import { useMonthArea } from "./MonthAreaHOC";

interface IGoalsAreaContext {
	goalsAreaState: IGoalsAreaState;
	addHabitsTracker: (tracker?: IHabitsTracker) => void;
	deleteHabitsTracker: (tracker: IHabitsTracker) => void;
}

const goalsAreaContext = createContext<IGoalsAreaContext>({
	goalsAreaState: {
		loading: false,
		area: null,
	},
	addHabitsTracker: null,
	deleteHabitsTracker: null,
});
const { Provider } = goalsAreaContext;

const GoalsAreaState: React.FC = ({ children }) => {
	const [state, dispatch] = useMonthArea(
		"goalsArea",
		goalsAreaReducer,
		goalsAreaActions.setArea
	);

	const addHabitsTracker = (tracker?: IHabitsTracker) => {
		if (!tracker) {
			tracker = {
				id: 0,
				goalName: "Цель на месяц",
				selectedDays: [],
				goalsAreaId: state.area.id,
			};
		}

		addOrUpdateTracker(tracker, dispatch);
	};

	const deleteHabitsTracker = (tracker: IHabitsTracker) => {
		deleteTracker(tracker, dispatch);
	};

	return (
		<Provider
			value={{
				deleteHabitsTracker,
				addHabitsTracker,
				goalsAreaState: state,
			}}
		>
			{children}
		</Provider>
	);
};

export { goalsAreaContext, GoalsAreaState };
