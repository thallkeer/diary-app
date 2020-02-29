import { IPurchasesArea } from "../models";
import { PurchasesAreaActions } from "../actions/purchasesArea-actions";

export const purchasesAreaReducer = (
  state: IPurchasesArea,
  action: PurchasesAreaActions
): IPurchasesArea => {
  switch (action.type) {
    case "ADD_PURCHASES_LIST": {
      return {
        ...state,
        purchasesLists: [...state.purchasesLists, action.payload]
      };
    }

    default:
      return state;
  }
};
