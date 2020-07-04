import { GoalsAreaActions } from "../../actions/goalsArea-actions";
import { IGoalsArea } from "../../../models";
import { PageAreaState } from "../../../hooks/usePageArea";
import { pageAreaReducer } from "./pageArea";
import { getGoalsLists } from "../../../selectors";

export interface IGoalsAreaState extends PageAreaState<IGoalsArea> {}

export const goalsAreaReducer = (
	state: IGoalsAreaState,
	action: GoalsAreaActions
): IGoalsAreaState => {
	const area = state.area;

	switch (action.type) {
		case "ADD_TRACKER": {
			return {
				...state,
				area: {
					...area,
					goalsLists: [...getGoalsLists(state), action.payload],
				},
			};
		}

		case "UPDATE_TRACKER":
			return {
				...state,
				area: {
					...area,
					goalsLists: getGoalsLists(state).map((t) =>
						t.id === action.payload.id ? action.payload : t
					),
				},
			};

		case "DELETE_TRACKER":
			return {
				...state,
				area: {
					...area,
					goalsLists: getGoalsLists(state).filter(
						(t) => t.id !== action.payload.id
					),
				},
			};

		default:
			return pageAreaReducer<IGoalsArea, IGoalsAreaState>(state, action);
	}
};
