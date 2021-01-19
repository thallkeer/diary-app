import { IHabitDay, IHabitTracker } from "models";
import React from "react";
import { Col, Row } from "react-bootstrap";
import { useDispatch } from "react-redux";
import {
	goalsListsHandler,
	IGoalsListState,
} from "store/pageAreaLists/goalsLists/goalsLists.reducer";
import { goalsListsThunks } from "../../../store/pageAreaLists/goalsLists/goalsLists.actions";
import { HabitTracker } from "../../HabitsTracker/HabitTracker";
import { DeleteBtn } from "../../Lists/Controls/DeleteBtn";
import ListHeaderInput from "../../Lists/Controls/ListHeaderInput";

const useGoalList = (goalList: IGoalsListState) => {
	const dispatch = useDispatch();
	const { goalListId, listState } = goalList;
	const tracker = listState.list;
	const listName = goalsListsHandler.getListName(goalListId);

	const updateHabitTracker = (tracker: IHabitTracker) => {
		dispatch(goalsListsThunks.updateList(tracker, listName));
	};

	const deleteHabitTracker = () => {
		dispatch(goalsListsThunks.deleteGoalsList(goalListId));
	};

	const markDay = (day: IHabitDay) => {
		dispatch(goalsListsThunks.addOrUpdateListItem(day, listName));
	};

	const unmarkDay = (day: IHabitDay) => {
		dispatch(goalsListsThunks.deleteListItem(day.id, listName));
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
	goalList: IGoalsListState;
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
		<Col key={"header_" + goalList.goalListId} md={4}>
			<h3 className="tracker-header" style={{ marginTop: "15px" }}>
				<ListHeaderInput value={tracker.goalName} handleBlur={handleBlur} />
				<DeleteBtn onDelete={deleteHabitTracker} />
			</h3>
		</Col>,
		<Col key={"goalList_" + goalList.goalListId} md={8}>
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
