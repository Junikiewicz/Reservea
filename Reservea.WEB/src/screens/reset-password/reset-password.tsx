import React from "react";
import { Form, Button, Row, Col } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useHistory } from "react-router";
import { toast } from "react-toastify";
import {
  ResetPasswordRequest,
  resetPassworrdRequest
} from "../../api/clients/userClient";
function ResetPassword(props: any): JSX.Element {
  const history = useHistory();
  const { register, handleSubmit } = useForm<ResetPasswordRequest>({
    mode: "onSubmit",
  });

  const onSubmit = async (data: ResetPasswordRequest): Promise<void> => {
    const params = new URLSearchParams(props.location.search);
    const token = params.get("token");
    const id = params.get("id");
    data.token = token!;
    data.userId = Number(id);
    resetPassworrdRequest(data)
      .then(() => {
        toast.success("Pomyślnie zresetowano hasło");
        history.push("/");
      })
      .catch(() => {});
  };

  return (
    <Row className="justify-content-center">
      <Col className="col-5 pageContent">
        <Form onSubmit={handleSubmit(onSubmit)} className="mt-2">
          <Form.Group controlId="formBasicPassword">
            <Form.Label>Nowe hasło</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="password"
              id="newPassword"
              name="newPassword"
              ref={register()}
              placeholder="Hasło"
            />
            <Form.Label>Powtórz nowe hasło</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="password"
              placeholder="Powtórz hasło"
            />
          </Form.Group>
          <Button className="float-right mb-3" variant="success" type="submit">
            Resetuj hasło
          </Button>
        </Form>
      </Col>
    </Row>
  );
}

export default ResetPassword;
