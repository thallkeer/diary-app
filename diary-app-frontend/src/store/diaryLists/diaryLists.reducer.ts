import { IDiaryList, IListItem } from "models";
import { IDiaryListState } from "../../models/states";
import { DiaryListActions } from "./diaryLists.actions";
import { listReducer } from "./lists.reducer";

export const diaryListReducer = <
	TState extends IDiaryListState<TList, TListItem>,
	TList extends IDiaryList<TListItem>,
	TListItem extends IListItem
>(
	state: TState,
	action: DiaryListActions
): TState => {
	return listReducer<TState, TList, TListItem>(state, action);
};
