import { ActionsUnion, createAction } from "./action-helpers";
import { IEventList, IPage } from "../../models";
import axios from "../../axios/axios";
import { IEventListContext } from "..";

export const SET_PAGE = "SET_PAGE";
export const SET_EVENTS = "SET_EVENTS";

const Actions = {
  setPage: (page: IPage) => createAction(SET_PAGE, page),
  setEvents: (eventListContext: IEventListContext) =>
    createAction(SET_EVENTS, eventListContext)
};

export const Thunks = {
  setEvents: (eventList: IEventListContext) => {
    return dispatch => {
      dispatch(Actions.setEvents(eventList));
    };
  },

  setPage: (page: IPage) => {
    return dispatch => {
      dispatch(Actions.setPage(page));
    };
  }
};

export type MainPageActions = ActionsUnion<typeof Actions>;
export type MainPageThunks = ActionsUnion<typeof Thunks>;
