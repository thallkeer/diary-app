export interface IAction<T extends string, P> {
  type: T;
  payload?: P;
  error?: boolean;
  meta?: any;
}

export function createAction<T extends string, P>(
  type: T,
  payload?: P,
  error?: boolean,
  meta?: any
): IAction<T, P> {
  return { type, payload, error, meta };
}

type FunctionType = (...args: any[]) => any;
type ActionCreatorsMap = { [actionCreator: string]: FunctionType };

export type ActionsUnion<A extends ActionCreatorsMap> = ReturnType<A[keyof A]>;
