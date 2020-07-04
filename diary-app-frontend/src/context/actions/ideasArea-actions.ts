import { ActionsUnion } from "./action-helpers";
import { IIdeasArea } from "../../models";
import { PageAreaBaseActionsGen } from "./pageArea-actions";

export const ideasAreaActions = PageAreaBaseActionsGen<IIdeasArea>();

export type IdeasAreaActions = ActionsUnion<typeof ideasAreaActions>;
