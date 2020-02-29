import React, { FC, useState } from "react";
import { ListItem } from "../../models";

interface IProps {
  updateItem?: (item: ListItem) => void;
  item: ListItem;
  getItemText?: (item: ListItem) => string;
  canEditUrl?: boolean;
  readonly?: boolean;
  style?: React.CSSProperties;
}

interface InputState {
  editUrlMode?: boolean;
  itemText: string;
  url: string;
}

export const ListItemInput: FC<IProps> = ({
  updateItem,
  item,
  style,
  getItemText = null,
  canEditUrl = false,
  readonly = false
}) => {
  const initialState: InputState = {
    itemText: item.subject,
    url: item.url || "",
    editUrlMode: false
  };

  const [state, setState] = useState<InputState>(initialState);

  const handleTextChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setState({
      ...state,
      [e.target.name]: e.target.value
    });
  };

  const handleBlur = () => {
    if (!updateItem) return;

    if (state.editUrlMode) {
      if (state.url.length && state.url !== item.url) {
        item.url = state.url;
        updateItem(item);
      }
      setState({ ...state, editUrlMode: false });
    } else if (state.itemText.length && state.itemText !== item.subject) {
      item.subject = state.itemText;
      updateItem(item);
    }
  };

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.key === "Enter") handleBlur();
  };

  const handleDoubleClick = (
    event: React.MouseEvent<HTMLInputElement, MouseEvent>
  ) => {
    if (!canEditUrl || readonly) return;
    setState({
      ...state,
      editUrlMode: true
    });
  };

  if (state.editUrlMode) {
    return (
      <input
        type="url"
        name="url"
        value={state.url}
        onChange={handleTextChange}
        onBlur={handleBlur}
        onKeyPress={handleKeyPress}
        className="list-item-input"
        style={style}
        autoComplete={"off"}
      />
    );
  }

  let inputValue = getItemText ? getItemText(item) : state.itemText;

  return (
    <input
      type="text"
      name="itemText"
      maxLength={200}
      value={inputValue}
      readOnly={readonly || (getItemText ? true : false)}
      onChange={handleTextChange}
      onBlur={handleBlur}
      onKeyPress={handleKeyPress}
      onDoubleClick={handleDoubleClick}
      className="list-item-input"
      style={style}
      autoComplete={"off"}
    />
  );
};
