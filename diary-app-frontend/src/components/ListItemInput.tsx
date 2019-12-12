import React, { FC, useState } from "react";
import { ITodoItem } from "../models/index";

interface IProps {
  itemId: number;
  itemText: string;
  readonly?: boolean;
  updateItem: (itemId: number, itemText: string) => void;
}

export const ListItemInput: FC<IProps> = ({
  itemId,
  itemText,
  readonly,
  updateItem
}) => {
  const [state, setState] = useState<IProps | null>({
    itemId,
    itemText,
    readonly: readonly || false,
    updateItem
  });

  const handleTextChange = ev => {
    setState({
      ...state,
      itemText: ev.target.value
    });
  };

  const handleBlur = () => {
    if (itemText !== state.itemText) updateItem(state.itemId, state.itemText);
  };

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.key === "Enter") handleBlur();
  };

  return (
    <input
      type="text"
      value={state.itemText}
      readOnly={state.readonly}
      onChange={handleTextChange}
      onBlur={handleBlur}
      onKeyPress={handleKeyPress}
      className="list-item-input"
    />
  );
};
