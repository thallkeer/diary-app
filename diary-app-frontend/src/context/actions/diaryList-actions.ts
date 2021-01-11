import { ActionsUnion } from "./action-helpers";
import { IListItem, IDiaryList } from "../../models/entities";
import { ListUrls } from "../../models/types";
import { createListActions } from "../reducers/list/listReducer";
import { getListActions } from "./listCrud-actions";

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

export type ListActions = ActionsUnion<typeof actions>;
