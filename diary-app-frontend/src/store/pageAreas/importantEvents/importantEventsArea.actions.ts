import { IImportantEventsArea } from "models/PageAreas/pageAreas";
import { ActionsUnion } from "store/actions/action-helpers";
import { eventThunks } from "store/diaryLists/events.actions";
import { BaseThunkType } from "store/state.types";
import { loadPageArea, PageAreaActions } from "../pageAreas.actions";

export const IMPORTANT_EVENTS_LIST = "importantEventsList";

const actions = {};

export const loadImportantEventsArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IImportantEventsArea>(
			"importantEventsArea",
			"mainPage",
			pageID,
			(pageArea) => {
				dispatch(
					eventThunks.setList(pageArea.importantEvents, IMPORTANT_EVENTS_LIST)
				);
			}
		)
	);
};

export type ImportantEventsAreaActions =
	| ActionsUnion<typeof actions>
	| PageAreaActions;
type ThunkType = BaseThunkType<ImportantEventsAreaActions>;
