import React, { useReducer, useEffect, useContext } from "react";
import { EventList } from "../Lists/EventList";
import {
  EventThunks,
  Thunks as eventThunks
} from "../../actions/events-actions";
import { eventsReducer } from "../../context/events";
import { EventListContext, MainPageContext } from "../../context";
import Loader from "../Loader";

export const ImportantEvents: React.FC = () => {
  const [state, _dispatch] = useReducer(eventsReducer, {
    list: null,
    loading: false,
    dispatch: () => {}
  });
  const dispatch = (action: EventThunks) => action(_dispatch);
  const pageState = useContext(MainPageContext);
  const { page, setPageState } = pageState;
  const { loading, list } = state;

  useEffect(() => {
    dispatch(eventThunks.loadEvents(page.id));
  }, [page]);

  useEffect(() => {
    if (list) {
      setPageState({ ...pageState, events: { ...state, dispatch } });
    }
  }, [list]);

  if (loading || !list) return <Loader />;

  return (
    <EventListContext.Provider value={{ ...state, dispatch }}>
      <EventList withDate readonly fillToNumber={6} />
    </EventListContext.Provider>
  );
};
