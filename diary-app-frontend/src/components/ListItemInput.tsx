import React, { Component } from "react";
import { DispatchThunk } from "../reducers";
import { Thunks as todoThunks } from "../actions/todo-actions";
import { connect } from "react-redux";

interface IProps {
  itemId: number;
  itemText: string;
  updateItem: (itemId: number, itemText: string) => void;
  readonly?: boolean;
}

interface IState {
  text: string;
}

class ListItemInput extends Component<IProps, IState> {
  constructor(props: IProps) {
    super(props);
    this.state = {
      text: props.itemText
    };
  }

  public static defaultProps = {
    readonly: false
  };

  public handleTextChange = ev => {
    this.setState({
      text: ev.target.value
    });
  };

  public handleBlur = () => {
    const { itemText, itemId, updateItem } = this.props;
    if (itemText !== this.state.text && updateItem)
      updateItem(itemId, this.state.text);
  };

  render() {
    return (
      <input
        type="text"
        value={this.state.text}
        readOnly={this.props.readonly}
        onChange={this.handleTextChange}
        onBlur={this.handleBlur}
        style={{
          cursor: "pointer",
          outline: "none",
          border: "none",
          fontSize: "18px",
          fontFamily: "AmaticSC-Regular",
          fontWeight: "bold",
          paddingLeft: "5px",
          borderBottom: "1px solid black",
          width: "100%"
        }}
      />
    );
  }
}

const mapDispatchToProps = (dispatch: DispatchThunk) => ({
  updateItem: (itemId: number, itemText: string) =>
    dispatch(todoThunks.updateTodo(itemId, itemText))
});

export default connect(
  null,
  mapDispatchToProps
)(ListItemInput);
