import React, { useRef } from "react";

type Props = {
  className?: string;
  value: string;
  handleBlur: (title: string) => void;
};

export const ListHeaderInput: React.FC<Props> = ({
  value,
  className,
  handleBlur
}) => {
  const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") handleBlur(titleInput.current.value);
  };

  const titleInput = useRef(null);

  let cn = `list-header-input ${className || ""}`;

  return (
    <input
      ref={titleInput}
      defaultValue={value}
      className={cn}
      type="text"
      maxLength={50}
      onBlur={() => handleBlur(titleInput.current.value)}
      onKeyPress={handleKeyPress}
      autoComplete={"off"}
    />
  );
};

export default ListHeaderInput;
