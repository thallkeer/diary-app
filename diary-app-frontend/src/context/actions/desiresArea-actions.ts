import { ActionsUnion } from "./action-helpers";
import { IDesiresArea } from "../../models";
import { PageAreaBaseActionsGen } from "./pageArea-actions";

export const desiresAreaActions = PageAreaBaseActionsGen<IDesiresArea>();

export type DesiresAreaActions = ActionsUnion<typeof desiresAreaActions>;
