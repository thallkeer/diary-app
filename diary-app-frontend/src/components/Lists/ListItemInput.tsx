import React, { FC, useState } from "react";
import { ListItem } from "../../models";

type ReadonlyMode = {
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
    readonlyMode
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
    if (
      updateItem &&
      state.item.subject.length &&
      state.item.subject !== item.subject
    ) {
      updateItem(state.item);
    }
  };

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.key === "Enter") handleBlur();
  };

  let inputValue =
    state.readonlyMode && state.readonlyMode.getItemText
      ? state.readonlyMode.getItemText(item)
      : state.item.subject;

  return (
    <input
      type="text"
      value={inputValue}
      readOnly={state.readonlyMode ? true : false}
      onChange={handleTextChange}
      onBlur={handleBlur}
      onKeyPress={handleKeyPress}
      className="list-item-input"
    />
  );
};
