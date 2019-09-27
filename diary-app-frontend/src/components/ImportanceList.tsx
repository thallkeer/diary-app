import React, { Component } from "react";
import ListItemInput from "./ListItemInput";
import ILightEvent from "../models/event-light-model";
import { connect } from "react-redux";
import {
  getEvents,
  getEventsLoading,
  getEventsLoaded
} from "../selectors/events";
import { IAppState, DispatchThunk } from "../reducers";
import { Thunks as eventThunks } from "../actions/events-actions";

interface IProps {
  header: string;
  events?: ILightEvent[];
  loading: boolean;
  loaded: boolean;
  loadEvents: () => void;
}

interface IState {}

class ImportanceList extends Component<IProps, IState> {
  public static defaultProps: Partial<IProps> = {
    header: "",
    events: []
  };

  componentDidMount() {
    const { loadEvents, loading, loaded } = this.props;
    if (!loaded && !loading) loadEvents();
  }

  public getListItems = () => {
    const { events } = this.props;
    return events.map(event => (
      <li key={event.eventID}>
        <ListItemInput
          itemId={event.eventID}
          itemText={
            event.date.toLocaleString("ru", {
              day: "numeric",
              month: "numeric"
            }) +
            " " +
            event.subject
          }
          readonly={true}
        />
      </li>
    ));
  };

  render() {
    const { header, loading } = this.props;

    return (
      <div>
        <h1 className="todo-list-header">{header}</h1>
        {loading ? (
          <div>
            <h2>Loading...</h2>
          </div>
        ) : (
          <ul
            style={{
              listStyleType: "none",
              padding: "0",
              margin: "0",
              textAlign: "left"
            }}
          >
            {this.getListItems()}
          </ul>
        )}
      </div>
    );
  }
}

const mapStateToProps = (state: IAppState) => ({
  events: getEvents(state),
  loading: getEventsLoading(state),
  loaded: getEventsLoaded(state)
});

const mapDispatchToProps = (dispatch: DispatchThunk) => ({
  loadEvents: () => dispatch(eventThunks.loadEvents())
});

export default connect<any, any, any>(
  mapStateToProps,
  mapDispatchToProps,
  null,
  { pure: false }
)(ImportanceList);
