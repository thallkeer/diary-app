import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
import {
	getImportantEventsArea,
	getImportantEventsList,
} from "../../store/pages/pages.selectors";
import Loader from "../Loader";
import { EventList } from "../Lists/EventList/EventList";
import { IEventItemActions } from "../Lists/Controls/EventInput";
import {
	importantEventsAreaComponent,
	importantEventsThunks,
} from "store/pageAreas/importantEventsArea.reducer";
import { useMainPageArea } from "hooks/usePageArea";

const ImportantEventsArea: React.FC = () => {
	const dispatch = useDispatch();
	const { isLoading } = useMainPageArea(
		getImportantEventsArea,
		importantEventsAreaComponent
	);

	const { list } = useSelector(getImportantEventsList);

	if (isLoading || !list) return <Loader />;

	const eventItemActions: IEventItemActions = {
		deleteEvent: (eventId) =>
			dispatch(importantEventsThunks.deleteListItem(eventId)),
		updateEvent: (event) =>
			dispatch(importantEventsThunks.addOrUpdateListItem(event)),
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
