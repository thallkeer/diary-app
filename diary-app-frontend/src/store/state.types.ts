import { Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { AppStateType } from "./reducer";

export type BaseThunkType<
	A extends Action = Action,
	R = Promise<void>
> = ThunkAction<R, AppStateType, unknown, A>;
