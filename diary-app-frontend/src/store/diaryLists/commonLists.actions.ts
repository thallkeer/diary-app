import {
	createDiaryListActions,
	getDiaryListActions,
} from "./diaryLists.actions";
import { ActionsUnion } from "../actions/action-helpers";
import { getListItemActions } from "./lists.actions";
import { ICommonList, IListItem } from "models";

export const commonListActions = createDiaryListActions<
	ICommonList,
	IListItem
>();

export const commonListThunks = {
	...getDiaryListActions<ICommonList, IListItem>("commonLists"),
	...getListItemActions<IListItem>("listItems"),
};

export type CommonListActions = ActionsUnion<typeof commonListActions>;
