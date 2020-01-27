import React from "react";

type Props = {
  value: string;
  handleBlur: (event?: React.FocusEvent<HTMLInputElement>) => void;
  handleChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};

export const ListHeaderInput: React.FC<Props> = ({
  value,
  handleBlur,
  handleChange
}) => {
  const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") handleBlur();
  };

  return (
    <input
      className="list-header-input"
      type="text"
      maxLength={50}
      value={value}
      onBlur={handleBlur}
      onKeyPress={handleKeyPress}
      onChange={handleChange}
    />
  );
};

export default ListHeaderInput;
