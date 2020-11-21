import React, { useReducer, createContext } from "react";
// import { IEventList, IEvent, IListState } from "../../../models";
// import { IListContext } from "../../../context";
// import { EventListActions } from "../../../context/actions/events-actions";
// import { eventListReducer } from "../../../context/reducers/list/events";
// import { useListActions } from "../../../context/actions/list-actions";
// import { useList } from "../../../hooks/useList";

// export interface IEventListState extends IListState<IEventList, IEvent> {}

// export interface IEventListContext extends IListContext {
// 	eventListState: IEventListState;
// 	dispatch: React.Dispatch<EventListActions>;
// }

// const initialState: IEventListState = {
// 	list: null,

// };

// const initialContext: IEventListContext = {
// 	eventListState: initialState,
// 	dispatch: () => null,
// };

// const EventListContext = createContext(initialContext);

// const {
// 	addOrUpdateListItem,
// 	deleteListItem,
// 	loadListByPageID,
// 	setList,
// 	updateList,
// } = useListActions("events/", "Event");

// const EventListState: React.FC = ({ children }) => {
// 	const [state, dispatch] = useReducer(eventListReducer, initialState);
// 	const eventList = state.list;

// 	const loadEventListByPageID = (pageID: number) =>
// 		loadListByPageID(pageID, dispatch);

// 	useList(eventList, loadEventListByPageID);

// 	return (
// 		<EventListContext.Provider
// 			value={{
// 				eventListState: state,
// 				dispatch,
// 				listFunctions: {
// 					addOrUpdateItem: (item: IEvent) =>
// 						addOrUpdateListItem({ ...item, ownerID: eventList.id }, dispatch),
// 					deleteListItem: (itemID) => deleteListItem(itemID, dispatch),
// 					loadByPageID: loadEventListByPageID,
// 					setList: (list: IEventList) => setList(list, dispatch),
// 					updateListTitle: (title) =>
// 						updateList({ ...eventList, title }, dispatch),
// 				},
// 			}}
// 		>
// 			{children}
// 		</EventListContext.Provider>
// 	);
// };

// export { EventListContext, EventListState };
