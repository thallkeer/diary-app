import { Action } from "redux";

export interface IActionWithPayload<T extends string, P> extends Action<T> {
	payload: P;
}

export function createAction<T extends string>(type: T): Action<T>;
export function createAction<T extends string, P>(
	type: T,
	payload: P
): IActionWithPayload<T, P>;
export function createAction<T extends string, P>(type: T, payload?: P) {
	return payload === undefined ? { type } : { type, payload };
}

export interface INamedAction<T extends string, P>
	extends IActionWithPayload<T, P> {
	subjectName: string;
}
export function createNamedAction<T extends string, P>(
	type: T,
	subjectName: string,
	payload: P
): INamedAction<T, P>;
export function createNamedAction<T extends string, P>(
	type: T,
	subjectName: string,
	payload?: P
) {
	return payload === undefined
		? { type, subjectName }
		: { type, subjectName, payload };
}

type FunctionType = (...args: any[]) => any;
type ActionCreatorsMap = { [actionCreator: string]: FunctionType };

export type ActionsUnion<A extends ActionCreatorsMap> = ReturnType<A[keyof A]>;
