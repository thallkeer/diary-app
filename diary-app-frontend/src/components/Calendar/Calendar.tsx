import React, { useState } from "react";
//import moment, { Moment } from "moment";
import { getEventsByDay } from "../../selectors";
import { AddEventForm } from "../Dialogs/AddEventForm";
import { IEvent } from "../../models";
import { Thunks as eventThunks } from "../../actions/events-actions";
import { IEventListContext } from "../../context";

interface ICalendarState {
  // momentContext: Moment;
  //today: Moment;
  showMonthPopup: boolean;
  showYearPopup: boolean;
  showYearNav: boolean;
  showAddEventForm: boolean;
  clickedDay?: number;
}

interface ICalendarProps {
  eventsState: IEventListContext;
}

export const Calendar: React.FC<ICalendarProps> = ({ eventsState }) => {
  const [state, setState] = useState<ICalendarState>({
    // today: moment(),
    // momentContext: moment(),
    showMonthPopup: false,
    showYearPopup: false,
    showYearNav: false,
    showAddEventForm: false
  });

  // const weekdays = (): string[] => moment.weekdays();
  // const weekdaysShort = (): string[] => moment.weekdaysShort();
  // const months = (): string[] => moment.months();
  // const year = (): string => state.momentContext.format("Y");
  // const month = (): string => state.momentContext.format("MMMM");

  const getDaysInMonth = (): number => {
    var curDate = new Date();
    return new Date(curDate.getFullYear(), curDate.getMonth() + 1, 0).getDate();
  };

  const currentDay = (): number => new Date().getDate();

  const getFirstDayOfMonth = (): number => {
    let curDate = new Date();
    let fDay = new Date(curDate.getFullYear(), curDate.getMonth(), 1).getDay();
    return fDay === 0 ? 7 : fDay;
  };

  const addEvent = (newEvent: IEvent) => {
    eventsState.dispatch(eventThunks.addEvent(newEvent));
  };

  const onDayClick = (e: React.MouseEvent<HTMLElement>, day: number) => {
    e.preventDefault();
    showModal(day);
  };

  const weekdaysShortRussian: string[] = [
    "Пн",
    "Вт",
    "Ср",
    "Чт",
    "Пт",
    "Сб",
    "Вс"
  ];

  const getWeekdays = () => {
    return weekdaysShortRussian.map(day => (
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

    const events = getEventsByDay(eventsState).filter(e => e.event.id !== 0);

    for (let d = 1; d <= getDaysInMonth(); d++) {
      let className = d === currentDay() ? "day current-day" : "day";

      let curEvents = events.filter(ev => ev.day === d);

      let curEventClass = curEvents.length ? "day-with-event" : "no-events-day";

      daysInMonth.push(
        <td key={d} className={className} onClick={e => onDayClick(e, d)}>
          <div className="day-span">{d}</div>
          {curEvents.map(eventByDay => (
            <div
              key={eventByDay.event.id}
              className={`mt-5 ${curEventClass}`}
              onClick={e => onDayClick(e, d)}
            >
              {eventByDay.event.subject}
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

  const showModal = (day: number) => {
    setState({ ...state, showAddEventForm: true, clickedDay: day });
  };

  const hideModal = () => {
    setState({ ...state, showAddEventForm: false, clickedDay: null });
  };

  const toggle = () => {
    setState({ ...state, showAddEventForm: !state.showAddEventForm });
  };

  const getMonthName = (): string => {
    let date = new Date();
    let stringMonth = date.toLocaleString("ru", { month: "long" });
    return stringMonth[0].toUpperCase() + stringMonth.slice(1);
  };

  return (
    <div className="calendar-wrapper">
      <h1 className="text-center calendar-header">{getMonthName()}</h1>
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
          eventInfo={{
            ownerID: eventsState.list.id,
            addEvent: addEvent
          }}
        />
      )}
    </div>
  );
};

// const setMonth = (month: string) => {
//   let monthNumber = this.months.indexOf(month);
//   let dateContext = Object.assign({}, this.state.momentContext);
//   dateContext = moment(dateContext).set("month", monthNumber);
//   this.setState({
//     momentContext: dateContext
//   });
// };

// nextMonth = () => {
//   let dateContext = Object.assign({}, this.state.momentContext);
//   dateContext = moment(dateContext).add(1, "month");
//   this.setState({
//     momentContext: dateContext
//   });
// };

// prevMonth = () => {
//   let dateContext = Object.assign({}, this.state.momentContext);
//   dateContext = moment(dateContext).subtract(1, "month");
//   this.setState({
//     momentContext: dateContext
//   });
// };

// SelectList = ({ months }) => {
//   let popup = months.map(month => {
//     return (
//       <div key={month}>
//         <a href="#" onClick={e => this.setMonth(month)}>
//           {month}
//         </a>
//       </div>
//     );
//   });

//   return <div className="month-popup">{popup}</div>;
// };

// onChangeMonth = (month: string) => {
//   this.setState({
//     showMonthPopup: !this.state.showMonthPopup
//   });
// };

// MonthNav = () => {
//   return (
//     <span
//       className="label-month"
//       onClick={() => {
//         this.onChangeMonth(this.month);
//       }}
//     >
//       {this.month}
//       {this.state.showMonthPopup && <this.SelectList months={this.months} />}
//     </span>
//   );
// };

{
  /* <thead>
              <tr className="calendar-header">
                <td colSpan={5}>
                  <this.MonthNav /> <this.YearNav />
                </td>
                <td colSpan={2} className="nav-month">
                  <i
                    className="prev fa fa-fw fa-chevron-left"
                    onClick={e => {
                      this.prevMonth();
                    }}
                  />
                  <i
                    className="prev fa fa-fw fa-chevron-right"
                    onClick={e => {
                      this.nextMonth();
                    }}
                  />
                </td>
              </tr>
            </thead> */
}

export default Calendar;
