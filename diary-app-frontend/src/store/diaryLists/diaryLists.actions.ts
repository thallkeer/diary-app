import { ActionsUnion } from "../actions/action-helpers";
import { ListUrls } from "../../models/types";
import { createListActions, getListActions } from "./lists.actions";
import { IDiaryList, IListItem } from "models";

export const createDiaryListActions = <
	TList extends IDiaryList<TListItem>,
	TListItem extends IListItem
>() => createListActions<TList, TListItem>();

const actions = {
	...createDiaryListActions(),
};

export const getDiaryListActions = <
	TList extends IDiaryList<TListItem>,
	TListItem extends IListItem
>(
	listUrl: ListUrls
) => {
	return getListActions<TList, TListItem>(listUrl);
};

export type DiaryListActions = ActionsUnion<typeof actions>;
