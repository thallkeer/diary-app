import React, { useState, useContext } from "react";
import {
  Button,
  FormGroup,
  FormControl,
  FormLabel,
  Container,
  Navbar
} from "react-bootstrap";
import { login } from "../../services/users";
import { AppContext } from "../../context";
import history from "../history";
import { IUser } from "../../models";
//import { Link } from "react-router-dom";

export const Login = () => {
  const [userName, setUsername] = useState("");
  const [password, setPassword] = useState("");

  function validateForm() {
    return userName.length > 0 && password.length > 0;
  }

  async function handleSubmit(event) {
    event.preventDefault();

    await login({
      id: "",
      userName,
      password
    })
      .then(res => {
        const user: IUser = res.data;
        localStorage.setItem("user", JSON.stringify(user));
        history.push("/");
      })
      .catch(err => console.log(err));
  }

  return (
    <>
      <Navbar bg="light" collapseOnSelect>
        <Navbar.Brand>Diary App</Navbar.Brand>
        <Navbar.Toggle />
      </Navbar>
      <header
        style={{
          width: "90vw",
          margin: "15px 60px",
          padding: "25px",
          borderRadius: "7px",
          backgroundColor: "#F8F8F8"
        }}
      >
        KEK
      </header>
      <Container>
        <div className="login">
          <form onSubmit={handleSubmit}>
            <FormGroup controlId="userName">
              <FormLabel>UserName</FormLabel>
              <FormControl
                autoFocus
                type="text"
                value={userName}
                onChange={e => setUsername(e.target.value)}
              />
            </FormGroup>
            <FormGroup controlId="password">
              <FormLabel>Password</FormLabel>
              <FormControl
                value={password}
                onChange={e => setPassword(e.target.value)}
                type="password"
              />
            </FormGroup>
            <Button block size="lg" disabled={!validateForm()} type="submit">
              Login
            </Button>
          </form>
        </div>
      </Container>
    </>
  );
};
