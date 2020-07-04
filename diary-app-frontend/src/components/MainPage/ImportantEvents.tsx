import React, { useEffect, useContext } from "react";
import { EventList } from "../Lists/EventList/EventList";
import {
	EventListState,
	EventListContext,
} from "../Lists/EventList/EventListState";
import { mainPageContext } from "./MainPageState";
import { Actions as mainPageActions } from "../../context/actions/mainPage-actions";
import Loader from "../Loader";
import { getPageEvents } from "../../selectors";

const ImportantEvents: React.FC = () => {
	const mpContext = useContext(mainPageContext);
	const { mainPage, loading } = mpContext.state;

	if (loading || !mainPage || !mainPage.page) return <Loader />;

	return (
		<EventListState>
			<ImportantEventsList />
		</EventListState>
	);
};

export const ImportantEventsList: React.FC = () => {
	const pageState = useContext(mainPageContext);
	const { state, dispatch } = pageState;
	const pageEvents = getPageEvents(state);
	const eventsContext = useContext(EventListContext);

	useEffect(() => {
		const eventList = eventsContext.eventListState.list;
		if (eventList !== null && (!pageEvents || pageEvents.list !== eventList)) {
			dispatch(mainPageActions.setEvents(eventsContext));
		}
	}, [eventsContext, dispatch, pageEvents]);

	return <EventList />;
};

export default ImportantEvents;
