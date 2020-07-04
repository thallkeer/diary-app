import { IdeasAreaActions } from "../../actions/ideasArea-actions";
import { PageAreaState } from "../../../hooks/usePageArea";
import { IIdeasArea } from "../../../models";
import { pageAreaReducer } from "./pageArea";

export interface IIdeasAreaState extends PageAreaState<IIdeasArea> {}

export const ideasAreaReducer = (
	state: IIdeasAreaState,
	actions: IdeasAreaActions
) => pageAreaReducer<IIdeasArea, IIdeasAreaState>(state, actions);
