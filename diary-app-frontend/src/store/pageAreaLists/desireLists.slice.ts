import {
	AnyAction,
	createEntityAdapter,
	createSlice,
	PayloadAction,
} from "@reduxjs/toolkit";
import { ICommonList, IListItem } from "models";
import { listItemService } from "services/todosService";
import {
	createCommonListSlice,
	ICommonListState,
	CommonListSlice,
} from "store/diaryLists/commonLists.reducer";
import { RootState } from "store/store";

const desiresListsAdapter = createEntityAdapter<ICommonListState>({
	selectId: (listState) => listState.list.id,
});

export const desireListsSlices = new Map<number, CommonListSlice>();
const getListActions = (listId: number) =>
	desireListsSlices.get(listId).actions;
const getListReducer = (listId: number) =>
	desireListsSlices.get(listId).reducer;

const desiresListsSlice = createSlice({
	name: "desiresLists",
	initialState: desiresListsAdapter.getInitialState(),
	reducers: {
		setLists(state, action: PayloadAction<ICommonList[]>) {
			const newState = action.payload.map((list) => {
				const listState: ICommonListState = {
					isDeletable: false,
					readonlyHeader: true,
					list: list,
				};
				desireListsSlices.set(
					list.id,
					createCommonListSlice("desiresLists-" + list.id)
				);
				return listState;
			});

			desiresListsAdapter.setAll(state, newState);
		},
		listChanged(state, action: PayloadAction<ICommonListState>) {
			const { list } = action.payload;
			desiresListsAdapter.updateOne(state, {
				id: list.id,
				changes: action.payload,
			});
		},
	},
});

const { reducer, actions } = desiresListsSlice;
export const desiresListsReducer = reducer;
export const desiresListsActions = actions;

export const setDesiresLists = (lists: ICommonList[]) => (dispatch) => {
	dispatch(desiresListsActions.setLists(lists));
};

const desireListsSelectors = desiresListsAdapter.getSelectors<RootState>(
	(state) => state.monthPage.desiresArea.desireLists
);

export const addOrUpdateItem =
	(listId: number, item: IListItem) => async (dispatch, getState) => {
		if (!item) return;

		const actions = getListActions(listId);
		let newState: ICommonListState = null;

		if (item.id === 0) {
			const createdItemId = await listItemService.create(item);
			newState = reduceListAction(getState(), listId, () =>
				actions.addItem({ ...item, id: createdItemId })
			);
		} else {
			await listItemService.update(item);
			newState = reduceListAction(getState(), listId, () =>
				actions.updateItem(item)
			);
		}

		dispatch(desiresListsActions.listChanged(newState));
	};

export const deleteItem =
	(listId: number, itemId: number) => async (dispatch, getState) => {
		await listItemService.deleteById(itemId);
		const newState = reduceListAction(getState(), listId, () =>
			getListActions(listId).deleteItem(itemId)
		);
		dispatch(desiresListsActions.listChanged(newState));
	};

const reduceListAction = (
	globalState: RootState,
	listId: number,
	action: () => AnyAction
) => {
	const listState = desireListsSelectors.selectById(globalState, listId);
	const reducer = getListReducer(listId);
	return reducer(listState, action());
};
