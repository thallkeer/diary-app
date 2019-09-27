import React, { Component } from "react";
import ITodoItem from "../../models/todo-model";
import { connect } from "react-redux";
import {
  getTodos,
  getTodosLoading,
  getTodosLoaded
} from "../../selectors/todos";
import { IAppState, DispatchThunk } from "../../reducers";
import { Thunks as todoThunks } from "../../actions/todo-actions";
import TodoInput from "./TodoInput";
import "./style.css";

interface IProps {
  header: string;
  todos?: ITodoItem[];
  loading: boolean;
  loaded: boolean;
  loadTodos: () => void;
}

interface IState {}

class TodoList extends Component<IProps, IState> {
  componentDidMount() {
    const { loadTodos, loading, loaded } = this.props;
    if (!loaded && !loading) loadTodos();
  }

  render() {
    const { todos, header, loading } = this.props;

    return (
      <div style={{ marginTop: "52px" }}>
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
            {todos.map(todo => (
              <li key={todo.id}>{TodoInput(todo)}</li>
            ))}
          </ul>
        )}
      </div>
    );
  }
}

const mapStateToProps = (state: IAppState) => ({
  todos: getTodos(state),
  loading: getTodosLoading(state),
  loaded: getTodosLoaded(state)
});

const mapDispatchToProps = (dispatch: DispatchThunk) => ({
  loadTodos: () => dispatch(todoThunks.loadTodos())
});

export default connect<any, any, any>(
  mapStateToProps,
  mapDispatchToProps,
  null,
  { pure: false }
)(TodoList);
