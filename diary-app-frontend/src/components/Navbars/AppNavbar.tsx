import React, { useContext, useState } from "react";
import { Navbar, Nav, NavDropdown } from "react-bootstrap";
import { logoff } from "../../services/users";
import { TransferDataForm } from "../Dialogs/TransferDataForm";
import { store } from "../../context/store";
import { Actions as appActions } from "../../context/actions/app-actions";

export const AppNavbar: React.FC<{ isOnMonthPage: boolean }> = ({
  isOnMonthPage: monthPage,
}) => {
  const { state, dispatch } = useContext(store);
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const onLogoff = () => {
    dispatch(appActions.setState({ ...state, user: null }));
    logoff();
  };

  return (
    <>
      <Navbar bg="light" collapseOnSelect>
        <Navbar.Brand href="/main">Diary App</Navbar.Brand>
        <Navbar.Toggle />
        <Navbar.Collapse className="justify-content-end">
          {state.user && (
            <Nav>
              <NavDropdown
                title={state.user.username}
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
