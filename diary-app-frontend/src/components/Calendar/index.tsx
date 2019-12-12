import React, { Component, CSSProperties, useContext } from "react";
import moment, { Moment } from "moment";
import "./style.css";
import { getEventsByDay } from "../../selectors/events";
import { AddEventForm } from "../Dialogs/AddEventForm";
import { IEventsByDay } from "../../models";
import { AppContext } from "../../contexts/app-state";

interface ICalendarState {
  momentContext: Moment;
  today: Moment;
  showMonthPopup: boolean;
  showYearPopup: boolean;
  showYearNav: boolean;
  showAddEventForm: boolean;
  clickedDay?: number;
}

class Calendar extends Component<{}, ICalendarState> {
  state: Readonly<ICalendarState> = {
    today: moment(),
    momentContext: moment(),
    showMonthPopup: false,
    showYearPopup: false,
    showYearNav: false,
    showAddEventForm: false
  };

  static contextType = AppContext;

  get events(): IEventsByDay[] {
    return getEventsByDay(this.context);
  }

  get weekdays(): string[] {
    return moment.weekdays();
  }

  get weekdaysShort(): string[] {
    return moment.weekdaysShort();
  }

  get months(): string[] {
    return moment.months();
  }

  get year(): string {
    return this.state.momentContext.format("Y");
  }

  get month(): string {
    return this.state.momentContext.format("MMMM");
  }
  get daysInMonth(): number {
    return this.state.momentContext.daysInMonth();
  }

  get currentDay(): number {
    return this.state.momentContext.date();
  }

  get firstDayOfMonth(): number {
    let dateContext = this.state.momentContext;
    let firstDay = moment(dateContext)
      .startOf("month")
      .get("day");
    return firstDay;
  }

  setMonth = (month: string) => {
    let monthNumber = this.months.indexOf(month);
    let dateContext = Object.assign({}, this.state.momentContext);
    dateContext = moment(dateContext).set("month", monthNumber);
    this.setState({
      momentContext: dateContext
    });
  };

  nextMonth = () => {
    let dateContext = Object.assign({}, this.state.momentContext);
    dateContext = moment(dateContext).add(1, "month");
    this.setState({
      momentContext: dateContext
    });
  };

  prevMonth = () => {
    let dateContext = Object.assign({}, this.state.momentContext);
    dateContext = moment(dateContext).subtract(1, "month");
    this.setState({
      momentContext: dateContext
    });
  };

  onSelectChange = (e, data) => {
    this.setMonth(data);
  };

  SelectList = props => {
    let popup = props.data.map(data => {
      return (
        <div key={data}>
          <a
            href="#"
            onClick={e => {
              this.onSelectChange(e, data);
            }}
          >
            {data}
          </a>
        </div>
      );
    });

    return <div className="month-popup">{popup}</div>;
  };

  onChangeMonth = (month: string) => {
    this.setState({
      showMonthPopup: !this.state.showMonthPopup
    });
  };

  MonthNav = () => {
    return (
      <span
        className="label-month"
        onClick={() => {
          this.onChangeMonth(this.month);
        }}
      >
        {this.month}
        {this.state.showMonthPopup && <this.SelectList data={this.months} />}
      </span>
    );
  };

  onDayClick = (e: React.MouseEvent<HTMLElement>, day: number) => {
    e.preventDefault();
    this.showModal(day);
  };

  private weekdaysShortRussian: string[] = [
    "Пн",
    "Вт",
    "Ср",
    "Чт",
    "Пт",
    "Сб",
    "Вс"
  ];

  getWeekdays = () => {
    return this.weekdaysShortRussian.map(day => {
      return (
        <td key={day} className="week-day">
          {day}
        </td>
      );
    });
  };

  getEmptySlots = () => {
    let blanks = [];
    let firstDayOfMonth: number = this.firstDayOfMonth;
    if (firstDayOfMonth === 0) firstDayOfMonth = 7;

    for (let i = 0; i < firstDayOfMonth - 1; i++) {
      blanks.push(
        <td key={i * 80} className="empty-slot">
          {""}
        </td>
      );
    }
    return blanks;
  };

  getDaysInMonth = () => {
    let daysInMonth = [];
    for (let d = 1; d <= this.daysInMonth; d++) {
      let className = d === this.currentDay ? "day current-day" : "day";

      var curEvent = this.events.find(ev => ev.day === d);
      let curEventClass = curEvent ? "day-with-event" : "no-events-day";
      daysInMonth.push(
        <td
          key={d}
          className={className}
          onClick={(e: React.MouseEvent<HTMLElement>) => {
            this.onDayClick(e, d);
          }}
        >
          <span className="day-span">{d}</span>
          <div className={curEventClass}>
            {curEvent ? curEvent.event.subject : ""}
          </div>
        </td>
      );
    }
    return daysInMonth;
  };

  getCalendarRows = () => {
    var totalSlots = [...this.getEmptySlots(), ...this.getDaysInMonth()];
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
      return <tr key={i * 100}>{d}</tr>;
    });
  };

  showModal = (day: number) => {
    this.setState({ showAddEventForm: true, clickedDay: day });
  };

  hideModal = () => {
    this.setState({ showAddEventForm: false, clickedDay: null });
  };

  toggle = () => {
    this.setState({ showAddEventForm: !this.state.showAddEventForm });
  };

  getMonthName = () => {
    var date: Date = new Date();
    var stringMonth: string = date.toLocaleString("ru", { month: "long" });
    return stringMonth[0].toUpperCase() + stringMonth.slice(1);
  };

  render() {
    const wrapStyle: CSSProperties = {
      position: "absolute",
      top: "0",
      right: "0",
      bottom: "0",
      left: "0",
      border: "0px"
    };

    return (
      <div style={wrapStyle}>
        <h1 className="text-center calendar-header">{this.getMonthName()}</h1>
        <div className="calendar-container">
          <table className="calendar">
            <tbody>
              <tr>{this.getWeekdays()}</tr>
              {this.getCalendarRows()}
            </tbody>
          </table>
        </div>
        {this.state.showAddEventForm && (
          <AddEventForm
            day={this.state.clickedDay}
            show={this.state.showAddEventForm}
            handleClose={this.toggle}
          />
        )}
      </div>
    );
  }
}

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
