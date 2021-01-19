import React, { useState } from "react";
import { Navbar, Nav, NavDropdown } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { AppThunks } from "store/app/app.actions";
import { getAppInfo } from "../../selectors/app-selectors";
import { usersService } from "../../services/users";
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
		dispatch(AppThunks.setUser(null));
		usersService.logoff();
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
