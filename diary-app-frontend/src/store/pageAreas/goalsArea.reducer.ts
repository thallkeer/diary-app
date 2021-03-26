import { IMonthPage } from "models";
import { IGoalsArea } from "models/PageAreas/pageAreas";
import { IPageAreaState } from "models/states";
import { combineReducers } from "redux";
import { goalsListsReducer, goalsListsThunks } from "store/pageAreaLists";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { PageAreaComponent } from "./pageAreas.reducer";

class GoalsAreaComponent extends PageAreaComponent<IMonthPage, IGoalsArea> {
	onAreaLoaded(pageArea: IGoalsArea, dispatch): void {
		dispatch(goalsListsThunks.setGoalsLists(pageArea.goalLists));
	}
}

export const goalsAreaComponent = new GoalsAreaComponent(
	"monthPage",
	"goalsArea"
);

export interface IGoalsAreaState extends IPageAreaState<IGoalsArea> {}

const initialState: IGoalsAreaState = {
	area: null,
	pageAreaName: "goalsArea",
	...INITIAL_LOADABLE_STATE,
};

export const goalsAreaReducer = combineReducers({
	area: goalsAreaComponent.getReducer(initialState),
	goalsLists: goalsListsReducer,
});
