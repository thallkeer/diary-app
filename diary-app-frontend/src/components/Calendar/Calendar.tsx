import React, { useState, useContext, useEffect } from "react";
import { getEventsByDay } from "../../selectors";
import { AddEventForm } from "../Dialogs/AddEventForm";
import { IEvent } from "../../models";
import { Link } from "react-router-dom";
import strelka from "../../images/right-arrow.png";
import { useDispatch, useSelector } from "react-redux";
import { getAppInfo } from "../../selectors/app-selectors";
import {
	getImportantEventsArea,
	getImportantEventsList,
	getMainPage,
} from "../../selectors/page-selectors";
import { setMonth } from "../../context/reducers/app-reducer";
import { eventsActions } from "../../context/reducers/list/events";
import { IMPORTANT_EVENTS_LIST } from "../../context/reducers/pageArea/importantEventsArea-reducer";

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

	//

	const getDaysInMonth = (): number => {
		let curDate = currentDate();
		let newDate = new Date(curDate.getFullYear(), curDate.getMonth() + 1, 0);
		return newDate.getDate();
	};

	const currentDate = () => {
		const appDate = new Date(year, month - 1);
		return appDate;
	};

	const currentDay = (): number => new Date().getDate();

	const getFirstDayOfMonth = (): number => {
		let curDate = currentDate();

		let fDay = new Date(
			curDate.getFullYear(),
			curDate.getMonth() - 1,
			1
		).getDay();
		return fDay === 0 ? 7 : fDay;
	};

	const addEvent = (newEvent: IEvent) => {
		dispatch(
			eventsActions.addOrUpdateListItem(
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
		let blanks = [];
		let firstDayOfMonth = getFirstDayOfMonth();

		for (let i = 0; i < firstDayOfMonth - 1; i++)
			blanks.push(<td key={i * 80} className="empty-slot"></td>);

		return blanks;
	};

	const getDays = (): any[] => {
		let daysInMonth = [];

		let curDay = currentDay();

		let isRealCurrentMonth =
			currentDate().getMonth() === new Date().getMonth() + 1;

		const monthDays = getDaysInMonth();

		for (let d = 1; d <= monthDays; d++) {
			let className =
				isRealCurrentMonth && d === curDay ? "day current-day" : "day";

			let curEvents: IEvent[] = eventsByDay.get(d) || [];

			let curEventClass = curEvents.length ? "day-with-event" : "no-events-day";

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
		let rows = [];
		let cells = [];

		totalSlots.forEach((row, i) => {
			if (i % 7 !== 0) {
				cells.push(row);
			} else {
				let insertRow = cells.slice();
				rows.push(insertRow);
				cells = [];
				cells.push(row);
			}
			if (i === totalSlots.length - 1) {
				let insertRow = cells.slice();
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
		let date = new Date(year, month - 1);
		let stringMonth = date.toLocaleString("ru", { month: "long" });
		return stringMonth[0].toUpperCase() + stringMonth.slice(1);
	};

	const changeMonth = (increment: boolean) => {
		let newMonth = increment ? Math.min(month + 1, 12) : Math.max(month - 1, 1);

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
