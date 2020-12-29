import React, { useEffect, useState } from "react";
import {
  Button,
  Row,
  Tab,
  Nav,
  Col,
  Container,
  Form,
  Table,
} from "react-bootstrap";
import { useFieldArray, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLongArrowAltLeft } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import {
  RoleResponse,
  UserDetailsResponse,
} from "../../api/dtos/user/auth/userDetailsResponse";
import {
  getAllRolesRequest,
  getUserDetailsRequest,
  updateUserRequest,
} from "../../api/clients/userClient";
import { getRoleName } from "../../common/enums/role";

function EditAccount(props: any) {
  const [accountDetails, setAccountDetails] = useState<any>();

  const {
    control,
    register,
    handleSubmit,
    reset,
    getValues,
    formState,
  } = useForm<any>({
    mode: "onSubmit",
  });

  const { fields, remove, insert } = useFieldArray<any, "customId">({
    control,
    name: "roles",
    keyName: "customId",
  });

  useEffect(() => {
    getAllRolesRequest().then((allRoles: Array<RoleResponse>) => {
      getUserDetailsRequest(props.match.params.id)
        .then((accountDetailsResponse: UserDetailsResponse) => {
          let roles = allRoles.map((x) => ({
            id: x.id,
            isChecked: accountDetailsResponse.roles.some((y) => y.id == x.id),
            name: x.name,
          }));
          accountDetailsResponse.roles = roles;
          reset({
            roles: accountDetailsResponse.roles,
          });
          setAccountDetails(accountDetailsResponse);
        })
        .catch(() => {});
    });
  }, []);

  const onSubmit = async (data: UserDetailsResponse): Promise<void> => {
    data.roles = data.roles.map((x, index) => ({
      name: fields[index].name,
      id: fields[index].id,
      isChecked: x.isChecked,
    }));
    let request = JSON.parse(JSON.stringify(data));

    request.roles = request.roles.filter((x: any) => x.isChecked);
    request.roles = request.roles.map((x: any) => x.name);

    updateUserRequest(props.match.params.id, request)
      .then(() => {
        reset(data);
        toast.success("Pomyślnie zapisano zmiany");
      })
      .catch(() => {});
  };

  return (
    <div>
      <Tab.Container defaultActiveKey="general">
        <div className="pageHeader">
          <Container>
            <Row>
              <Col className="col-4 mt-3">
                <Link className="customLink" to="/admin-panel/usersManagment">
                  <FontAwesomeIcon
                    className="mr-2"
                    size="lg"
                    icon={faLongArrowAltLeft}
                  ></FontAwesomeIcon>
                  Lista kont
                </Link>
              </Col>
              <Col className="text-center mt-2 col-4">
                <h2>
                  Konto ID: <strong>{props.match.params.id}</strong>
                </h2>
              </Col>
              <Col className="col-4 mt-2">
                {!accountDetails?.isActive ? (
                  <span className="float-right" style={{ color: "red" }}>
                    To konto jest nieaktywne!
                  </span>
                ) : (
                  <Button
                    disabled={!formState.isDirty || !accountDetails?.isActive}
                    onClick={handleSubmit(onSubmit)}
                    variant="success"
                    className="float-right"
                  >
                    Zapisz zmiany
                  </Button>
                )}
              </Col>
            </Row>
            <Row className="justify-content-center">
              <Nav variant="pills" className="customPillsColors">
                <Nav.Item>
                  <Nav.Link eventKey="general">Ogólne</Nav.Link>
                </Nav.Item>
              </Nav>
            </Row>
          </Container>
        </div>
        <div className="pageContent mt-4">
          <Container>
            <Tab.Content className="mt-4">
              <Tab.Pane eventKey="general">
                <Form>
                  <Row>
                    <Col>
                      <Form.Group>
                        <Form.Label>Imię</Form.Label>
                        <Form.Control
                          id="firstName"
                          name="firstName"
                          ref={register}
                          disabled={!accountDetails?.isActive}
                          className="bg-dark text-light"
                          type="text"
                          defaultValue={accountDetails?.firstName}
                        />
                        <Form.Label>Nazwisko</Form.Label>
                        <Form.Control
                          id="lastName"
                          name="lastName"
                          ref={register}
                          disabled={!accountDetails?.isActive}
                          className="bg-dark text-light"
                          type="text"
                          defaultValue={accountDetails?.lastName}
                        />
                        <Form.Label>Email</Form.Label>
                        <Form.Control
                          id="email"
                          name="email"
                          ref={register}
                          disabled={!accountDetails?.isActive}
                          className="bg-dark text-light"
                          type="text"
                          defaultValue={accountDetails?.email}
                        />
                      </Form.Group>
                    </Col>
                    <Col>
                      <Table striped bordered hover variant="dark">
                        <thead>
                          <tr>
                            <th>Rola</th>
                            <th></th>
                          </tr>
                        </thead>
                        <tbody>
                          {fields.map(
                            (element: any, index: number) => (
                              <tr key={element.customId}>
                                <td>{getRoleName(element.id)}</td>
                                <td>
                                  <Form.Control
                                    name={`roles[${index}].isChecked`}
                                    ref={register()}
                                    className="bg-dark text-light"
                                    type="checkbox"
                                    defaultChecked={element.isChecked}
                                  />
                                </td>
                              </tr>
                            )
                          )}
                        </tbody>
                      </Table>
                    </Col>
                  </Row>
                </Form>
              </Tab.Pane>
            </Tab.Content>
          </Container>
        </div>
      </Tab.Container>
    </div>
  );
}

export default EditAccount;
