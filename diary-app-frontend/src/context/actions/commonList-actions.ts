import { ActionsUnion } from "./action-helpers";
import { IListItem, ICommonList } from "../../models/index";
import { getActions } from "./list-actions";

const commonListActions = getActions<ICommonList, IListItem>();

export type CommonListActions = ActionsUnion<typeof commonListActions>;
