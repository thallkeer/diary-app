import { IEventList, IPageArea, IPageAreaState } from "../../../models";
import { ActionsUnion } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { createEventListReducer, eventsActions } from "../list/events";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import {
	createNamedWrapperPageAreaReducer,
	loadPageArea,
} from "./pageArea-reducer";

export interface IImportantEventsAreaState extends IPageAreaState {}

const initialState: IImportantEventsAreaState = {
	area: null,
	pageAreaName: "importantEventsArea",
	...INITIAL_LOADABLE_STATE,
};

export const importantEventsAreaReducer = createNamedWrapperPageAreaReducer(
	initialState,
	initialState.pageAreaName
);

export const IMPORTANT_EVENTS_LIST = "importantEventsList";

export const importantEventsListReducer = createEventListReducer(
	IMPORTANT_EVENTS_LIST
);

const actions = {};

interface IImportantEventsArea extends IPageArea {
	importantEvents: IEventList;
}

export const loadImportantEventsArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IImportantEventsArea>(
			initialState.pageAreaName,
			"mainPage",
			pageID,
			(pageArea) => {
				dispatch(
					eventsActions.setList(pageArea.importantEvents, IMPORTANT_EVENTS_LIST)
				);
			}
		)
	);
};

export type ImportantEventsAreaActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<ImportantEventsAreaActions>;
