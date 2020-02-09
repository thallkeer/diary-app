import React, { useEffect, useContext } from "react";
import { EventList } from "../Lists/EventList";
import { MainPageContext, IMainPageContext } from "../../context";
import Loader from "../Loader";
import { useEvents } from "../../hooks/useLists";
import { IEventList } from "../../models";

export const ImportantEvents: React.FC = () => {
  const pageState = useContext(MainPageContext);
  const { page, setPageState } = pageState;

  const state = useEvents(page);
  const { loading, list, dispatch } = state;

  const newPageState: IMainPageContext = {
    ...pageState,
    events: state
  };

  useEffect(() => {
    list && setPageState(newPageState);
  }, [list, setPageState]);

  if (loading || !list) return <Loader />;

  let listCopy: IEventList;
  listCopy = Object.assign({}, list);

  listCopy.items.sort((e1, e2) => e1.date.getTime() - e2.date.getTime());

  return (
    <EventList
      withDate
      readonly
      fillToNumber={6}
      eventList={listCopy}
      dispatch={dispatch}
    />
  );
};
