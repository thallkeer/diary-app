import { ListsStateByName } from "models/states";
import {
	commonListInitialState,
	createCommonListReducer,
	ICommonListState,
} from "store/diaryLists/commonLists.reducer";
import {
	ListCollectionHandler,
	ReducerCollection,
} from "../listCollectionHandler";
import { DesireListsActions } from "./desireLists.actions";

export const desireListsHandler = new ListCollectionHandler(
	new ReducerCollection(
		commonListInitialState,
		createCommonListReducer,
		"desireLists"
	)
);

export const desireListsReducer = (
	state = {} as ListsStateByName<ICommonListState>,
	action: DesireListsActions
): ListsStateByName<ICommonListState> => {
	switch (action.type) {
		case "SET_DESIRE_LISTS":
			return desireListsHandler.handleSetLists(action.payload);
		default:
			return desireListsHandler.handleListAction(
				state,
				action.subjectName,
				(reducer, desireListState) => reducer(desireListState, action)
			);
	}
};
