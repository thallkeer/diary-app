import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
import {
	getImportantEventsList,
	getMainPage,
} from "../../store/pages/pages.selectors";
import Loader from "../Loader";
import { EventList } from "../Lists/EventList/EventList";
import { IEventItemActions } from "../Lists/Controls/EventInput";
import { eventThunks } from "../../store/diaryLists/events.actions";
import { getAppInfo } from "../../selectors/app-selectors";
import {
	IMPORTANT_EVENTS_LIST,
	loadImportantEventsArea,
} from "store/pageAreas/importantEvents/importantEventsArea.actions";

const ImportantEventsArea: React.FC = () => {
	const dispatch = useDispatch();
	const mainPage = useSelector(getMainPage);
	const { user, year, month } = useSelector(getAppInfo);
	const { list } = useSelector(getImportantEventsList);

	useEffect(() => {
		if (mainPage) {
			dispatch(loadImportantEventsArea(mainPage.id));
		}
	}, [mainPage, user, year, month]);

	if (!mainPage || !list) return <Loader />;

	const eventItemActions: IEventItemActions = {
		deleteEvent: (eventId) =>
			dispatch(eventThunks.deleteListItem(eventId, IMPORTANT_EVENTS_LIST)),
		updateEvent: (event) =>
			dispatch(eventThunks.addOrUpdateListItem(event, IMPORTANT_EVENTS_LIST)),
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
