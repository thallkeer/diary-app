import { IPurchasesArea } from "../models";
import { PurchasesAreaActions } from "./actions/purchasesArea-actions";

export const purchasesAreaReducer = (
  state: IPurchasesArea,
  action: PurchasesAreaActions
): IPurchasesArea => {
  switch (action.type) {
    case "ADD_PURCHASES_LIST": {
      return {
        ...state,
        purchasesLists: [...state.purchasesLists, action.payload],
      };
    }

    case "DELETE_PURCHASES_LIST": {
      return {
        ...state,
        purchasesLists: state.purchasesLists.filter(
          (pl) => pl.id !== action.payload.id
        ),
      };
    }

    default:
      return state;
  }
};
