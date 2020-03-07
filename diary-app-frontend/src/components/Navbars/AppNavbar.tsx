import React, { useContext, useState } from "react";
import { Navbar, Nav, NavDropdown } from "react-bootstrap";
import { AppContext } from "../../context";
import { logoff } from "../../services/users";
import { TransferDataForm } from "../Dialogs/TransferDataForm";

export const AppNavbar: React.FC<{ monthPage: boolean }> = ({ monthPage }) => {
  const appState = useContext(AppContext);
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const onLogoff = () => {
    appState.setAppState({ ...appState, user: null });
    logoff();
  };

  return (
    <>
      <Navbar bg="light" collapseOnSelect>
        <Navbar.Brand href="/main">Diary App</Navbar.Brand>
        <Navbar.Toggle />
        <Navbar.Collapse className="justify-content-end">
          {appState.user && (
            <Nav>
              <NavDropdown
                title={appState.user.username}
                id="collasible-nav-dropdown"
                alignRight
                style={{ marginRight: "1rem", fontWeight: "bold" }}
              >
                {monthPage && (
                  <>
                    <NavDropdown.Item onClick={handleShow}>
                      Перенести списки на следующий месяц
                    </NavDropdown.Item>
                    <NavDropdown.Divider />
                  </>
                )}
                <NavDropdown.Item onClick={onLogoff}>Выйти</NavDropdown.Item>
              </NavDropdown>
            </Nav>
          )}
        </Navbar.Collapse>
      </Navbar>
      {show && <TransferDataForm show={show} onHide={handleClose} />}
    </>
  );
};
