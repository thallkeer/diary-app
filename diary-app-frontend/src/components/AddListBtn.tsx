import React from "react";

export const AddListBtn: React.FC<{ onClick: () => void }> = ({ onClick }) => {
  return (
    <div
      className="btn mt-10 more-btn"
      style={{ margin: "0px auto", maxHeight: "40px" }}
      onClick={onClick}
    >
      Ещё
    </div>
  );
};
