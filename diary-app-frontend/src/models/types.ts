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
export type ListItemUrls = "todos" | "events" | "listItems" | "habitDays";
export type PageUrls = "mainPage" | "monthPage";
export type MainPageAreaUrls = "importantEventsArea" | "importantThingsArea";
export type MonthPageAreaUrls =
	| "purchasesArea"
	| "desiresArea"
	| "ideasArea"
	| "goalsArea";
export type PageAreaUrls = MainPageAreaUrls | MonthPageAreaUrls;
