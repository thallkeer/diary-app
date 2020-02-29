import React from "react";

type Props = {
  className?: string;
  value: string;
  handleBlur: (event?: React.FocusEvent<HTMLInputElement>) => void;
  handleChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};

export const ListHeaderInput: React.FC<Props> = ({
  value,
  className,
  handleBlur,
  handleChange
}) => {
  const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") handleBlur();
  };

  let cn = `list-header-input ${className || ""}`;

  return (
    <input
      className={cn}
      type="text"
      maxLength={50}
      value={value}
      onBlur={handleBlur}
      onKeyPress={handleKeyPress}
      onChange={handleChange}
      autoComplete={"off"}
    />
  );
};

export default ListHeaderInput;
