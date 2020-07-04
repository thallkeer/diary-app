import {
	IMainPage,
	ListItem,
	IPage,
	IMonthPage,
	IGoalsArea,
	IUser,
	IHabitsTracker,
	List,
} from "../models/index";

export interface IAppState {
	month: number;
	year: number;
	user: IUser;
	mainPage: IMainPage;
	monthPage: IMonthPage;
	selectedPage: PageType;
}

export interface IPageState {
	page: IPage;
	loading: boolean;
}

export type PageType = IMainPage | IMonthPage;

export type ListFunctions = {
	setList: (list: List) => void;
	loadByPageID: (pageID: number) => void;
	updateListTitle: (title: string) => void;
	addOrUpdateItem: (item: ListItem) => void;
	deleteListItem: (itemID: number) => void;
};

export interface IListContext {
	listFunctions?: Partial<ListFunctions>;
}

export interface IGoalsAreaContext {
	goalsArea: IGoalsArea;
	addOrUpdate: (tracker?: IHabitsTracker) => void;
	deleteTracker: (tracker: IHabitsTracker) => void;
}
