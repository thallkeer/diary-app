import { ActionsUnion, createAction } from "./action-helpers";
import axios from "../../axios/axios";
import { ITodoList } from "../../models";

export const ADD_PURCHASES_LIST = "ADD_PURCHASES_LIST";
export const DELETE_PURCHASES_LIST = "DELETE_PURCHASES_LIST";

const Actions = {
  addList: (purchasesList: ITodoList) =>
    createAction(ADD_PURCHASES_LIST, purchasesList),
  deleteList: (purchasesListId: number) =>
    createAction(DELETE_PURCHASES_LIST, purchasesListId),
};

export const Thunks = {
  addPurchasesList: (purchasesList: ITodoList) => {
    return (dispatch) => {
      if (!purchasesList) return;

      axios.post("todo", purchasesList).then((res) => {
        purchasesList.id = res.data;
        dispatch(Actions.addList(purchasesList));
      });
    };
  },

  deletePurchasesList: (purchasesList: ITodoList) => {
    return (dispatch) => {
      if (!purchasesList) return;
      console.log("in purchases area delete func");
      dispatch(Actions.deleteList(purchasesList.id));
    };
  },
};

export type PurchasesAreaActions = ActionsUnion<typeof Actions>;
export type PurchasesAreaThunks = ActionsUnion<typeof Thunks>;
