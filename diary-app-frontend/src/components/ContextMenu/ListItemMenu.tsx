import React from "react";

import useContextMenu from "../../hooks/useContextMenu";

const Menu = ({ outerRef }) => {
  const { xPos, yPos, menu } = useContextMenu(outerRef);

  if (menu) {
    return (
      <ul className="menu" style={{ top: yPos, left: xPos }}>
        <li>Добавить ссылку</li>
        <li>Удалить</li>
      </ul>
    );
  }
  return <></>;
};

export default Menu;
