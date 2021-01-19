import { IHabitTracker } from "models";
import { IGoalList } from "models/entities";
import { IGoalsArea } from "models/PageAreas/pageAreas";
import { ActionsUnion } from "store/actions/action-helpers";
import { goalsListsThunks } from "store/pageAreaLists/goalsLists/goalsLists.actions";
import { BaseThunkType } from "store/state.types";
import { getPageAreaActions, loadPageArea } from "../pageAreas.actions";

export const loadGoalsArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IGoalsArea>("goalsArea", "monthPage", pageID, (goalsArea) => {
			const createGoalsList = (tracker: IHabitTracker) => {
				//just wrap habit tracker to use unified view
				const goalsList: IGoalList = {
					id: tracker.id,
					areaOwnerId: tracker.areaOwnerId,
					list: tracker,
				};
				return goalsList;
			};
			dispatch(
				goalsListsThunks.setGoalsLists(
					goalsArea.goalLists.map((ht) => createGoalsList(ht))
				)
			);
		})
	);
};

const goalsAreaActions = {
	...getPageAreaActions<IGoalsArea>("goalsArea"),
};

export type GoalsAreaActions = ActionsUnion<typeof goalsAreaActions>;
type ThunkType = BaseThunkType<GoalsAreaActions>;
