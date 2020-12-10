import axios from "../axios/axios";
import { IList, IListItem } from "../models";

export const getListService = <T extends IList<TItem>, TItem extends IListItem>(
	apiUrl: string
) => {
	const createList = (list: T) => {
		return axios.post<number>(apiUrl, list).then((res) => res.data);
	};

	const updateList = (list: T) => {
		return axios.put(apiUrl, list).then((res) => res.data);
	};

	const addItem = (item: TItem) => {
		return axios.post<number>(`${apiUrl}items`, item).then((res) => res.data);
	};

	const updateItem = (item: TItem) => {
		return axios.put(`${apiUrl}items`, item).then((res) => res.data);
	};

	const deleteItem = (itemID: number) => {
		return axios.delete(`${apiUrl}items/${itemID}`).then((res) => res.data);
	};

	const deleteList = (listID: number) => {
		return axios.delete(apiUrl + listID).then((res) => res.data);
	};

	return {
		createList,
		updateList,
		deleteList,
		addItem,
		updateItem,
		deleteItem,
	};
};
