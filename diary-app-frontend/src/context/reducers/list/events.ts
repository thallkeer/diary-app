import { IEvent, IEventList } from "../../../models/entities";
import { IDiaryListState } from "../../../models/states";
import { createNamedWrapperReducer, updateListInState } from "../../../utils";
import { ActionsUnion } from "../../actions/action-helpers";
import {
	createDiaryListActions,
	getDiaryListActions,
} from "../../actions/diaryList-actions";
import { getListItemActions } from "../../actions/listCrud-actions";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { diaryListReducer } from "./diaryListReducer";

export interface IEventListState extends IDiaryListState<IEventList, IEvent> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

const initialState: IEventListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: false,
	listName: "eventlist",
};

export const createEventListReducer = (reducerName: string) => {
	return createNamedWrapperReducer(eventListReducer, initialState, reducerName);
};

export const eventListReducer = (
	state = initialState,
	action: EventListActions
): IEventListState => {
	switch (action.type) {
		case "SET_LIST":
			const newState: IEventListState = {
				...state,
				list: action.payload,
			};

			return updateListInState(newState, (listItems) =>
				listItems.map((event) => {
					return { ...event, date: new Date(event.date) };
				})
			);

		case "ADD_LIST_ITEM":
			let addedEvent = action.payload;

			addedEvent.date = new Date(addedEvent.date);

			return updateListInState(state, (listItems) => [
				...listItems,
				addedEvent,
			]);

		default:
			return diaryListReducer<IEventListState, IEventList, IEvent>(
				state,
				action
			);
	}
};

const actions = {
	...createDiaryListActions<IEventList, IEvent>(),
};

export const eventsActions = {
	...getDiaryListActions<IEventList, IEvent>("eventLists"),
	...getListItemActions<IEvent>("events"),
};

type EventListActions = ActionsUnion<typeof actions>;
