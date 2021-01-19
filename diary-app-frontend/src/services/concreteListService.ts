import { IHabitTracker } from "models";
import { IPurchaseList } from "../models/entities";
import { ListUrls, ListWrapperUrls } from "../models/types";
import { CrudService } from "./crudService";

const habitTrackerApi: ListUrls = "habitTrackers";
export const habitTrackerService = new CrudService<IHabitTracker>(
	habitTrackerApi
);

const purchaseListsApi: ListWrapperUrls = "purchaseLists";

export const purchaseListsService = new CrudService<IPurchaseList>(
	purchaseListsApi
);
