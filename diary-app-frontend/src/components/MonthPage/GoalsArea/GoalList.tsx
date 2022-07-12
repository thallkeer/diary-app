import { HabitTracker } from "components/HabitsTracker/HabitTracker";
import { DeleteBtn } from "components/Lists/Controls/DeleteBtn";
import ListHeaderInput from "components/Lists/Controls/ListHeaderInput";
import { useAppDispatch } from "hooks/hooks";
import { IHabitDay, IHabitTracker } from "models";
import React from "react";
import { Col, Row } from "react-bootstrap";
import { IHabitTrackerState } from "store/diaryLists/habitTrackers.reducer";
import {
	addOrUpdateHabitDay,
	deleteGoalList,
	deleteHabitDay,
	updateGoalList,
} from "store/pageAreaLists/goalLists.slice";

const useGoalList = (goalList: IHabitTrackerState) => {
	const { list: tracker } = goalList;
	const dispatch = useAppDispatch();

	const updateHabitTracker = (tracker: IHabitTracker) => {
		dispatch(updateGoalList(tracker));
	};

	const deleteHabitTracker = () => {
		dispatch(deleteGoalList(tracker.id));
	};

	const markDay = (day: IHabitDay) => {
		dispatch(addOrUpdateHabitDay(tracker.id, day));
	};

	const unmarkDay = (day: IHabitDay) => {
		dispatch(deleteHabitDay(tracker.id, day.id));
	};

	return {
		tracker,
		updateHabitTracker,
		deleteHabitTracker,
		markDay,
		unmarkDay,
	};
};

export const GoalList: React.FC<{
	goalList: IHabitTrackerState;
	reversed: boolean;
}> = ({ goalList, reversed }) => {
	const {
		tracker,
		updateHabitTracker,
		deleteHabitTracker,
		markDay,
		unmarkDay,
	} = useGoalList(goalList);

	const handleBlur = (title: string) => {
		if (tracker.goalName !== title)
			updateHabitTracker({ ...tracker, goalName: title });
	};

	const components = [
		<Col key={"header_" + tracker.id} md={4} className="tracker-header-column">
			<h3 className="tracker-header">
				<ListHeaderInput value={tracker.goalName} handleBlur={handleBlur} />
				<DeleteBtn onDelete={deleteHabitTracker} />
			</h3>
		</Col>,
		<Col key={"goalList_" + tracker.id} md={8}>
			<HabitTracker
				tracker={tracker}
				trackerActions={{
					updateHabitTracker,
					markDay,
					unmarkDay,
				}}
			/>
		</Col>,
	];

	return (
		<Row className="mt-20">{reversed ? components : components.reverse()}</Row>
	);
};
