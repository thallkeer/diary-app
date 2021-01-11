import { IDiaryList, IListItem } from "../../../models/entities";
import { IDiaryListState } from "../../../models/states";
import { ListActions } from "../../actions/diaryList-actions";
import { listReducer } from "./listReducer";

export const diaryListReducer = <
	TState extends IDiaryListState<TList, TListItem>,
	TList extends IDiaryList<TListItem>,
	TListItem extends IListItem
>(
	state: TState,
	action: ListActions
): TState => {
	return listReducer<TState, TList, TListItem>(state, action);
};
