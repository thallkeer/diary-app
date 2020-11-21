import React, { useState } from "react";
import {
	Button,
	FormGroup,
	FormControl,
	FormLabel,
	Container,
} from "react-bootstrap";
import { login, register } from "../../services/users";
import history from "../history";
import { IUser } from "../../models";
import axios from "../../axios/axios";
import { AxiosError } from "axios";
import { setUser } from "../../context/reducers/app-reducer";
import { useDispatch } from "react-redux";

type LoginError = {
	isError: boolean;
	errorMessage: string;
};

const Login: React.FC = () => {
	const [error, setError] = useState<LoginError>({
		isError: false,
		errorMessage: "",
	});
	const dispatch = useDispatch();
	const [userName, setUsername] = useState("");
	const [password, setPassword] = useState("");

	function validateForm() {
		return userName.length > 0 && password.length > 0;
	}

	const onSubmit = (signIn: boolean) => {
		return signIn ? login : register;
	};

	const errMsg = () => {
		if (error.isError)
			return (
				<div
					className="alert alert-danger"
					style={{ marginTop: "1em" }}
					role="alert"
				>
					{error.errorMessage}
				</div>
			);
		return <></>;
	};

	async function handleSubmit(
		e: React.MouseEvent<HTMLElement, MouseEvent>,
		signIn: boolean
	) {
		e.preventDefault();
		await onSubmit(signIn)({
			id: 0,
			username: userName,
			password,
		})
			.then((res) => {
				const user: IUser = res.data;
				dispatch(setUser(user));
				axios.defaults.headers.common["Authorization"] = "Bearer " + user.token;
				history.push("/");
			})
			.catch((err: AxiosError) => {
				setError({
					isError: true,
					errorMessage: err.response
						? err.response.data
						: "Unexpected error " + err.name,
				});
			});
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
				{errMsg()}
			</div>
		</Container>
	);
};

export default Login;
