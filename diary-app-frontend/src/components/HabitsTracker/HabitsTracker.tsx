import React, { useContext } from "react";
import { IHabitsTracker, HabitDay } from "../../models";
import { HabitsTrackerContext } from "../MonthPage/GoalsArea";
import { GoalsAreaContext } from "../../context";
import { HabitDayCell } from "./HabitDayCell";

export const HabitsTracker = () => {
  const { tracker } = useContext(HabitsTrackerContext);
  const { addOrUpdate } = useContext(GoalsAreaContext);

  const onDayClick = (e: React.MouseEvent<HTMLElement>, day: HabitDay) => {
    let target = e.target as HTMLElement;
    let updatedTracker: IHabitsTracker;
    if (tracker.selectedDays.some((hd) => hd.number === day.number)) {
      target.classList.remove("marked");
      updatedTracker = {
        ...tracker,
        selectedDays: tracker.selectedDays.filter(
          (sd) => sd.number !== day.number
        ),
      };
    } else {
      target.classList.add("marked");
      updatedTracker = {
        ...tracker,
        selectedDays: [...tracker.selectedDays, day],
      };
    }
    addOrUpdate(updatedTracker);
  };

  const getDaysInMonth = () => {
    let daysInMonth = [];

    var curDate = new Date();
    let days = new Date(
      curDate.getFullYear(),
      curDate.getMonth() + 1,
      0
    ).getDate();

    for (let d = 1; d <= days; d++) {
      let curDay = tracker.selectedDays.find((day) => day.number === d);
      let cn = `p-2 day-cell ${curDay ? "marked" : ""}`;

      if (d !== 1 && d !== 16) cn += " no-left-border";
      if (d >= 16) cn += " no-top-border";

      const dayCell = (
        <div className={cn} key={d}>
          <HabitDayCell
            tracker={tracker}
            day={curDay || { number: d, note: "" }}
            isSelected={curDay ? true : false}
            onDayClick={onDayClick}
          />
        </div>
      );
      daysInMonth.push(dayCell);
    }

    if (days % 2 !== 0)
      daysInMonth.push(
        <div className="p-2 day-cell" key={daysInMonth.length + 1}></div>
      );

    return daysInMonth;
  };

  const getCalendarRows = () => {
    var totalSlots = getDaysInMonth();
    let rows = [];
    let cells = [];

    let divider = Math.trunc(totalSlots.length / 2);

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

  return <>{getCalendarRows()}</>;
};
