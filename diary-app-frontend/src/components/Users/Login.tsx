import React, { useState } from "react";
import {
	Button,
	FormGroup,
	FormControl,
	FormLabel,
	Container,
} from "react-bootstrap";
import { useDispatch } from "react-redux";
import { AppThunks } from "store/app/app.actions";

const Login: React.FC = () => {
	const dispatch = useDispatch();
	const [userName, setUsername] = useState("");
	const [password, setPassword] = useState("");

	const validateForm = () => userName.length > 0 && password.length > 0;

	async function handleSubmit(
		e: React.MouseEvent<HTMLElement, MouseEvent>,
		signIn: boolean
	) {
		e.preventDefault();
		dispatch(
			AppThunks.authUser(
				{
					id: 0,
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
				block
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
					<SignInBtn text="Войти" isSignIn={true} />
					<SignInBtn text="Регистрация" isSignIn={false} />
				</form>
			</div>
		</Container>
	);
};

export default Login;
