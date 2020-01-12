import React, { useEffect, useContext } from "react";
import { EventList } from "../Lists/EventList";
import { MainPageContext } from "../../context";
import Loader from "../Loader";
import { useEvents } from "../../hooks/useLists";

export const ImportantEvents: React.FC = () => {
  const pageState = useContext(MainPageContext);
  const { page, setPageState } = pageState;
  const [state, dispatch] = useEvents(page);

  const { loading, list } = state;

  useEffect(() => {
    if (list) {
      setPageState({ ...pageState, events: { ...state, dispatch } });
    }
  }, [list]);

  if (loading || !list) return <Loader />;

  return (
    <EventList
      withDate
      readonly
      fillToNumber={6}
      eventList={state.list}
      dispatch={dispatch}
    />
  );
};
