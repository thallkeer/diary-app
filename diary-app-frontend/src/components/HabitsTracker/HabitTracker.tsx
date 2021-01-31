import React from "react";
import { useSelector } from "react-redux";
import { IHabitDay, IHabitTracker } from "models";
import { getAppInfo } from "../../selectors/app-selectors";
import { HabitDayCell } from "./HabitDayCell";

export const HabitTracker: React.FC<{
	tracker: IHabitTracker;
	trackerActions: {
		updateHabitTracker: (tracker: IHabitTracker) => void;
		markDay: (day: IHabitDay) => void;
		unmarkDay: (day: IHabitDay) => void;
	};
}> = ({ tracker, trackerActions }) => {
	const { year, month } = useSelector(getAppInfo);
	const { updateHabitTracker, markDay, unmarkDay } = trackerActions;

	const onDayClick = (e: React.MouseEvent<HTMLElement>, day: IHabitDay) => {
		if (tracker.items.some((hd) => hd.number === day.number)) {
			unmarkDay(day);
		} else {
			markDay(day);
		}
	};

	const getDaysInMonth = () => {
		const daysInMonth = [];

		const days = new Date(year, month, 0).getDate();

		const daysInFirstRow = Math.round(days / 2);

		for (let d = 1; d <= days; d++) {
			const markedDay = tracker.items.find((day) => day.number === d);
			let cn = "p-2 day-cell";

			if (markedDay) cn += " marked";
			if (d !== 1 && d !== daysInFirstRow + 1) cn += " no-left-border";
			if (d >= daysInFirstRow + 1) cn += " no-top-border";

			const habitDay = markedDay || {
				id: 0,
				number: d,
				note: "",
				habitTrackerId: tracker.id,
			};

			const dayCell = (
				<button key={d} className={cn} onClick={(e) => onDayClick(e, habitDay)}>
					<HabitDayCell
						key={d}
						className={cn}
						tracker={tracker}
						updateHabitTracker={updateHabitTracker}
						day={habitDay}
						isMarked={markedDay ? true : false}
					/>
				</button>
			);

			daysInMonth.push(dayCell);
		}

		if (days % 2 !== 0)
			daysInMonth.push(
				<div
					className="p-2 day-cell no-left-border no-top-border"
					key={daysInMonth.length + 1}
				></div>
			);

		return daysInMonth;
	};

	const renderCalendarRows = () => {
		var totalSlots = getDaysInMonth();
		const rows = [];
		let cells = [];

		const divider = Math.round(totalSlots.length / 2);

		totalSlots.forEach((row, i) => {
			if (i % divider !== 0) {
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

		return rows
			.filter((r) => r.length !== 0)
			.map((d, i) => (
				<div className="d-flex" key={d + i}>
					{d}
				</div>
			));
	};

	return <>{renderCalendarRows()}</>;
};
