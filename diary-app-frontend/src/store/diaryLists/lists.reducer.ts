import { IList } from "models";
import { IListState } from "models/states";
import { BaseThunkType } from "store/state.types";
import { IEntity } from "../../models/entities";
import { ListItemUrls, ListUrls } from "../../models/types";
import { CrudService } from "../../services/crudService";
import { ActionsUnion, createNamedAction } from "../actions/action-helpers";
import { createNamedReducer, updateListInState } from "utils";

const SET_LIST = "SET_LIST";
const DELETE_LIST = "DELETE_LIST";
const UPDATE_LIST = "UPDATE_LIST";
const ADD_LIST_ITEM = "ADD_LIST_ITEM";
const UPDATE_LIST_ITEM = "UPDATE_LIST_ITEM";
const DELETE_LIST_ITEM = "DELETE_LIST_ITEM";

export class DiaryListComponent<
	TList extends IList<TListItem>,
	TListItem extends IEntity
> {
	protected listUrl: ListUrls;
	protected listItemUrl: ListItemUrls;
	protected listService: CrudService<TList>;
	protected listItemService: CrudService<TListItem>;

	constructor(listUrl: ListUrls, listItemUrl: ListItemUrls) {
		this.listUrl = listUrl;
		this.listItemUrl = listItemUrl;
		this.listService = new CrudService<TList>(listUrl);
		this.listItemService = new CrudService<TListItem>(listItemUrl);
	}

	public getListItemUrl() {
		return this.listItemUrl;
	}

	public getActions(listName: string) {
		return {
			setList: (list: TList) => createNamedAction(SET_LIST, listName, list),
			updateListItem: (listItem: TListItem) =>
				createNamedAction(UPDATE_LIST_ITEM, listName, listItem),
			addListItem: (newListItem: TListItem) =>
				createNamedAction(ADD_LIST_ITEM, listName, newListItem),
			deleteListItem: (listItemId: number) =>
				createNamedAction(DELETE_LIST_ITEM, listName, listItemId),
			deleteList: (listId: number) =>
				createNamedAction(DELETE_LIST, listName, listId),
			updateList: (list: TList) =>
				createNamedAction(UPDATE_LIST, listName, list),
		};
	}

	public getThunks(listName: string) {
		const actions = this.getActions(listName);
		type ListActions = ActionsUnion<typeof actions>;
		type ListThunkType = BaseThunkType<ListActions>;

		const thunks = {
			setList: (list: TList): ListThunkType => async (dispatch) => {
				dispatch(actions.setList(list));
			},

			updateList: (list: TList): ListThunkType => async (dispatch) => {
				await this.listService.update(list);
				dispatch(actions.updateList(list));
			},

			deleteList: (listId: number): ListThunkType => async (dispatch) => {
				await this.listService.delete(listId);
				dispatch(actions.deleteList(listId));
			},

			addOrUpdateListItem: (listItem: TListItem): ListThunkType => async (
				dispatch
			) => {
				if (!listItem) return;

				if (listItem.id === 0) {
					await this.listItemService
						.create(listItem)
						.then((listItemId) =>
							dispatch(actions.addListItem({ ...listItem, id: listItemId }))
						);
				} else {
					await this.listItemService
						.update(listItem)
						.then((_) => dispatch(actions.updateListItem(listItem)));
				}
			},

			deleteListItem: (listItemId: number): ListThunkType => async (
				dispatch
			) => {
				if (listItemId === 0) return;
				await this.listItemService.delete(listItemId);
				dispatch(actions.deleteListItem(listItemId));
			},
		};

		return thunks;
	}

	public getReducer<TState extends IListState<TList, TListItem>>(
		initialState: TState,
		listName: string
	) {
		const actions = this.getActions(listName);
		type ListActions = ActionsUnion<typeof actions>;

		const listReducer = (state = initialState, action: ListActions): TState => {
			switch (action.type) {
				case "SET_LIST": {
					return {
						...state,
						list: action.payload,
					};
				}
				case "ADD_LIST_ITEM":
					return updateListInState(state, (listItems) => [
						...listItems,
						action.payload,
					]);

				case "UPDATE_LIST_ITEM":
					return updateListInState(state, (listItems) =>
						listItems.map((item) =>
							item.id === action.payload.id ? action.payload : item
						)
					);

				case "DELETE_LIST_ITEM":
					return updateListInState(state, (listItems) =>
						listItems.filter((event) => event.id !== action.payload)
					);

				case "UPDATE_LIST":
					return {
						...state,
						list: action.payload,
					};

				case "DELETE_LIST":
					return {
						...state,
						list: null,
					};

				default:
					return state;
			}
		};

		return createNamedReducer(listReducer, initialState, listName);
	}
}
