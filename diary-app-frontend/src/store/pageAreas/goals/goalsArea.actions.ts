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
			dispatch(goalsListsThunks.setGoalsLists(goalsArea.goalLists));
		})
	);
};

const goalsAreaActions = {
	...getPageAreaActions<IGoalsArea>("goalsArea"),
};

export type GoalsAreaActions = ActionsUnion<typeof goalsAreaActions>;
type ThunkType = BaseThunkType<GoalsAreaActions>;
