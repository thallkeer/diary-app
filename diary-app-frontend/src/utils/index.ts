import { INamedAction } from "../context/actions/action-helpers";
import {
	ITodo,
	IEvent,
	IListItem,
	IDiaryList,
	IListWithItems,
	IEntity,
} from "../models/entities";
import { IDiaryListState, IListState } from "../models/states";

var _getRandomInt = (min: number, max: number) => {
	return Math.floor(Math.random() * (max - min + 1)) + min;
};

export const getRandomId = () => {
	var ts = new Date().getTime().toString();
	var parts = ts.split("").reverse();
	var id = "";

	for (var i = 0; i < 8; ++i) {
		var index = _getRandomInt(0, parts.length - 1);
		id += parts[index];
	}
	return Number(id);
};

/**
 * returns empty list item
 *
 * @param ownerId id of list that empty item will belongs
 */
export const getEmptyItem = (ownerId: number) => {
	const item: IListItem = { id: 0, subject: "", url: "", ownerID: ownerId };
	return item;
};

export const getEmptyTodo = (ownerId: number) => {
	const todo: ITodo = { ...getEmptyItem(ownerId), done: false };
	return todo;
};

export const getEmptyEvent = (ownerId: number) => {
	const event: IEvent = { ...getEmptyItem(ownerId), date: new Date() };
	return event;
};

/**
 *
 * @param list
 * @param fillTo number of items list should contains (if it's not contains required number of items, they will be added as empty items)
 * @param getEmptyItem
 */
export function fillToNumber<T extends IListItem>(
	list: T[],
	fillTo: number,
	getEmptyItem: () => T
): T[] {
	let length = list.length;
	fillTo = length >= fillTo ? length + 1 : fillTo;
	for (let i = length; i < fillTo; i++) {
		let emptyItem = getEmptyItem();
		emptyItem.readonly = true;
		list.push(emptyItem);
	}

	list[length].readonly = false;
	return list;
}

export const updateListInState = <
	TState extends IListState<TList, TListItem>,
	TList extends IListWithItems<TListItem>,
	TListItem extends IEntity
>(
	state: TState,
	updateItemsFunc: (listItems: TListItem[]) => TListItem[]
) => {
	return {
		...state,
		list: {
			...state.list,
			items: updateItemsFunc(getListItems(state)),
		},
	};
};

const getListItems = <
	TState extends IListState<TList, TListItem>,
	TList extends IListWithItems<TListItem>,
	TListItem
>(
	state: TState
) => state.list?.items ?? [];

export function createNamedWrapperReducer<TState, TAction extends INamedAction>(
	reducer: (state: TState, action: TAction) => TState,
	initialState: TState,
	reducerName: string
) {
	return (state = initialState, action: TAction) => {
		const isInitializationCall = state === undefined;

		if (reducerName !== action.subjectName && !isInitializationCall) {
			return state;
		}

		return reducer(state, action);
	};
}
