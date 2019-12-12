// import { Action } from "redux";

// export default interface IAction<T> extends Action<string> {
//   type: string;
//   payload?: T;
//   error?: boolean;
//   meta?: any;
// }

// export function createAction<T>(
//   type: string,
//   payload?: T,
//   error?: boolean,
//   meta?: any
// ): IAction<T>;

export function createAction<T>(
  type: string,
  payload?: T,
  error?: boolean,
  meta?: any
) {
  return { type, payload, error, meta };
}

type FunctionType = (...args: any[]) => any;
type ActionCreatorsMap = { [actionCreator: string]: FunctionType };

export type ActionsUnion<A extends ActionCreatorsMap> = ReturnType<A[keyof A]>;
