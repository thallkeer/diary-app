import { IDesiresArea } from "models/PageAreas/pageAreas";
import { desireListsThunks } from "store/pageAreaLists/desiresLists/desireLists.actions";
import { PageAreaComponent } from "./pageAreas.reducer";
import { IMonthPage } from "models";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { combineReducers } from "redux";
import { desireListsReducer } from "store/pageAreaLists/desiresLists/desireLists.reducer";
import { IPageAreaState } from "models/states";

class DesiresAreaComponent extends PageAreaComponent<IMonthPage, IDesiresArea> {
	onAreaLoaded(desiresArea: IDesiresArea, dispatch) {
		dispatch(desireListsThunks.setDesireLists(desiresArea.desiresLists));
	}
}

export const desiresAreaComponent = new DesiresAreaComponent(
	"monthPage",
	"desiresArea"
);

export interface IDesiresAreaState extends IPageAreaState<IDesiresArea> {}

const initialState: IDesiresAreaState = {
	area: null,
	pageAreaName: "desiresArea",
	...INITIAL_LOADABLE_STATE,
};

export const desiresAreaReducer = combineReducers({
	area: desiresAreaComponent.getReducer(initialState),
	desireLists: desireListsReducer,
});
