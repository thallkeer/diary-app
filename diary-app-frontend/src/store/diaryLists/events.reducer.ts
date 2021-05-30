import { PayloadAction } from "@reduxjs/toolkit";
import { IEvent, IEventList } from "models";
import { IListState } from "models/states";
import { eventItemService, eventListService } from "services/todosService";
import {
	createGenericListThunks,
	createGenericSlice,
	generateListSliceReducers,
} from "./lists.reducer";

export interface IEventListState extends IListState<IEventList, IEvent> {}

const initialState: IEventListState = {
	list: null,
};

const baseReducers =
	generateListSliceReducers<IEventListState, IEventList, IEvent>();
type EventReducersType = typeof baseReducers;

export const createEventListSlice = (listName: string) =>
	createGenericSlice<IEventListState, IEventList, IEvent, EventReducersType>({
		name: listName,
		initialState,
		reducers: {
			...baseReducers,
			setList(state: IEventListState, action: PayloadAction<IEventList>) {
				const listToSet: IEventList = {
					...action.payload,
					items: action.payload.items.map((event) => {
						return { ...event, date: new Date(event.date) };
					}),
				};
				state.list = listToSet;
			},
			addItem(state: IEventListState, action: PayloadAction<IEvent>) {
				const addedEvent: IEvent = {
					...action.payload,
					date: new Date(action.payload.date),
				};
				state.list.items.push(addedEvent);
			},
		},
	});

type EventSlice = ReturnType<typeof createEventListSlice>;

export const createEventListThunks = (slice: EventSlice) => {
	const baseThunks = createGenericListThunks<
		IEventListState,
		IEventList,
		IEvent
	>(slice.name, eventListService, eventItemService);
	return {
		...baseThunks,
	};
};
