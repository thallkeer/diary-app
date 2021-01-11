import axios from "../axios/axios";
import { IDiaryListWrapper, IPurchaseList } from "../models/entities";
import { ListWrapperUrls } from "../models/types";
import { CrudService } from "./crudService";

export const getListWrapperService = <T extends IDiaryListWrapper>(
	apiUrl: ListWrapperUrls
) => {
	const crudService = new CrudService<T>(apiUrl);

	return {
		createList: crudService.create,
		updateList: crudService.update,
		deleteList: crudService.delete,
	};
};

export const purchaseListsService = getListWrapperService<IPurchaseList>(
	"purchaseLists"
);
