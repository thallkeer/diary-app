import {
	createDiaryListActions,
	getDiaryListActions,
} from "../../actions/diaryList-actions";
import { IListItem, ICommonList } from "../../../models/entities";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { ActionsUnion } from "../../actions/action-helpers";
import { createNamedWrapperReducer } from "../../../utils";
import { IDiaryListState } from "../../../models/states";
import { diaryListReducer } from "./diaryListReducer";
import { getListItemActions } from "../../actions/listCrud-actions";

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
	return createNamedWrapperReducer(
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

export const commonListActions = createDiaryListActions<
	ICommonList,
	IListItem
>();

export const commonListActionCreators = {
	...getDiaryListActions<ICommonList, IListItem>("commonLists"),
	...getListItemActions<IListItem>("listItems"),
};

export type CommonListActions = ActionsUnion<typeof commonListActions>;
