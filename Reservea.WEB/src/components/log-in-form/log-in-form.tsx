import React, { useState } from "react";
import { FormControl, Button, Form, Dropdown } from "react-bootstrap";
import { loginRequest } from "../../api/clients/userClient";
import { LoginFormData } from "../../common/models/forms/loginFormData";
import {
  getUserToken,
  removeUserToken,
  setUserToken,
} from "../../common/helpers/localStorageHelper";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { LoginResponse } from "../../api/dtos/user/auth/loginResponse";
import { getUserNameFromJWTToken } from "../../common/helpers/jwtTokenHelper";
import { useHistory, withRouter } from "react-router";
function LogInForm(): JSX.Element {
  const history = useHistory();
  const [loggedIn, setLoggedIn] = useState(getUserToken() != null);
  const [userName, setUserName] = useState(
    loggedIn ? getUserNameFromJWTToken() : null
  );

  const { register, handleSubmit } = useForm<LoginFormData>({
    mode: "onSubmit",
  });

  const onSubmit = async (data: LoginFormData): Promise<void> => {
    loginRequest(data)
      .then((response: LoginResponse) => {
        toast.success("Pomyślnie zalogowano");
        setUserToken(response.jwtToken);
        setLoggedIn(true);
        setUserName(getUserNameFromJWTToken());
        window.location.reload(false);
      })
      .catch(() => {});
  };

  const logOut = () => {
    removeUserToken();
    setLoggedIn(false);
    history.push("/");
    toast.info("Wylogowano");
  };

  if (!loggedIn) {
    return (
      <Form inline onSubmit={handleSubmit(onSubmit)}>
        <FormControl
          id="email"
          name="email"
          type="text"
          placeholder="Email"
          ref={register}
          className="mr-sm-2 bg-dark text-light"
        />
        <FormControl
          id="password"
          name="password"
          type="password"
          placeholder="Hasło"
          ref={register}
          className="mr-sm-2 bg-dark text-light"
        />
        <Button type="submit" variant="outline-secondary text-light">
          Zaloguj
        </Button>
        <Button
          href="/register"
          variant="outline-secondary text-light"
          className="ml-1"
        >
          Załóż konto
        </Button>
      </Form>
    );
  } else {
    return (
      <Dropdown>
        <Dropdown.Toggle as="span" style={{ cursor: "pointer" }}>
          <span>Witaj {userName}!</span>
        </Dropdown.Toggle>
        <Dropdown.Menu align="right" className="mt-3">
          <Dropdown.Item tag="a" href="/user-reservations">
            Twoje rezerwacje
          </Dropdown.Item>
          <Dropdown.Item as="button">Edytuj konto</Dropdown.Item>
          <Dropdown.Divider />
          <Dropdown.Item as="button" onClick={logOut}>
            Wyloguj się
          </Dropdown.Item>
        </Dropdown.Menu>
      </Dropdown>
    );
  }
}
export default withRouter(LogInForm);
