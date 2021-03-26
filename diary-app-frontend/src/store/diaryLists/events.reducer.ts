import { IEvent, IEventList } from "models";
import { IListState } from "models/states";
import { ActionsUnion } from "store/actions/action-helpers";
import { createNamedReducer, updateListInState } from "utils";
import { DiaryListComponent } from "./lists.reducer";

class EventListComponent {
	private listComponent: DiaryListComponent<IEventList, IEvent>;

	constructor() {
		this.listComponent = new DiaryListComponent<IEventList, IEvent>(
			"eventLists",
			"events"
		);
	}

	public getThunks(listName: string) {
		return this.listComponent.getThunks(listName);
	}

	public getReducer(initialState: IEventListState, listName: string) {
		const baseReducer = this.listComponent.getReducer(initialState, listName);
		const actions = this.listComponent.getActions(listName);
		type EventListActions = ActionsUnion<typeof actions>;
		const eventListReducer = (
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
					return baseReducer(state, action);
			}
		};
		return createNamedReducer(eventListReducer, initialState, listName);
	}
}

export const eventListComponent = new EventListComponent();

export interface IEventListState extends IListState<IEventList, IEvent> {}

const initialState: IEventListState = {
	list: null,
};

export const createEventListReducer = (reducerName: string) => {
	return eventListComponent.getReducer(initialState, reducerName);
};
