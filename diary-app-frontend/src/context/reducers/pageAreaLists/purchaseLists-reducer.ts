import {
	createTodoListReducer,
	TodoActions,
	todosReducer,
	todoListInitialState,
	todoActionCreators,
	todoActions,
} from "../list/todos";
import {
	IPurchaseListState,
	IPurchaseListsState,
	ListsStateByName,
} from "../../../models/states";
import { IPurchaseList } from "../../../models/entities";
import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { purchaseListsService } from "../../../services/concreteListService";
import { ListWrapperUrls } from "../../../models/types";
import { getPurchaseListByName } from "../../../selectors/page-selectors";

const SET_PURCHASE_LISTS = "SET_PURCHASE_LISTS";
const ADD_PURCHASE_LIST = "ADD_PURCHASE_LIST";
const DELETE_PURCHASE_LIST = "DELETE_PURCHASE_LIST";
const PURCHASE_LIST: ListWrapperUrls = "purchaseLists";
const PURCHASE_LIST_PATTERN = PURCHASE_LIST + "_";

export const getPurchaseListName = (listId: number) =>
	PURCHASE_LIST_PATTERN + listId;

const initialState: IPurchaseListsState = {
	byName: {},
};

type TodoReducerType = typeof todosReducer;
const reducers: Map<string, TodoReducerType> = new Map<
	string,
	TodoReducerType
>();

export const purchaseListsReducer = (
	state = initialState,
	action: PurchaseListsActions
): IPurchaseListsState => {
	switch (action.type) {
		case "SET_PURCHASE_LISTS":
			reducers.clear();
			const newState: ListsStateByName<IPurchaseListState> = {};
			action.payload.forEach((pl) => {
				newState[getPurchaseListName(pl.id)] = createPurchaseListState(pl);
			});
			return {
				byName: newState,
			};

		case "ADD_PURCHASE_LIST":
			const newList = action.payload;
			const addedState = createPurchaseListState(newList);
			let plName = getPurchaseListName(newList.id);
			return {
				...state,
				byName: {
					...state.byName,
					[plName]: addedState,
				},
			};

		case "DELETE_PURCHASE_LIST":
			let plistName = getPurchaseListName(action.payload);
			const stateAfterDelete = { ...state };
			delete stateAfterDelete.byName[plistName];
			return stateAfterDelete;

		default:
			return oneListReducer(state, action);
	}
};

const oneListReducer = (
	state: IPurchaseListsState,
	action: TodoActions
): IPurchaseListsState => {
	let listKey = action.subjectName;
	const reducer = reducers.get(listKey);
	if (!reducer) return state;
	const purchaseList = getPurchaseListByName(listKey)(state);
	const newTodoListState = reducer(purchaseList.listState, action);
	return {
		...state,
		byName: {
			...state.byName,
			[listKey]: {
				...purchaseList,
				listState: newTodoListState,
			},
		},
	};
};

const actions = {
	setLists: (purchaseLists: IPurchaseList[]) =>
		createAction(SET_PURCHASE_LISTS, purchaseLists),
	addPurchaseList: (purchaseList: IPurchaseList) =>
		createAction(ADD_PURCHASE_LIST, purchaseList),
	deletePurchaseList: (purchaseListId: number) =>
		createAction(DELETE_PURCHASE_LIST, purchaseListId),
	...todoActions,
};

export const purchaseListsActions = {
	...todoActionCreators,
	setPurchaseLists: (purchaseLists: IPurchaseList[]): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setLists(purchaseLists));
	},
	addPurchaseList: (purchaseList: IPurchaseList): ThunkType => async (
		dispatch
	) => {
		let id = await purchaseListsService.createList(purchaseList);
		dispatch(
			actions.addPurchaseList({
				id: id,
				...purchaseList,
			})
		);
	},
	deletePurchaseList: (purchaseListId: number): ThunkType => async (
		dispatch
	) => {
		await purchaseListsService.deleteList(purchaseListId);
		dispatch(actions.deletePurchaseList(purchaseListId));
	},
};

export type PurchaseListsActions = ActionsUnion<typeof actions> | TodoActions;
type ThunkType = BaseThunkType<PurchaseListsActions>;

const createPurchaseListState = (pl: IPurchaseList) => {
	const listName = getPurchaseListName(pl.id);
	const todoReducer = createTodoListReducer(listName);
	reducers.set(listName, todoReducer);
	const plistState: IPurchaseListState = {
		purchaseListId: pl.id,
		purchaseAreaId: pl.areaOwnerId,
		listState: {
			...todoListInitialState,
			list: pl.list,
			listName: "todoList_" + pl.list.id,
		},
	};
	return plistState;
};
