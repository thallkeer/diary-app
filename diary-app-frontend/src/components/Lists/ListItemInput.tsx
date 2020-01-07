import React, { FC, useState } from "react";
import { ITodo, IEvent } from "../../models";

type ListItem = ITodo | IEvent;

type ReadonlyMode = {
  readonly: boolean;
  getItemText: (item: ListItem) => string;
};

interface IProps {
  updateItem?: (item: ListItem) => void;
  item: ListItem;
  readonlyMode?: ReadonlyMode;
}

export const ListItemInput: FC<IProps> = ({
  updateItem,
  item,
  readonlyMode
}) => {
  const initialState: IProps = {
    updateItem,
    item,
    readonlyMode: readonlyMode || {
      readonly: false,
      getItemText: null
    }
  };

  const [state, setState] = useState<IProps | null>(initialState);

  const handleTextChange = ev => {
    setState({
      ...state,
      item: {
        ...item,
        subject: ev.target.value
      }
    });
  };

  const handleBlur = () => {
    if (state.item.subject.length && state.item.subject !== item.subject) {
      updateItem(state.item);
      setState(initialState);
    }
  };

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.key === "Enter") handleBlur();
  };

  let inputValue = state.readonlyMode.getItemText
    ? state.readonlyMode.getItemText(item)
    : state.item.subject;

  return (
    <input
      type="text"
      value={inputValue}
      readOnly={state.readonlyMode.readonly}
      onChange={handleTextChange}
      onBlur={handleBlur}
      onKeyPress={handleKeyPress}
      className="list-item-input"
    />
  );
};
