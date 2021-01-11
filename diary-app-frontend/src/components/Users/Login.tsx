import React, { useState } from "react";
import {
	Button,
	FormGroup,
	FormControl,
	FormLabel,
	Container,
} from "react-bootstrap";
import { authUser } from "../../context/reducers/app-reducer";
import { useDispatch } from "react-redux";

const Login: React.FC = () => {
	const dispatch = useDispatch();
	const [userName, setUsername] = useState("");
	const [password, setPassword] = useState("");

	function validateForm() {
		return userName.length > 0 && password.length > 0;
	}

	async function handleSubmit(
		e: React.MouseEvent<HTMLElement, MouseEvent>,
		signIn: boolean
	) {
		e.preventDefault();
		dispatch(
			authUser(
				{
					id: 0,
					username: userName,
					password,
				},
				signIn
			)
		);
	}

	return (
		<Container>
			<div className="login">
				<form>
					<FormGroup controlId="userName">
						<FormLabel>Логин</FormLabel>
						<FormControl
							autoFocus={true}
							type="text"
							value={userName}
							onChange={(e) => setUsername(e.target.value)}
						/>
					</FormGroup>
					<FormGroup controlId="password">
						<FormLabel>Пароль</FormLabel>
						<FormControl
							value={password}
							onChange={(e) => setPassword(e.target.value)}
							type="password"
						/>
					</FormGroup>
					<Button
						onClick={(e: React.MouseEvent<HTMLElement, MouseEvent>) =>
							handleSubmit(e, true)
						}
						className="btn btn-primary"
						block
						size="lg"
						disabled={!validateForm()}
						type="submit"
					>
						Войти
					</Button>
					<Button
						onClick={(e: React.MouseEvent<HTMLElement, MouseEvent>) =>
							handleSubmit(e, false)
						}
						className="btn btn-success"
						block
						size="lg"
						disabled={!validateForm()}
						type="submit"
					>
						Регистрация
					</Button>
				</form>
			</div>
		</Container>
	);
};

export default Login;
