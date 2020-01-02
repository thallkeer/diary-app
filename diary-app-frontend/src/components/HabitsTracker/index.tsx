import React, { CSSProperties } from "react";
import { getRandomId } from "../../utils";

const onDayClick = (e: React.MouseEvent<HTMLElement>, day: number) => {
  let target = e.target as HTMLElement;
  target.style.backgroundColor = "skyblue";
};

const getDaysInMonth = () => {
  let daysInMonth = [];

  var curDate = new Date();
  let days = new Date(
    curDate.getFullYear(),
    curDate.getMonth() + 1,
    0
  ).getDate();

  let dayStyle: CSSProperties = {
    border: "1px solid black",
    textAlign: "center",
    fontFamily: "RadioVolna",
    fontWeight: "bold",
    flex: "1",
    minWidth: "25px",
    cursor: "pointer"
  };

  for (let d = 1; d <= days; d++) {
    daysInMonth.push(
      <div
        className="p-2"
        key={d}
        style={dayStyle}
        onClick={e => onDayClick(e, d)}
      >
        {d}
      </div>
    );
  }

  if (days === 31)
    daysInMonth.push(
      <div className="p-2" key={getRandomId()} style={dayStyle}></div>
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
    .filter(r => r.length !== 0)
    .map((d, i) => (
      <div className="d-flex" key={d + i}>
        {d}
      </div>
    ));
};

const HabitsTracker = () => {
  return <>{getCalendarRows()}</>;
};

export default HabitsTracker;
