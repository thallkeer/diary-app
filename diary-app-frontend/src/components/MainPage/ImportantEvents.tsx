import React from "react";
import { useDispatch, useSelector } from "react-redux";
import {
	getImportantEventsArea,
	getImportantEventsList,
} from "../../selectors/pages.selectors";
import Loader from "../Loader";
import { EventList } from "../Lists/EventList/EventList";
import { IEventItemActions } from "../Lists/Controls/EventInput";
import {
	loadImportantEventsArea,
	importantEventsThunks,
} from "store/pageAreas/importantEventsArea.reducer";
import { useMainPageArea } from "hooks/usePageArea";

const ImportantEventsArea: React.FC = () => {
	const dispatch = useDispatch();
	const { status } = useMainPageArea(
		getImportantEventsArea,
		loadImportantEventsArea
	);

	const { list } = useSelector(getImportantEventsList);

	if (status !== "succeeded" || !list) return <Loader />;

	const eventItemActions: IEventItemActions = {
		deleteEvent: (eventId) =>
			dispatch(importantEventsThunks.deleteItemById(eventId)),
		updateEvent: (event) =>
			dispatch(importantEventsThunks.addOrUpdateItem(event)),
	};

	return (
		<EventList
			className="mt-10 no-list-header"
			eventList={list}
			eventItemActions={eventItemActions}
		/>
	);
};

export default ImportantEventsArea;
