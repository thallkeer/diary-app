import { ITodo, IEvent, IListItem, IList } from "../models/index";

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

export const getEmptyItem = (ownerId: number) => {
	const item: IListItem = { id: 0, subject: "", url: "", ownerID: ownerId};
	return item;
};

export const getEmptyTodo = (ownerId: number) => {
	const todo: ITodo = { ...getEmptyItem(ownerId),done: false };
	console.log(todo);
	
	return todo;
};

export const getEmptyEvent = (ownerId: number) => {
	const event: IEvent = { ...getEmptyItem(ownerId), date: new Date() };
	return event;
};

export function updateListItems<
	T extends IList<TItem>,
	TItem extends IListItem
>(list: T, items: TItem[], updateFunction: (listItem: TItem) => TItem[]): T {
	return {
		...list,
		items: items.map((item) => updateFunction(item)),
	};
}

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
