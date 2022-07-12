import { IEvent, IListItem, ITodo } from "models";

/**
 * returns empty list item
 *
 * @param ownerId id of list that empty item will belongs
 */
export const getEmptyItem = (ownerId: number) => {
	const item: IListItem = { id: 0, subject: "", url: "", ownerId: ownerId };
	return item;
};

export const getEmptyTodo = (ownerId: number) => {
	const todo: ITodo = { ...getEmptyItem(ownerId), done: false };
	return todo;
};

export const getEmptyEvent = (ownerId: number) => {
	const event: IEvent = {
		...getEmptyItem(ownerId),
		date: new Date(),
		location: "",
	};
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
	let filledList = [...list];
	const length = filledList.length;
	fillTo = length >= fillTo ? length + 1 : fillTo;
	for (let i = length; i < fillTo; i++) {
		const emptyItem = getEmptyItem();
		emptyItem.readonly = true;
		filledList.push(emptyItem);
	}

	filledList[length].readonly = false;

	return filledList;
}
