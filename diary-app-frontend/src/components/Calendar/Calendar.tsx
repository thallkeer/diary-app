import React, { useState } from "react";
import { getEventsByDay } from "../../selectors/lists-selectors";
import { AddEventForm } from "../Dialogs/AddEventForm";
import { Link } from "react-router-dom";
import strelka from "../../images/right-arrow.png";
import { useSelector } from "react-redux";
import { getAppInfo } from "../../selectors/app-selectors";
import { getImportantEventsList } from "selectors/pages.selectors";
import { importantEventsThunks } from "store/pageAreas";
import {
	DragDropContext,
	DropResult,
	ResponderProvided,
	Draggable,
	Droppable,
} from "react-beautiful-dnd";
import { IEvent } from "models/index";
import { setMonth } from "store/app/appSlice";
import { useAppDispatch } from "hooks/hooks";

interface ICalendarState {
	showAddEventForm: boolean;
	clickedDay?: number;
	clickedEvent?: IEvent;
}

const weekdaysShortRussian: string[] = [
	"Пн",
	"Вт",
	"Ср",
	"Чт",
	"Пт",
	"Сб",
	"Вс",
];

export const Calendar: React.FC = () => {
	const [state, setState] = useState<ICalendarState>({
		showAddEventForm: false,
	});

	const dispatch = useAppDispatch();
	const { year, month } = useSelector(getAppInfo);
	const importantEventsList = useSelector(getImportantEventsList).list;
	const eventsByDay: Map<number, IEvent[]> = useSelector(getEventsByDay);

	const getDaysInMonth = (): number => {
		const curDate = getCurrentAppDate();
		const newDate = new Date(curDate.getFullYear(), curDate.getMonth() + 1, 0);
		return newDate.getDate();
	};

	/**
	 * Represents the date set in the app.
	 */
	const getCurrentAppDate = () => {
		const appDate = new Date(year, month - 1);
		return appDate;
	};

	/**
	 * Represents a real time day
	 */
	const getPresentDay = (): number => getPresentDate().getDate();

	/**
	 * Represents a real time date
	 */
	const getPresentDate = (): Date => new Date();

	/**
	 * Returns first day of week of app month
	 */
	const getFirstDayOfMonth = (): number => {
		const curDate = getCurrentAppDate();
		const fDay = curDate.getDay();
		return fDay === 0 ? 7 : fDay;
	};

	const addEvent = (newEvent: IEvent) => {
		dispatch(
			importantEventsThunks.addOrUpdateItem({
				...newEvent,
				ownerId: importantEventsList.id,
			})
		);
	};

	let eventClicked = false;

	const onDayClick = (e: React.MouseEvent<HTMLElement>, day: number) => {
		if (eventClicked) return;
		e.preventDefault();
		showModal(day);
	};

	const onEventClick = (
		e: React.MouseEvent<HTMLElement>,
		day: number,
		event: IEvent
	) => {
		eventClicked = true;
		e.preventDefault();
		showModal(day, event);
	};

	const getWeekdays = () => {
		return weekdaysShortRussian.map((day) => (
			<td key={day} className="week-day">
				{day}
			</td>
		));
	};

	const getEmptySlots = (): JSX.Element[] => {
		const blanks: JSX.Element[] = [];
		const firstDayOfMonth = getFirstDayOfMonth();

		for (let i = 0; i < firstDayOfMonth - 1; i++)
			blanks.push(<td key={i * 80} className="empty-slot"></td>);

		return blanks;
	};

	const getDays = (): JSX.Element[] => {
		const daysInMonth: JSX.Element[] = [];

		const presentDay = getPresentDay();

		const isRealCurrentMonth =
			getPresentDate().getMonth() === getCurrentAppDate().getMonth();

		const monthDays = getDaysInMonth();

		for (let d = 1; d <= monthDays; d++) {
			const className =
				isRealCurrentMonth && d === presentDay
					? "day real-time-current-day"
					: "day";

			const curEvents: IEvent[] = eventsByDay.get(d) || [];

			daysInMonth.push(
				<CalendarDay
					key={d}
					day={d}
					className={className}
					curEvents={curEvents}
					onDayClick={onDayClick}
					onEventClick={onEventClick}
				/>
			);
		}
		return daysInMonth;
	};

	const onDragEnd = (result: DropResult, provided: ResponderProvided): void => {
		const { destination, source, draggableId } = result;
		if (!destination) return;

		if (
			destination.droppableId === source.droppableId &&
			destination.index === source.index
		)
			return;

		const sourceDayNumber = Number.parseInt(source.droppableId);
		const eventId = Number.parseInt(draggableId);
		const eventsOfDay: IEvent[] = eventsByDay.get(sourceDayNumber) || [];
		const eventToUpdate = eventsOfDay.find((e) => e.id === eventId);
		const date = new Date(eventToUpdate.date.getTime());

		const destinationDayNumber = Number.parseInt(destination.droppableId);
		date.setDate(destinationDayNumber);

		addEvent({
			...eventToUpdate,
			date,
		});
	};

	const getCalendarRows = () => {
		var totalSlots = [...getEmptySlots(), ...getDays()];
		const rows = [];
		let cells = [];

		totalSlots.forEach((row, i) => {
			if (i % 7 !== 0) {
				cells.push(row);
			} else {
				const insertRow = cells.slice();
				rows.push(insertRow);
				cells = [];
				cells.push(row);
			}
			if (i === totalSlots.length - 1) {
				const insertRow = cells.slice();
				rows.push(insertRow);
			}
		});

		return (
			<DragDropContext onDragEnd={onDragEnd}>
				{rows.map((d, i) => (
					<tr key={d + i}>{d}</tr>
				))}
			</DragDropContext>
		);
	};

	const showModal = (day: number, event?: IEvent) => {
		setState({
			...state,
			showAddEventForm: true,
			clickedDay: day,
			clickedEvent: event,
		});
	};

	const onAddEventClosed = () => {
		setState({ ...state, showAddEventForm: !state.showAddEventForm });
	};

	const getCurrentMonthName = (): string => {
		const date = new Date(year, month - 1);
		const stringMonth = date.toLocaleString("ru", { month: "long" });
		return stringMonth[0].toUpperCase() + stringMonth.slice(1);
	};

	const changeMonth = (increment: boolean) => {
		const newMonth = increment
			? Math.min(month + 1, 12)
			: Math.max(month - 1, 1);

		if (newMonth === month) return;
		dispatch(setMonth(newMonth));
	};

	const setNextMonth = () => {
		changeMonth(true);
	};

	const setPrevMonth = () => {
		changeMonth(false);
	};

	return (
		<div>
			<h1 className="text-center calendar-header">
				<span className="month-nav" onClick={setPrevMonth}>
					<img
						src={strelka}
						alt="previous month"
						className="month-arrow mirrored-arrow"
					/>
				</span>
				<Link className="month-name" to="/month">
					{getCurrentMonthName()}
				</Link>
				<span className="month-nav" onClick={setNextMonth}>
					<img src={strelka} alt="next month" className="month-arrow" />
				</span>
			</h1>
			<div className="calendar-container">
				<table className="calendar">
					<tbody>
						<tr>{getWeekdays()}</tr>
						{getCalendarRows()}
					</tbody>
				</table>
			</div>
			{state.showAddEventForm && (
				<AddEventForm
					day={state.clickedDay}
					show={state.showAddEventForm}
					handleClose={onAddEventClosed}
					addEvent={addEvent}
					event={state.clickedEvent}
				/>
			)}
		</div>
	);
};
export default Calendar;

const CalendarDay: React.FC<{
	day: number;
	className: string;
	onDayClick: (e: React.MouseEvent<HTMLElement>, day: number) => void;
	curEvents: IEvent[];
	onEventClick: (
		e: React.MouseEvent<HTMLElement>,
		day: number,
		event: IEvent
	) => void;
}> = ({ day, className, onDayClick, curEvents, onEventClick }) => {
	const events = [...curEvents].sort(
		(e1, e2) => e1.date.getTime() - e2.date.getTime()
	);

	return (
		<Droppable droppableId={day.toString()}>
			{(provided) => (
				<td
					className={className}
					onClick={(e) => onDayClick(e, day)}
					ref={provided.innerRef}
					{...provided.droppableProps}
				>
					<div className="day-container">
						<div className="day-span">{day}</div>
						{events.length !== 0 && (
							<div style={{ marginBottom: "0.2rem" }}>
								{events.map((event) => (
									<DayEvent
										key={event.id}
										event={event}
										day={day}
										onEventClick={onEventClick}
									/>
								))}
							</div>
						)}
						{provided.placeholder}
					</div>
				</td>
			)}
		</Droppable>
	);
};

const DayEvent: React.FC<{
	event: IEvent;
	day: number;
	onEventClick: (
		e: React.MouseEvent<HTMLElement>,
		day: number,
		event: IEvent
	) => void;
}> = ({ event, day, onEventClick }) => {
	const formatOptions: Intl.DateTimeFormatOptions = {
		hour: "2-digit",
		minute: "2-digit",
	};

	const locale = "ru";

	const formattedEvent = `${event.date.toLocaleTimeString(
		locale,
		formatOptions
	)} ${event.subject}`;

	return (
		<Draggable draggableId={event.id.toString()} index={day}>
			{(provided) => (
				<div
					key={event.id}
					className={"day-event"}
					onClick={(e) => onEventClick(e, day, event)}
					ref={provided.innerRef}
					{...provided.draggableProps}
					{...provided.dragHandleProps}
				>
					{formattedEvent}
				</div>
			)}
		</Draggable>
	);
};
