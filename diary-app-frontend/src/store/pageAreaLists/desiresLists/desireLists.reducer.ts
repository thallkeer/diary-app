import { IDesireList } from "models/entities";
import { ListsStateByName } from "models/states";
import {
	commonListInitialState,
	CommonListReducerType,
	createCommonListReducer,
	ICommonListState,
} from "store/diaryLists/commonLists.reducer";
import {
	ListCollectionHandler,
	ReducerCollection,
} from "../listCollectionHandler";
import { DesireListsActions } from "./desireLists.actions";

export interface IDesireListState {
	desireListId: number;
	desireAreaId: number;
	listState: ICommonListState;
}

class DesireListsReducerCollection extends ReducerCollection<
	CommonListReducerType,
	IDesireList,
	IDesireListState
> {
	reducerNamePrefix = "desireLists";

	createReducer(listName: string) {
		return createCommonListReducer(listName);
	}

	createState(desireList: IDesireList) {
		const purchaseListState: IDesireListState = {
			desireListId: desireList.id,
			desireAreaId: desireList.areaOwnerId,
			listState: {
				...commonListInitialState,
				list: desireList.list,
				listName: "commonList_" + desireList.list.id,
			},
		};
		return purchaseListState;
	}
}

export const desireListsHandler = new ListCollectionHandler<
	IDesireListState,
	IDesireList,
	ICommonListState,
	CommonListReducerType
>(new DesireListsReducerCollection());

export const desireListsReducer = (
	state = {} as ListsStateByName<IDesireListState>,
	action: DesireListsActions
): ListsStateByName<IDesireListState> => {
	switch (action.type) {
		case "SET_DESIRE_LISTS":
			return desireListsHandler.handleSetLists(action.payload);
		default:
			return desireListsHandler.handleListAction(
				state,
				action.subjectName,
				(reducer, desireListState) => reducer(desireListState.listState, action)
			);
	}
};
