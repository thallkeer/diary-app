export type ListUrls =
	| "todoLists"
	| "eventLists"
	| "commonLists"
	| "habitTrackers";
export type ListWrapperUrls =
	| "purchaseLists"
	| "desireLists"
	| "ideasLists"
	| "goalLists";
export type ListItemUrls = "todos" | "events" | "listItems";
export type PageNames = "mainPage" | "monthPage";
export type MainPageAreaNames = "importantEventsArea" | "importantThingsArea";
export type MonthPageAreaNames =
	| "purchasesArea"
	| "desiresArea"
	| "ideasArea"
	| "goalsArea";
export type PageAreaNames = MainPageAreaNames | MonthPageAreaNames;
