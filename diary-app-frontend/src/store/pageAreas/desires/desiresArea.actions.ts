import { IDesiresArea } from "models/PageAreas/pageAreas";
import { ActionsUnion } from "store/actions/action-helpers";
import { desireListsThunks } from "store/pageAreaLists/desiresLists/desireLists.actions";
import { BaseThunkType } from "store/state.types";
import { getPageAreaActions, loadPageArea } from "../pageAreas.actions";

export const loadDesiresArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IDesiresArea>(
			"desiresArea",
			"monthPage",
			pageID,
			(desiresArea) => {
				dispatch(desireListsThunks.setDesireLists(desiresArea.desiresLists));
			}
		)
	);
};

const desiresAreaActions = {
	...getPageAreaActions<IDesiresArea>("desiresArea"),
};

export type DesiresAreaActions = ActionsUnion<typeof desiresAreaActions>;
type ThunkType = BaseThunkType<DesiresAreaActions>;
