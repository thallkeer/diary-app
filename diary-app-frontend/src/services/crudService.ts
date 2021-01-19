import { AxiosInstance } from "axios";
import axios from "../axios/axios";

export class CrudService<T> {
	protected apiUrl: string;
	protected axios: AxiosInstance;
	constructor(apiUrl: string) {
		this.apiUrl = apiUrl;
		this.axios = axios;
	}

	getById(id: number) {
		return this.axios.get<T>(`${this.apiUrl}/${id}`).then((res) => res.data);
	}

	create(entity: T) {
		return this.axios.post<number>(this.apiUrl, entity).then((res) => res.data);
	}

	update(entity: T) {
		return this.axios.put(this.apiUrl, entity).then((res) => res.data);
	}

	delete(entityId: number) {
		return this.axios
			.delete(`${this.apiUrl}/${entityId}`)
			.then((res) => res.data);
	}
}
