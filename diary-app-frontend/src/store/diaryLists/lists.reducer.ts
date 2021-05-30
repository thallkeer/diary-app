import { IListState } from "models/states";
import { IEntity } from "../../models/entities";
import { CrudService } from "../../services/crudService";
import { IList } from "models";
import { AppThunk } from "store/store";
import {
	createSlice,
	PayloadAction,
	SliceCaseReducers,
	ValidateSliceCaseReducers,
} from "@reduxjs/toolkit";

export const generateListSliceReducers = <
	TState extends IListState<TList, TItem>,
	TList extends IList<TItem>,
	TItem extends IEntity
>() => {
	const reducers = {
		setList(state: TState, action: PayloadAction<TList>) {
			state.list = action.payload;
		},
		updateList(state: TState, action: PayloadAction<TList>) {
			state.list = action.payload;
		},
		addItem(state: TState, action: PayloadAction<TItem>) {
			state.list.items.push(action.payload);
		},
		updateItem(state: TState, action: PayloadAction<TItem>) {
			state.list.items = state.list.items.map((item) =>
				item.id === action.payload.id ? action.payload : item
			);
		},
		deleteItem(state: TState, action: PayloadAction<number>) {
			state.list.items = state.list.items.filter(
				(item) => item.id !== action.payload
			);
		},
	};
	return reducers as SliceCaseReducers<TState>;
};

export const createGenericSlice = <
	TState extends IListState<TList, TItem>,
	TList extends IList<TItem>,
	TItem extends IEntity,
	Reducers extends SliceCaseReducers<TState>
>({
	name = "",
	initialState,
	reducers,
}: {
	name: string;
	initialState: TState;
	reducers: ValidateSliceCaseReducers<TState, Reducers>;
}) => {
	const extension = {
		setList(state: TState, action: PayloadAction<TList>) {
			state.list = action.payload;
		},
		updateList(state: TState, action: PayloadAction<TList>) {
			state.list = action.payload;
		},
		addItem(state: TState, action: PayloadAction<TItem>) {
			state.list.items.push(action.payload);
		},
		updateItem(state: TState, action: PayloadAction<TItem>) {
			state.list.items = state.list.items.map((item) =>
				item.id === action.payload.id ? action.payload : item
			);
		},
		deleteItem(state: TState, action: PayloadAction<number>) {
			state.list.items = state.list.items.filter(
				(item) => item.id !== action.payload
			);
		},
	};

	return createSlice({
		name,
		initialState,
		reducers: {
			...extension,
			...reducers,
		},
	});
};

export const createGenericListThunks = <
	TState extends IListState<TList, TListItem>,
	TList extends IList<TListItem>,
	TListItem extends IEntity
>(
	listName: string,
	listService: CrudService<TList>,
	listItemService: CrudService<TListItem>
) => {
	const slice = createGenericSlice<TState, TList, TListItem, {}>({
		name: listName,
		initialState: null,
		reducers: {},
	});

	const actions = slice.actions;
	const thunks = {
		setList:
			(list: TList): AppThunk =>
			async (dispatch) => {
				dispatch(actions.setList<TList>(list));
			},
		updateList:
			(list: TList): AppThunk =>
			async (dispatch) => {
				await listService.update(list);
				dispatch(actions.updateList<TList>(list));
			},

		addOrUpdateItem:
			(listItem: TListItem): AppThunk =>
			async (dispatch) => {
				if (!listItem) return;

				if (listItem.id === 0) {
					await listItemService
						.create(listItem)
						.then((listItemId) =>
							dispatch(actions.addItem({ ...listItem, id: listItemId }))
						);
				} else {
					await listItemService
						.update(listItem)
						.then((_) => dispatch(actions.updateItem(listItem)));
				}
			},

		deleteItemById:
			(listItemId: number): AppThunk =>
			async (dispatch) => {
				if (listItemId === 0) return;
				await listItemService.deleteById(listItemId);
				dispatch(actions.deleteItem(listItemId));
			},
	};

	return thunks;
};
