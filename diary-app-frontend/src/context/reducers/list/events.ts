// import { EventListActions } from "../../actions/events-actions";
import { listReducer } from "../../actions/list-actions";
// import { getEvents } from "../../../selectors";
// import { IEventListState } from "../../../components/Lists/EventList/EventListState";

// export const eventListReducer = (
// 	state: IEventListState,
// 	action: EventListActions
// ): IEventListState => {
// 	switch (action.type) {
// 		case "SET_LIST":
// 			const eventList = action.payload;
// 			const events = eventList.items || [];
// 			return {
// 				...state,
// 				list: {
// 					...eventList,
// 					items: events.map((event) => {
// 						return { ...event, date: new Date(event.date) };
// 					}),
// 				},
// 				loading: false,
// 			};

// 		case "ADD_LIST_ITEM":
// 			let addedEvent = action.payload;

// 			addedEvent.date = new Date(addedEvent.date);

// 			return {
// 				...state,
// 				list: {
// 					...state.list,
// 					items: [...getEvents(state), addedEvent],
// 				},
// 			};

// 		default:
// 			return listReducer(state, action);
// 	}
// };

// // export const eventsReducer = (
// // 	state: IEventListState,
// // 	action: EventActions
// // ): IEventListState => {
// // 	switch (action.type) {
// // 		case "LOAD_EVENTS_START":
// // 			return { ...state, loading: true };

// // 		case "LOAD_EVENTS": {
// // 			const eventList = action.payload;
// // 			const events = eventList.items || [];

// // 			return {
// // 				...state,
// // 				eventList: {
// // 					...eventList,
// // 					items: events.map((event) => {
// // 						return { ...event, date: event.date ? new Date(event.date) : null };
// // 					}),
// // 				},
// // 				loading: false,
// // 			};
// // 		}
// // 		case "ADD_EVENT":
// // 			let addedEvent = action.payload;

// // 			addedEvent.date = new Date(addedEvent.date);

// // 			return {
// // 				...state,
// // 				eventList: {
// // 					...state.eventList,
// // 					items: [...getEvents(state), addedEvent],
// // 				},
// // 			};

// // 		case "UPDATE_EVENT":
// // 			return {
// // 				...state,
// // 				eventList: {
// // 					...state.eventList,
// // 					items: getEvents(state).map((item) =>
// // 						item.id === action.payload.id ? action.payload : item
// // 					),
// // 				},
// // 			};

// // 		case "DELETE_EVENT":
// // 			return {
// // 				...state,
// // 				eventList: {
// // 					...state.eventList,
// // 					items: getEvents(state).filter(
// // 						(event) => event.id !== action.payload
// // 					),
// // 				},
// // 			};

// // 		case "UPDATE_EVENTLIST":
// // 			return {
// // 				...state,
// // 				eventList: action.payload,
// // 			};

// // 		case "DELETE_EVENT_LIST":
// // 			return {
// // 				...state,
// // 				eventList: null,
// // 			};

// // 		default:
// // 			console.log("unknown action type reducer", action, state);
// // 			return state;
// // 	}
// // };
