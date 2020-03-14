import { ActionsUnion, createAction } from "./action-helpers";
import axios from "../../axios/axios";
import { ITodoList } from "../../models";

export const ADD_PURCHASES_LIST = "ADD_PURCHASES_LIST";

const Actions = {
  addList: (purchasesList: ITodoList) =>
    createAction(ADD_PURCHASES_LIST, purchasesList)
};

export const Thunks = {
  addPurchasesList: (purchasesList: ITodoList) => {
    return dispatch => {
      if (!purchasesList) return;

      axios.post("todo", purchasesList).then(res => {
        purchasesList.id = res.data;
        dispatch(Actions.addList(purchasesList));
      });
    };
  }
};

export type PurchasesAreaActions = ActionsUnion<typeof Actions>;
export type PurchasesAreaThunks = ActionsUnion<typeof Thunks>;
