import React, { useReducer, useEffect, useContext } from "react";
import { EventList } from "../Lists/EventList";
import {
  EventThunks,
  Thunks as eventThunks
} from "../../actions/events-actions";
import { eventsReducer } from "../../context/events";
import { EventListContext, IMainPageState, ListState } from "../../context";
import { MainPageContext } from "./MainPage";
import Loader from "../Loader";

type Props = {
  setEventsState: React.Dispatch<React.SetStateAction<IMainPageState>>;
};

export const ImportantEvents: React.FC<Props> = ({ setEventsState }) => {
  const [state, _dispatch] = useReducer(eventsReducer, {
    list: null,
    loading: false
  });
  const dispatch = (action: EventThunks) => action(_dispatch);
  const mainPageState = useContext(MainPageContext);
  const { id, month, year } = mainPageState.page;

  useEffect(() => {
    dispatch(eventThunks.loadEvents(id));
    setEventsState({
      ...mainPageState,
      events: { dispatch, eventList: state }
    });
  }, [id, month, year]);

  if (state.loading || !state.list) return <Loader />;

  return (
    <EventListContext.Provider value={{ eventList: state, dispatch }}>
      <EventList withDate readonly fillToNumber={6} />
    </EventListContext.Provider>
  );
};
