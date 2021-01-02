import React from "react";
import { Form, Button, Row, Col } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useHistory } from "react-router";
import { toast } from "react-toastify";
import {
  ResetPasswordRequest,
  sendResertPasswordEmailRequest,
} from "../../api/clients/userClient";
function CreateResetPassword(props: any): JSX.Element {
  const history = useHistory();
  const { register, handleSubmit } = useForm<ResetPasswordRequest>({
    mode: "onSubmit",
  });

  const onSubmit = async (data: any): Promise<void> => {
    sendResertPasswordEmailRequest(data.email)
      .then(() => {
        toast.success("Wysłano prośbę o zresetowanie hasła");
        history.push("/");
      })
      .catch(() => {});
  };

  return (
    <Row className="justify-content-center">
      <Col className="col-5 pageContent">
        <Form onSubmit={handleSubmit(onSubmit)} className="mt-2">
          <Form.Group controlId="formBasicPassword">
            <Form.Label>Email</Form.Label>
            <Form.Control
              className="bg-dark text-light"
              type="email"
              id="email"
              name="email"
              ref={register()}
              placeholder="Email"
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

export default CreateResetPassword;
