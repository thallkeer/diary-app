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
	showMonthPopup: boolean;
	showYearPopup: boolean;
	showYearNav: boolean;
	showAddEventForm: boolean;
	clickedDay?: number;
	clickedEvent?: IEvent;
}

export const Calendar: React.FC = () => {
	const [state, setState] = useState<ICalendarState>({
		showMonthPopup: false,
		showYearPopup: false,
		showYearNav: false,
		showAddEventForm: false,
	});

	const dispatch = useDispatch();
	const { year, month } = useSelector(getAppInfo);
	const { list } = useSelector(getImportantEventsList);
	const eventsByDay: Map<number, IEvent[]> = useSelector(getEventsByDay);

	const getDaysInMonth = (): number => {
		const curDate = currentDate();
		const newDate = new Date(curDate.getFullYear(), curDate.getMonth() + 1, 0);
		return newDate.getDate();
	};

	const currentDate = () => {
		const appDate = new Date(year, month - 1);
		return appDate;
	};

	const currentDay = (): number => new Date().getDate();

	const getFirstDayOfMonth = (): number => {
		const curDate = currentDate();

		const fDay = new Date(
			curDate.getFullYear(),
			curDate.getMonth() - 1,
			1
		).getDay();
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

	const weekdaysShortRussian: string[] = [
		"Пн",
		"Вт",
		"Ср",
		"Чт",
		"Пт",
		"Сб",
		"Вс",
	];

	const getWeekdays = () => {
		return weekdaysShortRussian.map((day) => (
			<td key={day} className="week-day">
				{day}
			</td>
		));
	};

	const getEmptySlots = (): any[] => {
		const blanks = [];
		const firstDayOfMonth = getFirstDayOfMonth();

		for (let i = 0; i < firstDayOfMonth - 1; i++)
			blanks.push(<td key={i * 80} className="empty-slot"></td>);

		return blanks;
	};

	const getDays = (): any[] => {
		const daysInMonth = [];

		const curDay = currentDay();

		const isRealCurrentMonth =
			currentDate().getMonth() === new Date().getMonth() + 1;

		const monthDays = getDaysInMonth();

		for (let d = 1; d <= monthDays; d++) {
			const className =
				isRealCurrentMonth && d === curDay ? "day current-day" : "day";

			const curEvents: IEvent[] = eventsByDay.get(d) || [];

			const curEventClass = curEvents.length
				? "day-with-event"
				: "no-events-day";

			daysInMonth.push(
				<td key={d} className={className} onClick={(e) => onDayClick(e, d)}>
					<div className="day-span">{d}</div>
					{curEvents.map((event) => (
						<div
							key={event.id}
							className={curEventClass}
							style={{ marginTop: "5px" }}
							onClick={(e) => onEventClick(e, d, event)}
						>
							{event.subject}
						</div>
					))}
				</td>
			);
		}
		return daysInMonth;
	};

	const getCalendarRows = (): any[] => {
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

	const toggle = () => {
		setState({ ...state, showAddEventForm: !state.showAddEventForm });
	};

	const getMonthName = (): string => {
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
						className="mirrored-arrow"
						width="30"
						height="30"
					/>
				</span>
				<Link className="month-name" to="/month">
					{getMonthName()}
				</Link>
				<span className="month-nav" onClick={setNextMonth}>
					<img src={strelka} alt="next month" width="30" height="30" />
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
					handleClose={toggle}
					addEvent={addEvent}
					event={state.clickedEvent}
				/>
			)}
		</div>
	);
};

export default Calendar;
