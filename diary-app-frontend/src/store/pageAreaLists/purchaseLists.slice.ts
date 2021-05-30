import {
	AnyAction,
	createEntityAdapter,
	createSlice,
	PayloadAction,
} from "@reduxjs/toolkit";
import { ITodoList, ITodo } from "models";
import { purchaseListsService } from "services/concreteListService";
import { todosService } from "services/todosService";
import { createTodoSlice, ITodoListState, TodoSlice } from "store/diaryLists";
import { AppThunk, RootState } from "store/store";

const purchaseListsAdapter = createEntityAdapter<ITodoListState>({
	selectId: (listState) => listState.list.id,
});

export const purchaseListsSlices = new Map<number, TodoSlice>();
const getListActions = (listId: number) =>
	purchaseListsSlices.get(listId).actions;
const getListReducer = (listId: number) =>
	purchaseListsSlices.get(listId).reducer;

const purchaseListsSlice = createSlice({
	name: "purchaseLists",
	initialState: purchaseListsAdapter.getInitialState(),
	reducers: {
		setLists(state, action: PayloadAction<ITodoList[]>) {
			const newState = action.payload.map((list) => {
				const listState: ITodoListState = {
					list: list,
				};
				purchaseListsSlices.set(
					list.id,
					createTodoSlice("purchaseList-" + list.id)
				);
				return listState;
			});

			purchaseListsAdapter.setAll(state, newState);
		},
		listAdded(state, action: PayloadAction<ITodoListState>) {
			purchaseListsAdapter.addOne(state, action.payload);
		},
		listChanged(state, action: PayloadAction<ITodoListState>) {
			const { list } = action.payload;
			purchaseListsAdapter.updateOne(state, {
				id: list.id,
				changes: action.payload,
			});
		},
		listDeleted(state, action: PayloadAction<number>) {
			purchaseListsAdapter.removeOne(state, action.payload);
		},
	},
});

const { reducer, actions } = purchaseListsSlice;
export const purchaseListsReducer = reducer;
export const purchaseListsActions = actions;

const purchaseListsSelectors = purchaseListsAdapter.getSelectors<RootState>(
	(state) => state.monthPage.purchasesArea.purchaseLists
);

export const setPurchaseLists =
	(lists: ITodoList[]): AppThunk =>
	(dispatch) => {
		dispatch(purchaseListsActions.setLists(lists));
	};

export const addPurchaseList =
	(purchaseList: ITodoList, purchaseAreaId: number): AppThunk =>
	async (dispatch) => {
		const listId = await purchaseListsService.create(
			purchaseList,
			purchaseAreaId
		);
		const todoListId = await purchaseListsService.getTodoListId(listId);
		dispatch(
			purchaseListsActions.listAdded({
				list: {
					...purchaseList,
					id: todoListId,
				},
			})
		);
	};

export const updatePurchaseList =
	(purchaseList: ITodoList): AppThunk =>
	async (dispatch, getState) => {
		await todosService.list.update(purchaseList);
		const listState = purchaseListsSelectors.selectById(
			getState(),
			purchaseList.id
		);
		dispatch(
			purchaseListsActions.listChanged({
				...listState,
				list: purchaseList,
			})
		);
	};

export const deletePurchaseList =
	(purchaseListId: number): AppThunk =>
	async (dispatch) => {
		await todosService.list.deleteById(purchaseListId);
		dispatch(purchaseListsActions.listDeleted(purchaseListId));
	};

export const addOrUpdatePurchaseItem =
	(listId: number, item: ITodo): AppThunk =>
	async (dispatch, getState) => {
		if (!item) return;

		const actions = getListActions(listId);
		let newState: ITodoListState = null;

		if (item.id === 0) {
			const createdItemId = await todosService.items.create(item);
			newState = reduceListAction(getState(), listId, () =>
				actions.addItem({ ...item, id: createdItemId })
			);
		} else {
			await todosService.items.update(item);
			newState = reduceListAction(getState(), listId, () =>
				actions.updateItem(item)
			);
		}

		dispatch(purchaseListsActions.listChanged(newState));
	};

export const togglePurchaseItem =
	(listId: number, itemId: number): AppThunk =>
	async (dispatch, getState) => {
		await todosService.items.toggleTodo(itemId);
		const newState = reduceListAction(getState(), listId, () =>
			getListActions(listId).toggleTodo(itemId)
		);
		dispatch(purchaseListsActions.listChanged(newState));
	};

export const deletePurchaseItem =
	(listId: number, itemId: number): AppThunk =>
	async (dispatch, getState) => {
		await todosService.items.deleteById(itemId);
		const newState = reduceListAction(getState(), listId, () =>
			getListActions(listId).deleteItem(itemId)
		);
		dispatch(purchaseListsActions.listChanged(newState));
	};

const reduceListAction = (
	globalState: RootState,
	listId: number,
	action: () => AnyAction
) => {
	const listState = purchaseListsSelectors.selectById(globalState, listId);
	const reducer = getListReducer(listId);
	return reducer(listState, action());
};
