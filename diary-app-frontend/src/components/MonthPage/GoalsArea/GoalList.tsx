import { IHabitDay, IHabitTracker } from "models";
import React from "react";
import { Col, Row } from "react-bootstrap";
import { useDispatch } from "react-redux";
import {
	habitTrackerComponent,
	IHabitTrackerState,
} from "store/diaryLists/habitTrackers.reducer";
import { goalsListsHandler } from "store/pageAreaLists/goalsLists/goalsLists.reducer";
import { goalsListsThunks } from "../../../store/pageAreaLists/goalsLists/goalsLists.actions";
import { HabitTracker } from "../../HabitsTracker/HabitTracker";
import { DeleteBtn } from "../../Lists/Controls/DeleteBtn";
import ListHeaderInput from "../../Lists/Controls/ListHeaderInput";

const useGoalList = (goalList: IHabitTrackerState) => {
	const dispatch = useDispatch();
	const { list: tracker } = goalList;
	const listName = goalsListsHandler.getListName(tracker.id);
	const listThunks = habitTrackerComponent.getThunks(listName);

	const updateHabitTracker = (tracker: IHabitTracker) => {
		dispatch(listThunks.updateList(tracker));
	};

	const deleteHabitTracker = () => {
		dispatch(goalsListsThunks.deleteGoalsList(tracker.id));
	};

	const markDay = (day: IHabitDay) => {
		dispatch(listThunks.addOrUpdateListItem(day));
	};

	const unmarkDay = (day: IHabitDay) => {
		dispatch(listThunks.deleteListItem(day.id));
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
		<Col key={"header_" + tracker.id} md={4}>
			<h3 className="tracker-header" style={{ marginTop: "15px" }}>
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
