import { ICommonList, IListItem } from "models";
import { IListState } from "models/states";
import { ActionsUnion } from "store/actions/action-helpers";
import { DiaryListComponent } from "./lists.reducer";

class CommonListComponent extends DiaryListComponent<ICommonList, IListItem> {}

export const commonListComponent = new CommonListComponent(
	"commonLists",
	"listItems"
);

const commonListActions = commonListComponent.getActions("commonList");
export type CommonListActions = ActionsUnion<typeof commonListActions>;

export interface ICommonListState extends IListState<ICommonList, IListItem> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

export const commonListInitialState: ICommonListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: true,
};

export const createCommonListReducer = (reducerName: string) => {
	return commonListComponent.getReducer(commonListInitialState, reducerName);
};
