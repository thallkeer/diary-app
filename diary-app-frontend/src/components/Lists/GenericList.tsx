/*@typescript-eslint/no-unused-vars*/
import React, { useState } from "react";
import { ListItemInput } from "./ListItemInput";
import { IList, ListItem } from "../../models";
import { useFillToNumber } from "../../hooks/useFillToNumber";
import { DeleteBtn } from "./DeleteBtn";
import ListHeaderInput from "./ListHeaderInput";

interface IProps<T extends IList<ListItem>> {
  readonly?: boolean;
  className?: string;
  fillToNumber?: number;
  getEmptyItem: () => ListItem;
  getItemText: (item: ListItem) => string;
  onDelete: (itemID: number) => void;
  onUpdate: (item: ListItem) => void;
  onTitleChange: (list: T) => void;
  list: T;
}

export default function GenericList<T extends IList<ListItem>>(
  props: IProps<T>
) {
  const {
    readonly = false,
    className,
    fillToNumber,
    getEmptyItem,
    getItemText,
    list,
    onDelete,
    onUpdate,
    onTitleChange
  } = props;

  const [listTitle, setListTitle] = useState(list.title);

  const items = useFillToNumber([...list.items], fillToNumber, getEmptyItem);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    setListTitle(e.target.value);

  const handleBlur = (e: React.FocusEvent<HTMLInputElement>) =>
    onTitleChange(list);

  return (
    <div style={{ marginTop: "40px" }} className={className}>
      <h1 className="todo-list-header">
        <ListHeaderInput
          value={listTitle}
          handleChange={handleChange}
          handleBlur={handleBlur}
        />
      </h1>
      <ul className="todos">
        {items.map((item: ListItem, i) => (
          <li key={item.id !== 0 ? item.id : i + 80} className="list-item">
            <ListItemInput
              item={item}
              updateItem={onUpdate}
              readonlyMode={readonly ? { getItemText } : null}
            />
            {item.id !== 0 && <DeleteBtn onDelete={() => onDelete(item.id)} />}
          </li>
        ))}
      </ul>
    </div>
  );
}
