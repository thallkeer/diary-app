import { Action } from "redux";
import { IStateWithLoading } from "../../models/states";

export const isLoadingReducer = <S extends IStateWithLoading>(state: S): S => ({
	...state,
	error: false,
	isLoading: true,
	success: false,
});

const successReducer = <S extends IStateWithLoading>(state: S): S => ({
	...state,
	error: false,
	isLoading: false,
	success: true,
});

const errorReducer = <S extends IStateWithLoading>(state: S): S => ({
	...state,
	error: true,
	isLoading: false,
	success: false,
});

// Initial state of the async flags
// Can be spread into the initial state object of your base reducer
export const INITIAL_LOADABLE_STATE: IStateWithLoading = {
	error: false,
	isLoading: false,
	success: false,
};

type LoadingActionTypes = Record<"START" | "SUCCESS" | "ERROR", string>;

/*
	ActionTypes is an object that has three key value pairs
	  - isLoadingActionType
	  - successActionType
	  - errorActionType
	with each value being the action type for the associated reducer
 */
export const withLoadingStates = ({
	START,
	SUCCESS,
	ERROR,
}: LoadingActionTypes) => {
	// Create an object to map the the given action types to
	// the correct reducer defined above
	const actionReducerMap = {
		[START]: isLoadingReducer,
		[SUCCESS]: successReducer,
		[ERROR]: errorReducer,
	};

	// Returns a higher order reducer that takes a baseReducer
	return <S extends IStateWithLoading, A extends Action>(
		baseReducer: (state: S | undefined, action: A) => S
	) =>
		// Returns a new reducer
		(state: S | undefined, action: A): S => {
			// Is the action type a loadable action specified above?
			// if yes, set the action reducer, else set the noopReducer
			const reducerFunction = actionReducerMap[action.type];
			// compute new state with the specificed reducer set in reducerAction
			const nextState = reducerFunction ? reducerFunction(state) : state;
			// return the result of the newState and action passed into the baseReducer
			return baseReducer(nextState, action);
		};
};

export default withLoadingStates;
