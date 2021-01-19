import { ICommonList, IListItem } from "models";
import { IDesireList } from "models/entities";
import { IDiaryListWrapperCollectionState } from "models/states";
import { ListWrapperUrls } from "models/types";
import {
	commonListInitialState,
	commonListReducer,
	createCommonListReducer,
	ICommonListState,
} from "store/diaryLists/commonLists.reducer";
import { ListCollectionHandler } from "../listCollectionHandler";
import { DesireListsActions } from "./desireLists.actions";

const DESIRE_LIST: ListWrapperUrls = "desireLists";

export interface IDesireListState {
	desireListId: number;
	desireAreaId: number;
	listState: ICommonListState;
}
export interface IDesireListsState
	extends IDiaryListWrapperCollectionState<IDesireListState> {}

type CommonListReducerType = typeof commonListReducer;
class DesireListsCollectionHandler extends ListCollectionHandler<
	IDesireListsState,
	IDesireListState,
	IDesireList,
	ICommonListState,
	ICommonList,
	IListItem,
	CommonListReducerType
> {
	listNamePrefix = DESIRE_LIST;

	createListReducer(listName: string) {
		return createCommonListReducer(listName);
	}

	listStateCreator(desireList: IDesireList) {
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

export const desireListsHandler = new DesireListsCollectionHandler();

const initialState: IDesireListsState = {
	byName: {},
};

export const desireListsReducer = (
	state = initialState,
	action: DesireListsActions
): IDesireListsState => {
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
