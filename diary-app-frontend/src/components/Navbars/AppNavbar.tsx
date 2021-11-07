import React, { useState } from "react";
import { Navbar, Nav, NavDropdown } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { setUser } from "store/app/appSlice";
import { getAppInfo } from "../../selectors/app-selectors";
import { userService } from "../../services/users";
import { TransferDataForm } from "../Dialogs/TransferDataForm";

export const AppNavbar: React.FC<{ isOnMonthPage: boolean }> = ({
	isOnMonthPage: monthPage,
}) => {
	const dispatch = useDispatch();
	const { user } = useSelector(getAppInfo);
	const [show, setShow] = useState(false);
	const handleClose = () => setShow(false);
	const handleShow = () => setShow(true);

	const onLogoff = () => {
		dispatch(setUser(null));
		userService.logoff();
	};

	return (
		<>
			<Navbar bg="light" collapseOnSelect>
				<Navbar.Brand href="/main">Diary App</Navbar.Brand>
				<Navbar.Toggle />
				<Navbar.Collapse className="justify-content-end">
					{user && (
						<Nav>
							<NavDropdown
								title={user.username}
								id="collasible-nav-dropdown"
								style={{ marginRight: "1rem", fontWeight: "bold" }}
								// alignRight={true}
							>
								{monthPage && (
									<NavDropdown.Item onClick={handleShow}>
										Перенести списки на следующий месяц
									</NavDropdown.Item>
								)}
								<NavDropdown.Item href="/settings">
									Настройки пользователя
								</NavDropdown.Item>
								<NavDropdown.Divider />
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
