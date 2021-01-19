import { IEvent, IEventList } from "models";
import { IDiaryListState } from "models/states";
import { createNamedReducer, updateListInState } from "utils";
import { diaryListReducer } from "./diaryLists.reducer";
import { EventListActions } from "./events.actions";

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
	return createNamedReducer(eventListReducer, initialState, reducerName);
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
			const addedEvent: IEvent = {
				...action.payload,
				date: new Date(action.payload.date),
			};

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
