import { useAppDispatch } from "hooks/hooks";
import React, { useState } from "react";
import {
	Button,
	FormGroup,
	FormControl,
	FormLabel,
	Container,
} from "react-bootstrap";
import { authUser } from "store/app/appSlice";

const Login: React.FC = () => {
	const dispatch = useAppDispatch();
	const [userName, setUsername] = useState("");
	const [password, setPassword] = useState("");

	const validateForm = () => userName.length > 0 && password.length > 0;

	async function handleSubmit(
		e: React.MouseEvent<HTMLElement, MouseEvent>,
		signIn: boolean
	) {
		e.preventDefault();
		dispatch(
			authUser(
				{
					username: userName,
					password,
				},
				signIn
			)
		);
	}

	const SignInBtn: React.FC<{ text: string; isSignIn: boolean }> = ({
		text,
		isSignIn,
	}) => {
		return (
			<Button
				className={`btn ${isSignIn ? "btn-primary" : "btn-success"}`}
				size="lg"
				disabled={!validateForm()}
				type="submit"
				onClick={(e: React.MouseEvent<HTMLElement, MouseEvent>) =>
					handleSubmit(e, isSignIn)
				}
			>
				{text}
			</Button>
		);
	};

	return (
		<Container>
			<div className="login">
				<form>
					<FormGroup controlId="userName">
						<FormLabel>Логин</FormLabel>
						<FormControl
							className="login-control"
							autoFocus={true}
							type="text"
							value={userName}
							onChange={(e) => setUsername(e.target.value)}
						/>
					</FormGroup>
					<FormGroup controlId="password">
						<FormLabel style={{ marginTop: "0.5rem" }}>Пароль</FormLabel>
						<FormControl
							className="login-control"
							value={password}
							onChange={(e) => setPassword(e.target.value)}
							type="password"
						/>
					</FormGroup>
					<div className="d-grid gap-2 mt-3">
						<SignInBtn text="Войти" isSignIn={true} />
						<SignInBtn text="Регистрация" isSignIn={false} />
					</div>
				</form>
			</div>
		</Container>
	);
};

export default Login;
