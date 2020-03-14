import React, { useReducer, useEffect } from "react";
import { IPage, IEventList, IEvent } from "../../models";
import { EventListContext } from "../../context";
import {
  EventThunks,
  Thunks as eventThunks
} from "../../context/actions/events-actions";
import { eventsReducer } from "../../context/events";

export const EventListState: React.FC<{
  page?: IPage;
  initList?: IEventList;
}> = ({ page, initList, children }) => {
  const [state, _dispatch] = useReducer(eventsReducer, {
    loading: false,
    list: null
  });

  const { list } = state;

  const {
    addOrUpdateEvent,
    deleteEvent,
    loadEventsByPageID,
    setEventList,
    updateEventList
  } = eventThunks;

  const dispatch = (action: EventThunks) => action(_dispatch);

  useEffect(() => {
    if (!list) {
      if (initList) dispatch(setEventList(initList));
      else if (page) dispatch(loadEventsByPageID(page.id));
    }
  }, [page, initList]);

  const deleteItem = (eventID: number) => {
    eventID !== 0 && dispatch(deleteEvent(eventID));
  };

  const addOrUpdate = (event: IEvent) => {
    dispatch(
      addOrUpdateEvent({
        ...event,
        ownerID: list.id
      })
    );
  };

  const updateListTitle = (title: string) => {
    dispatch(
      updateEventList({
        ...list,
        title
      })
    );
  };

  return (
    <EventListContext.Provider
      value={{
        ...state,
        deleteItem,
        updateListTitle,
        addOrUpdateItem: addOrUpdate
      }}
    >
      {children}
    </EventListContext.Provider>
  );
};
