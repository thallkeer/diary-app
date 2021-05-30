import {
	AnyAction,
	createEntityAdapter,
	createSlice,
	PayloadAction,
} from "@reduxjs/toolkit";
import {
	createHabitTrackerSlice,
	HabitTrackerSlice,
	IHabitTrackerState,
} from "store/diaryLists/habitTrackers.reducer";
import { IHabitTracker, IHabitDay } from "models";
import { todosService } from "services/todosService";
import { AppThunk, RootState } from "store/store";
import {
	habitDayService,
	habitTrackerService,
} from "services/concreteListService";

const goalListsAdapter = createEntityAdapter<IHabitTrackerState>({
	selectId: (listState) => listState.list.id,
});

export const goalListsSlices = new Map<number, HabitTrackerSlice>();
const getListActions = (listId: number) => goalListsSlices.get(listId).actions;
const getListReducer = (listId: number) => goalListsSlices.get(listId).reducer;

const goalListsSlice = createSlice({
	name: "goalLists",
	initialState: goalListsAdapter.getInitialState(),
	reducers: {
		setLists(state, action: PayloadAction<IHabitTracker[]>) {
			const newState = action.payload.map((list) => {
				const listState: IHabitTrackerState = {
					list: list,
				};
				goalListsSlices.set(
					list.id,
					createHabitTrackerSlice("goalList-" + list.id)
				);
				return listState;
			});

			goalListsAdapter.setAll(state, newState);
		},
		listAdded(state, action: PayloadAction<IHabitTrackerState>) {
			goalListsAdapter.addOne(state, action.payload);
		},
		listChanged(state, action: PayloadAction<IHabitTrackerState>) {
			const { list } = action.payload;
			goalListsAdapter.updateOne(state, {
				id: list.id,
				changes: action.payload,
			});
		},
		listDeleted(state, action: PayloadAction<number>) {
			goalListsAdapter.removeOne(state, action.payload);
		},
	},
});

const { reducer, actions } = goalListsSlice;
export const goalListsReducer = reducer;
export const goalListsActions = actions;

const goalListsSelectors = goalListsAdapter.getSelectors<RootState>(
	(state) => state.monthPage.goalsArea.goalsLists
);

export const setGoalLists =
	(lists: IHabitTracker[]): AppThunk =>
	(dispatch) => {
		dispatch(goalListsActions.setLists(lists));
	};

export const addGoalList =
	(goalList: IHabitTracker): AppThunk =>
	async (dispatch) => {
		if (!goalList) return;

		const id = await habitTrackerService.create(goalList);
		const newGoalList: IHabitTracker = {
			...goalList,
			id,
		};
		dispatch(actions.listAdded({ list: newGoalList }));
	};

export const updateGoalList =
	(goalList: IHabitTracker): AppThunk =>
	async (dispatch, getState) => {
		await habitTrackerService.update(goalList);
		const listState = goalListsSelectors.selectById(getState(), goalList.id);
		dispatch(
			goalListsActions.listChanged({
				...listState,
				list: goalList,
			})
		);
	};

export const deleteGoalList =
	(goalListId: number): AppThunk =>
	async (dispatch) => {
		await habitTrackerService.deleteById(goalListId);
		dispatch(goalListsActions.listDeleted(goalListId));
	};

export const addOrUpdateHabitDay =
	(listId: number, item: IHabitDay): AppThunk =>
	async (dispatch, getState) => {
		if (!item) return;

		const actions = getListActions(listId);
		let newState: IHabitTrackerState = null;

		if (item.id === 0) {
			const createdItemId = await habitDayService.create(item);
			newState = reduceListAction(getState(), listId, () =>
				actions.addItem({ ...item, id: createdItemId })
			);
		} else {
			await habitDayService.update(item);
			newState = reduceListAction(getState(), listId, () =>
				actions.updateItem(item)
			);
		}

		dispatch(goalListsActions.listChanged(newState));
	};

export const deleteHabitDay =
	(listId: number, itemId: number): AppThunk =>
	async (dispatch, getState) => {
		await habitDayService.deleteById(itemId);
		const newState = reduceListAction(getState(), listId, () =>
			getListActions(listId).deleteItem(itemId)
		);
		dispatch(goalListsActions.listChanged(newState));
	};

const reduceListAction = (
	globalState: RootState,
	listId: number,
	action: () => AnyAction
) => {
	const listState = goalListsSelectors.selectById(globalState, listId);
	const reducer = getListReducer(listId);
	return reducer(listState, action());
};
