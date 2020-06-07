import React, { FC, useEffect, useRef } from "react";
import { ListItem } from "../../models";

interface ListItemInputPropsBase
  extends React.HTMLAttributes<HTMLInputElement> {
  updateItem?: (item: ListItem) => void;
  item: ListItem;
}

interface ListItemInputProps extends ListItemInputPropsBase {
  getItemText?: (item: ListItem) => string;
  readonly?: boolean;
}

interface useListItemInputProps {
  validateAndUpdate: (text: string) => void;
}

function useListItemInput(props: useListItemInputProps) {
  const { validateAndUpdate } = props;
  const inputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {}, [validateAndUpdate, inputRef]);

  const handleBlur = () => {
    let { value } = inputRef.current as HTMLInputElement;
    if (!value && !value.length) return;

    validateAndUpdate(value);
  };

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.key === "Enter") handleBlur();
  };

  return { inputRef, handleBlur, handleKeyPress };
}

export const UrlInput: FC<ListItemInputPropsBase & { endEdit: () => void }> = ({
  item,
  updateItem,
  endEdit,
}) => {
  const validateAndUpdate = (value: string) => {
    if (value !== item.url) {
      item.url = value;
      updateItem(item);
    }
    endEdit();
  };

  const { inputRef, handleBlur, handleKeyPress } = useListItemInput({
    validateAndUpdate,
  });

  useEffect(() => {
    if (inputRef !== null) inputRef.current.focus();
  }, []);

  return (
    <input
      type="url"
      defaultValue={item.url}
      onKeyPress={handleKeyPress}
      onBlur={handleBlur}
      className="list-item-input"
      autoComplete={"off"}
      ref={inputRef}
    />
  );
};

export const ListItemInput: FC<ListItemInputProps> = ({
  updateItem,
  item,
  getItemText = null,
  readonly = false,
}) => {
  const validateAndUpdate = (value: string) => {
    if (value !== item.subject) {
      item.subject = value;
      updateItem(item);
    }
  };

  const { inputRef, handleBlur, handleKeyPress } = useListItemInput({
    validateAndUpdate,
  });

  let inputValue = getItemText ? getItemText(item) : item.subject;

  const inputControl = (
    <input
      type="text"
      maxLength={200}
      defaultValue={inputValue}
      readOnly={readonly || (getItemText ? true : false)}
      onBlur={handleBlur}
      onKeyPress={handleKeyPress}
      className="list-item-input"
      autoComplete={"off"}
      ref={inputRef}
    />
  );

  if (item.url && item.url.length) {
    const url = item.url.includes("http") ? item.url : `https:/${item.url}`;
    return (
      <a href={url} target="_blank" rel="noopener noreferrer">
        {inputControl}
      </a>
    );
  }
  return inputControl;
};
