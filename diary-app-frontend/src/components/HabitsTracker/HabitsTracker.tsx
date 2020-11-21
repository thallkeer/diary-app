import React, { useContext } from "react";
// import { IHabitsTracker, HabitDay } from "../../models";
// import { HabitsTrackerContext } from "../MonthPage/GoalsArea";
// import { HabitDayCell } from "./HabitDayCell";
// import { goalsAreaContext } from "../MonthPage/GoalsAreaState";
// import { useSelector } from "react-redux";
// import { getAppInfo } from "../../context/reducers/app-selectors";

// export const HabitsTracker = () => {
// 	const { tracker } = useContext(HabitsTrackerContext);
// 	const { year, month } = useSelector(getAppInfo);
// 	const { addHabitsTracker } = useContext(goalsAreaContext);

// 	const onDayClick = (e: React.MouseEvent<HTMLElement>, day: HabitDay) => {
// 		let target = e.target as HTMLElement;
// 		let updatedTracker: IHabitsTracker;
// 		if (tracker.selectedDays.some((hd) => hd.number === day.number)) {
// 			target.classList.remove("marked");
// 			updatedTracker = {
// 				...tracker,
// 				selectedDays: tracker.selectedDays.filter(
// 					(sd) => sd.number !== day.number
// 				),
// 			};
// 		} else {
// 			target.classList.add("marked");
// 			updatedTracker = {
// 				...tracker,
// 				selectedDays: [...tracker.selectedDays, day],
// 			};
// 		}
// 		addHabitsTracker(updatedTracker);
// 	};

// 	const getDaysInMonth = () => {
// 		let daysInMonth = [];

// 		const days = new Date(year, month, 0).getDate();

// 		const daysInFirstRow = Math.round(days / 2);

// 		for (let d = 1; d <= days; d++) {
// 			let curDay = tracker.selectedDays.find((day) => day.number === d);
// 			let cn = `p-2 day-cell ${curDay ? "marked" : ""}`;

// 			if (d !== 1 && d !== daysInFirstRow + 1) cn += " no-left-border";
// 			if (d >= daysInFirstRow + 1) cn += " no-top-border";

// 			const dayCell = (
// 				<div className={cn} key={d}>
// 					<HabitDayCell
// 						tracker={tracker}
// 						day={curDay || { number: d, note: "" }}
// 						isSelected={curDay ? true : false}
// 						onDayClick={onDayClick}
// 					/>
// 				</div>
// 			);

// 			daysInMonth.push(dayCell);
// 		}

// 		if (days % 2 !== 0)
// 			daysInMonth.push(
// 				<div
// 					className="p-2 day-cell no-left-border no-top-border"
// 					key={daysInMonth.length + 1}
// 				></div>
// 			);

// 		return daysInMonth;
// 	};

// 	const renderCalendarRows = () => {
// 		var totalSlots = getDaysInMonth();
// 		let rows = [];
// 		let cells = [];

// 		let divider = Math.round(totalSlots.length / 2);

// 		totalSlots.forEach((row, i) => {
// 			if (i % divider !== 0) {
// 				cells.push(row);
// 			} else {
// 				let insertRow = cells.slice();
// 				rows.push(insertRow);
// 				cells = [];
// 				cells.push(row);
// 			}
// 			if (i === totalSlots.length - 1) {
// 				let insertRow = cells.slice();
// 				rows.push(insertRow);
// 			}
// 		});

// 		return rows
// 			.filter((r) => r.length !== 0)
// 			.map((d, i) => (
// 				<div className="d-flex" key={d + i}>
// 					{d}
// 				</div>
// 			));
// 	};

// 	return <>{renderCalendarRows()}</>;
// };
