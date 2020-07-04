import { PageAreaState } from "../../../hooks/usePageArea";
import { IDesiresArea } from "../../../models";
import { pageAreaReducer } from "./pageArea";
import { DesiresAreaActions } from "../../actions/desiresArea-actions";

export interface IDesiresAreaState extends PageAreaState<IDesiresArea> {}

export const desiresAreaReducer = (
	state: IDesiresAreaState,
	actions: DesiresAreaActions
) => pageAreaReducer<IDesiresArea, IDesiresAreaState>(state, actions);
