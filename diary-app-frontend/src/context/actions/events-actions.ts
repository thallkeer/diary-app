import { ActionsUnion } from "./action-helpers";
import { IEvent, IEventList } from "../../models/index";
import { getActions } from "./list-actions";

const eventListActions = getActions<IEventList, IEvent>();

export type EventListActions = ActionsUnion<typeof eventListActions>;
