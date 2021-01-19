import { ICommonList, IListItem } from "models";
import { IDiaryListState } from "models/states";
import { createNamedReducer } from "utils";
import { CommonListActions } from "./commonLists.actions";
import { diaryListReducer } from "./diaryLists.reducer";

export interface ICommonListState
	extends IDiaryListState<ICommonList, IListItem> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

export const commonListInitialState: ICommonListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: true,
	listName: "commonList",
};

export const createCommonListReducer = (reducerName: string) => {
	return createNamedReducer(
		commonListReducer,
		commonListInitialState,
		reducerName
	);
};

export const commonListReducer = (
	state = commonListInitialState,
	action: CommonListActions
): ICommonListState =>
	diaryListReducer<ICommonListState, ICommonList, IListItem>(state, action);
