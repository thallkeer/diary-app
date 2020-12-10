import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
import {
	getImportantEventsList,
	getMainPage,
} from "../../selectors/page-selectors";
import Loader from "../Loader";
import { EventList } from "../Lists/EventList/EventList";
import {
	IMPORTANT_EVENTS_LIST,
	loadImportantEventsArea,
} from "../../context/reducers/pageArea/importantEventsArea-reducer";
import { IEventItemActions } from "../Lists/Controls/EventInput";
import { eventsActions } from "../../context/reducers/list/events";
import { getAppInfo } from "../../selectors/app-selectors";

const ImportantEventsArea: React.FC = () => {
	const dispatch = useDispatch();
	const mainPage = useSelector(getMainPage);
	const { user, year, month } = useSelector(getAppInfo);
	const { isLoading, list } = useSelector(getImportantEventsList);

	useEffect(() => {
		if (mainPage) {
			dispatch(loadImportantEventsArea(mainPage.id));
		}
	}, [mainPage, user, year, month]);

	if (isLoading || !mainPage || !list) return <Loader />;

	const eventItemActions: IEventItemActions = {
		deleteEvent: (eventId) =>
			dispatch(eventsActions.deleteListItem(eventId, IMPORTANT_EVENTS_LIST)),
		updateEvent: (event) =>
			dispatch(eventsActions.addOrUpdateListItem(event, IMPORTANT_EVENTS_LIST)),
		getItemText: (event) => `${event.date} ${event.subject}`,
	};

	return (
		<Row>
			<Col md={12}>
				<EventList
					className="mt-10 no-list-header"
					eventList={list}
					eventItemActions={eventItemActions}
				/>
			</Col>
		</Row>
	);
};

export default ImportantEventsArea;
