import React, { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import {
  getAllTextFieldsContentsRequest,
  TextFieldContent,
  updateTextFieldContentsRequest,
} from "../../../api/clients/cmsClients";

function HomePageManagment() {
  const { register, handleSubmit, reset, getValues, formState } = useForm<any>({
    mode: "onSubmit",
  });
  const [textFieldsContents, setTextFieldsContents] = useState<
    Array<TextFieldContent>
  >([]);

  useEffect(() => {
    getAllTextFieldsContentsRequest()
      .then((response: Array<TextFieldContent>) => {
        setTextFieldsContents(response);
      })
      .catch(() => {});
  }, []);

  const onSubmit = async (data: any): Promise<void> => {
    let request = [...textFieldsContents];
    request.find((x) => (x.name === "aboutUs"))!.content = data.aboutUs;
    request.find((x) => (x.name === "contact"))!.content = data.contact;

    updateTextFieldContentsRequest(request)
      .then(() => {
        reset(data);
        setTextFieldsContents(request);
        toast.success("Zmiany zostały zapisane");
      })
      .catch(() => {});
  };
  return (
    <div className="pageContent mt-4">
      <Container>
        <Form onSubmit={handleSubmit(onSubmit)}>
          <Row className="mt-3">
            <Col>
              <Form.Group>
                <h3>O nas</h3>
                <Form.Control
                  id="aboutUs"
                  name="aboutUs"
                  className="bg-dark text-light"
                  as="textarea"
                  ref={register()}
                  defaultValue={
                    textFieldsContents.find((x) => (x.name === "aboutUs"))
                      ?.content
                  }
                  rows={8}
                />
              </Form.Group>
            </Col>
            <Col>
              <Form.Group>
                <h3>Kontakt</h3>
                <Form.Control
                  id="contact"
                  name="contact"
                  className="bg-dark text-light"
                  as="textarea"
                  defaultValue={
                    textFieldsContents.find((x) => (x.name === "contact"))
                      ?.content
                  }
                  ref={register()}
                  rows={8}
                />
              </Form.Group>
            </Col>
          </Row>
          <Row>
            <Button
              variant="success"
              type="submit"
              disabled={!formState.isDirty}
              className="ml-auto mr-3 mb-3"
            >
              Zapisz zmiany
            </Button>
          </Row>
        </Form>
      </Container>
    </div>
  );
}

export default HomePageManagment;
