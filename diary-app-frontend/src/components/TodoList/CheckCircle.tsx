import React from "react";
import { Thunks as todoThunks } from "../../actions/todo-actions";
import { connect } from "react-redux";
import { DispatchThunk } from "../../reducers";

interface IProps {
  id: number;
  done: boolean;
  onTodoClicked?: (todoId: number) => void;
}

interface IState {}

class CheckCircle extends React.Component<IProps, IState> {
  render() {
    const { id, done, onTodoClicked } = this.props;

    return (
      <span
        onClick={() => onTodoClicked(id)}
        style={{
          height: "25px",
          width: "25px",
          backgroundColor: done ? "lightblue" : "white",
          borderRadius: "50%",
          border: "1px solid black",
          display: "inline-block",
          position: "absolute"
        }}
      />
    );
  }
}

const mapDispatchToProps = (dispatch: DispatchThunk) => ({
  onTodoClicked: (todoId: number) => dispatch(todoThunks.toggleTodo(todoId))
});

const ConnectedCheckCircle = connect(
  null,
  mapDispatchToProps
)(CheckCircle);

export default ConnectedCheckCircle;
