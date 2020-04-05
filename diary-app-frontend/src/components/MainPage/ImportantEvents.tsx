import React, { useEffect, useContext, useState } from "react";
import { EventList } from "../Lists/EventList";
import {
  MainPageContext,
  IMainPageContext,
  EventListContext,
} from "../../context";
import { EventListState } from "../Lists/EventListState";
import { Thunks as mainPageThunks } from "../../context/actions/mainPage-actions";
import { IMainPage } from "../../models";
import Loader from "../Loader";

export const ImportantEvents: React.FC = () => {
  const pageState = useContext(MainPageContext);
  const [page, setPage] = useState<IMainPage>(null);

  useEffect(() => {
    const check: boolean = pageState && pageState.page !== null;
    if (check) setPage(pageState.page);
  }, [pageState.page]);

  if (!page) return <Loader />;

  return (
    <EventListState page={page}>
      <ImportantEventsList pageState={pageState} />
    </EventListState>
  );
};

export const ImportantEventsList: React.FC<{ pageState: IMainPageContext }> = ({
  pageState,
}) => {
  const state = useContext(EventListContext);
  const { dispatch } = pageState;
  const { list } = state;

  useEffect(() => {
    if (list !== null) dispatch(mainPageThunks.setEvents(state));
  }, [list]);

  return <EventList withDate readonly />;
};
