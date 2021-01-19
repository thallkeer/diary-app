import { IImportantThingsArea } from "models/PageAreas/pageAreas";
import { ActionsUnion } from "store/actions/action-helpers";
import { todoThunks } from "store/diaryLists/todos.actions";
import { BaseThunkType } from "store/state.types";
import { loadPageArea, PageAreaActions } from "../pageAreas.actions";

export const IMPORTANT_THINGS_LIST = "importantThingsList";

const actions = {};

export const loadImportantThingsArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IImportantThingsArea>(
			"importantThingsArea",
			"mainPage",
			pageID,
			(pageArea) => {
				dispatch(
					todoThunks.setList(pageArea.importantThings, IMPORTANT_THINGS_LIST)
				);
			}
		)
	);
};

export type ImportantThingsAreaActions =
	| ActionsUnion<typeof actions>
	| PageAreaActions;
type ThunkType = BaseThunkType<ImportantThingsAreaActions>;
