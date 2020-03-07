import React, { useState, useContext } from "react";
import {
  Button,
  FormGroup,
  FormControl,
  FormLabel,
  Container
} from "react-bootstrap";
import { login, register } from "../../services/users";
import history from "../history";
import { IUser } from "../../models";
import axios from "../../axios/axios";
import { AppContext } from "../../context";

export const Login: React.FC = () => {
  const [userName, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const appState = useContext(AppContext);

  function validateForm() {
    return userName.length > 0 && password.length > 0;
  }

  const onSubmit = (signIn: boolean) => {
    return signIn ? login : register;
  };

  async function handleSubmit(
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    signIn: boolean
  ) {
    e.preventDefault();
    await onSubmit(signIn)({
      id: 0,
      username: userName,
      password
    })
      .then(res => {
        const user: IUser = res.data;
        localStorage.setItem("user", JSON.stringify(user));
        appState.setAppState({ ...appState, user });
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.token;
        history.push("/");
      })
      .catch(err => console.log(err));
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
              onChange={e => setUsername(e.target.value)}
            />
          </FormGroup>
          <FormGroup controlId="password">
            <FormLabel>Пароль</FormLabel>
            <FormControl
              value={password}
              onChange={e => setPassword(e.target.value)}
              type="password"
            />
          </FormGroup>
          <Button
            onClick={(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) =>
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
            onClick={(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) =>
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
