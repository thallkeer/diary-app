import { IEvent, IEventList, IListState } from "../../../models";
import { createNamedWrapperReducer, updateListInState } from "../../../utils";
import { ActionsUnion } from "../../actions/action-helpers";
import {
	createListActions,
	getListActions,
	listReducer,
} from "../../actions/list-actions";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";

export interface IEventListState extends IListState<IEventList, IEvent> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

const initialState: IEventListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: false,
	listName: "eventlist",
	...INITIAL_LOADABLE_STATE,
};

export const createEventListReducer = (reducerName: string) => {
	return createNamedWrapperReducer(
		eventListReducer,
		initialState,
		reducerName,
		(action) => action.subjectName
	);
};

export const eventListReducer = (
	state = initialState,
	action: EventListActions
): IEventListState => {
	switch (action.type) {
		case "LOAD_LIST_SUCCESS":
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
			return listReducer<IEventListState, IEventList, IEvent>(state, action);
	}
};

const actions = {
	...createListActions<IEventList, IEvent>(),
};

export const eventsActions = {
	...getListActions<IEventList, IEvent>("events/", "Event"),
};

type EventListActions = ActionsUnion<typeof actions>;
