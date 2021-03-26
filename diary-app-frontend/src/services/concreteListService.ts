import { AxiosInstance } from "axios";
import { IHabitTracker, ITodoList } from "models";
import { ListUrls, ListWrapperUrls } from "models/types";
import { CrudService } from "./crudService";
import axios from "../axios/axios";

const habitTrackerApi: ListUrls = "habitTrackers";
export const habitTrackerService = new CrudService<IHabitTracker>(
	habitTrackerApi
);

const purchaseListsApi: ListWrapperUrls = "purchaseLists";

export class PurchaseListService {
	protected axios: AxiosInstance;
	constructor() {
		this.axios = axios;
	}

	create(todoList: ITodoList, purchasesAreaId: number): Promise<number> {
		return this.axios
			.post<number>(purchaseListsApi, { todoList, purchasesAreaId })
			.then((res) => res.data);
	}

	getTodoListId(purchaseListId: number): Promise<number> {
		return this.axios
			.get<number>(`${purchaseListsApi}/${purchaseListId}`)
			.then((res) => res.data);
	}
}

export const purchaseListsService = new PurchaseListService();
