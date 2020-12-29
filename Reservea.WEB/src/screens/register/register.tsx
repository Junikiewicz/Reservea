import React from "react";
import { Form, Button, Row, Col } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useHistory } from "react-router";
import { toast } from "react-toastify";
import { signupRequest } from "../../api/clients/userClient";
import { RegisterRequest } from "../../api/dtos/user/auth/registerRequest";

function Register(): JSX.Element {
  const history = useHistory();
  const { register, handleSubmit } = useForm<RegisterRequest>({
    mode: "onSubmit",
  });

  const onSubmit = async (data: RegisterRequest): Promise<void> => {
    signupRequest(data)
      .then(() => {
        toast.success("Pomyślnie utworzono konto");
        history.push("/");
      })
      .catch(() => {});
  };

  return (
    <Row className="justify-content-center">
      <Col className="col-5 pageContent">
        <Form onSubmit={handleSubmit(onSubmit)} className="mt-2">
          <Form.Group controlId="formBasicPersonal">
            <Form.Label>Imię</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="text"
              id="firstName"
              name="firstName"
              ref={register()}
              placeholder="Imię"
            />
            <Form.Label>Nazwisko</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="text"
              id="lastName"
              name="lastName"
              ref={register()}
              placeholder="Nazwisko"
            />
          </Form.Group>
          <Form.Group controlId="formBasicEmail">
            <Form.Label>Email</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="email"
              id="email"
              name="email"
              ref={register()}
              placeholder="Email"
            />
            <Form.Text className="text-muted">
              Nie udostępnimy go nikomu.
            </Form.Text>
          </Form.Group>
          <Form.Group controlId="formBasicPassword">
            <Form.Label>Hasło</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="password"
              id="password"
              name="password"
              ref={register()}
              placeholder="Hasło"
            />
            <Form.Label>Powtórz hasło</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="password"
              placeholder="Powtórz hasło"
            />
          </Form.Group>
          <Button className="float-right mb-3" variant="success" type="submit">
            Zarejestruj
          </Button>
        </Form>
      </Col>
    </Row>
  );
}

export default Register;
