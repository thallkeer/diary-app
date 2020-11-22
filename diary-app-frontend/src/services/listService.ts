import axios from "../axios/axios";
import { IList, IListItem } from "../models";

export function getListService<T extends IList<TItem>, TItem extends IListItem>(
	apiUrl: string,
	itemName: string
) {
	async function update(list: T) {
		await axios.put(apiUrl, list);
	}

	async function addItem(item: TItem): Promise<number> {
		console.log("add item ", item);

		return await axios
			.post(apiUrl + "add" + itemName, item)
			.then((res) => res.data);
	}

	async function updateItem(item: TItem) {
		await axios.put(apiUrl + "update" + itemName, item);
	}

	async function deleteItem(itemID: number) {
		await axios.delete(`${apiUrl}delete${itemName}/${itemID}`);
	}

	async function deleteList(listID: number) {
		await axios.delete(apiUrl + listID);
	}

	return {
		update,
		deleteList,
		addItem,
		updateItem,
		deleteItem,
	};
}
