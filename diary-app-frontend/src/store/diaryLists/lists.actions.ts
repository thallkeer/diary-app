import { IListWithItems } from "models";
import { BaseThunkType } from "store/state.types";
import { IEntity } from "../../models/entities";
import { ListItemUrls, ListUrls } from "../../models/types";
import { CrudService } from "../../services/crudService";
import { ActionsUnion, createNamedAction } from "../actions/action-helpers";

const SET_LIST = "SET_LIST";
const DELETE_LIST = "DELETE_LIST";
const UPDATE_LIST = "UPDATE_LIST";
const ADD_LIST_ITEM = "ADD_LIST_ITEM";
const UPDATE_LIST_ITEM = "UPDATE_LIST_ITEM";
const DELETE_LIST_ITEM = "DELETE_LIST_ITEM";

export const createListActions = <
	TList extends IListWithItems<TListItem>,
	TListItem extends IEntity
>() => {
	const actions = {
		setList: (list: TList, listName: string) =>
			createNamedAction(SET_LIST, listName, list),
		updateListItem: (listItem: TListItem, listName: string) =>
			createNamedAction(UPDATE_LIST_ITEM, listName, listItem),
		addListItem: (newListItem: TListItem, listName: string) =>
			createNamedAction(ADD_LIST_ITEM, listName, newListItem),
		deleteListItem: (listItemID: number, listName: string) =>
			createNamedAction(DELETE_LIST_ITEM, listName, listItemID),
		deleteList: (listID: number, listName: string) =>
			createNamedAction(DELETE_LIST, listName, listID),
		updateList: (list: TList, listName: string) =>
			createNamedAction(UPDATE_LIST, listName, list),
	};
	return actions;
};

const actions = {
	...createListActions(),
};

export const getListItemActions = <TEntity extends IEntity>(
	listItemName: ListItemUrls
) => {
	const crudService = new CrudService<TEntity>(listItemName);

	const addOrUpdateListItem = (
		listItem: TEntity,
		listName: string
	): ThunkType => async (dispatch) => {
		if (!listItem) return;

		if (listItem.id === 0) {
			await crudService
				.create(listItem)
				.then((listItemID) =>
					dispatch(
						actions.addListItem({ ...listItem, id: listItemID }, listName)
					)
				);
		} else {
			await crudService
				.update(listItem)
				.then((_) => dispatch(actions.updateListItem(listItem, listName)));
		}
	};

	const deleteListItem = (
		listItemID: number,
		listName: string
	): ThunkType => async (dispatch) => {
		if (listItemID === 0) return;
		await crudService.delete(listItemID);
		dispatch(actions.deleteListItem(listItemID, listName));
	};

	return {
		addOrUpdateListItem,
		deleteListItem,
	};
};

export const getListActions = <
	TList extends IListWithItems<TListItem>,
	TListItem extends IEntity
>(
	listUrl: ListUrls
) => {
	const crudService = new CrudService<TList>(listUrl);

	const setList = (list: TList, listName: string): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setList(list, listName));
	};

	const updateListAction = (list: TList, listName: string): ThunkType => async (
		dispatch
	) => {
		await crudService.update(list);
		dispatch(actions.updateList(list, listName));
	};

	const deleteListAction = (
		listID: number,
		listName: string
	): ThunkType => async (dispatch) => {
		await crudService.delete(listID);
		dispatch(actions.deleteList(listID, listName));
	};

	return {
		setList,
		updateList: updateListAction,
		removeList: deleteListAction,
	};
};

export type ListActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<ListActions>;
