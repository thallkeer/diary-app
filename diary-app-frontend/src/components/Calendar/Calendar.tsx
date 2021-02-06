import React, { useState } from "react";
import { getEventsByDay } from "../../selectors/lists-selectors";
import { AddEventForm } from "../Dialogs/AddEventForm";
import { Link } from "react-router-dom";
import strelka from "../../images/right-arrow.png";
import { useDispatch, useSelector } from "react-redux";
import { getAppInfo } from "../../selectors/app-selectors";
import { getImportantEventsList } from "../../store/pages/pages.selectors";
import { eventThunks } from "../../store/diaryLists/events.actions";
import { IEvent } from "models";
import { AppThunks } from "store/app/app.actions";
import { IMPORTANT_EVENTS_LIST } from "store/pageAreas/importantEvents/importantEventsArea.actions";

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

	const dispatch = useDispatch();
	const { year, month } = useSelector(getAppInfo);
	const { list } = useSelector(getImportantEventsList);
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
			eventThunks.addOrUpdateListItem(
				{
					...newEvent,
					ownerID: list.id,
				},
				IMPORTANT_EVENTS_LIST
			)
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
				isRealCurrentMonth && d === presentDay ? "day current-day" : "day";

			const curEvents: IEvent[] = eventsByDay.get(d) || [];

			const curEventClass = curEvents.length
				? "day-with-event"
				: "no-events-day";

			daysInMonth.push(
				<CalendarDay
					key={d}
					day={d}
					className={className}
					curEvents={curEvents}
					curEventClass={curEventClass}
					onDayClick={onDayClick}
					onEventClick={onEventClick}
				/>
			);
		}
		return daysInMonth;
	};

	const getCalendarRows = (): JSX.Element[] => {
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

		return rows.map((d, i) => {
			return <tr key={d + i}>{d}</tr>;
		});
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
		dispatch(AppThunks.setMonth(newMonth));
	};

	const setNextMonth = () => {
		changeMonth(true);
	};

	const setPrevMonth = () => {
		changeMonth(false);
	};

	return (
		<div className="calendar-wrapper">
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
	curEventClass: string;
	onEventClick: (
		e: React.MouseEvent<HTMLElement>,
		day: number,
		event: IEvent
	) => void;
}> = ({
	day,
	className,
	onDayClick,
	curEvents,
	curEventClass,
	onEventClick,
}) => {
	return (
		<td className={className} onClick={(e) => onDayClick(e, day)}>
			<div className="day-span">{day}</div>
			{curEvents.map((event) => (
				<div
					key={event.id}
					className={curEventClass}
					onClick={(e) => onEventClick(e, day, event)}
				>
					{event.subject}
				</div>
			))}
		</td>
	);
};
