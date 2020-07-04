import axios from "../axios/axios";
import { List, ListItem } from "../models";

export function getListService<T extends List, TItem extends ListItem>(
	apiUrl: string,
	itemName: string
) {
	async function getByPageID(pageID: number): Promise<T> {
		return await axios.get(apiUrl + pageID).then((response) => response.data);
	}

	async function update(list: T) {
		await axios.put(apiUrl, list);
	}

	async function addItem(item: TItem): Promise<number> {
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
		getByPageID,
		update,
		remove: deleteList,
		addItem,
		updateItem,
		deleteItem,
	};
}
