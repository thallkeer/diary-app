import React from "react";
import { useSelector } from "react-redux";
import { IHabitDay, IHabitTracker } from "../../models/entities";
import { getAppInfo } from "../../selectors/app-selectors";
import { HabitDayCell } from "./HabitDayCell";

export const HabitTracker: React.FC<{
	tracker: IHabitTracker;
	updateHabitTracker: (tracker: IHabitTracker) => void;
}> = ({ tracker, updateHabitTracker }) => {
	const { year, month } = useSelector(getAppInfo);

	const onDayClick = (e: React.MouseEvent<HTMLElement>, day: IHabitDay) => {
		let target = e.target as HTMLElement;
		let updatedTracker: IHabitTracker;
		console.log("clicked", tracker);

		if (tracker.items.some((hd) => hd.number === day.number)) {
			target.classList.remove("marked");
			updatedTracker = {
				...tracker,
				items: tracker.items.filter((sd) => sd.number !== day.number),
			};
		} else {
			target.classList.add("marked");
			updatedTracker = {
				...tracker,
				items: [...tracker.items, day],
			};
		}
		updateHabitTracker(updatedTracker);
	};

	const getDaysInMonth = () => {
		let daysInMonth = [];

		const days = new Date(year, month, 0).getDate();

		const daysInFirstRow = Math.round(days / 2);

		for (let d = 1; d <= days; d++) {
			let curDay = tracker.items.find((day) => day.number === d);
			let cn = `p-2 day-cell ${curDay ? "marked" : ""}`;

			if (d !== 1 && d !== daysInFirstRow + 1) cn += " no-left-border";
			if (d >= daysInFirstRow + 1) cn += " no-top-border";

			const dayCell = (
				<div className={cn} key={d}>
					<HabitDayCell
						tracker={tracker}
						updateHabitTracker={updateHabitTracker}
						day={
							curDay || {
								number: d,
								note: "",
								id: 0,
								habitTrackerId: tracker.id,
							}
						}
						isSelected={curDay ? true : false}
						onDayClick={onDayClick}
					/>
				</div>
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
		let rows = [];
		let cells = [];

		let divider = Math.round(totalSlots.length / 2);

		totalSlots.forEach((row, i) => {
			if (i % divider !== 0) {
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
