import React from "react";
import { Nav, Row, Tab } from "react-bootstrap";
import ResourceManagment from "./resources-managment/resource-managment";
import AttributesManagment from "./attributes-managment/attributes-managment";
import ResourceTypesManagment from "./resource-types-managment/resource-types-managment";
import UsersManagment from "./users-managment/users-managment";
import ReservationsManagment from "./reservations-managment/reservations-managment";
import HomePageManagment from "./home-page-managment/home-page-managment";

const AdminPanel = () => (
  <Tab.Container defaultActiveKey="reservationsManagment">
    <Row className="justify-content-center">
      <Nav variant="pills" className="customPillsColors">
        <Nav.Item>
          <Nav.Link eventKey="reservationsManagment">Rezerwacje</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="usersManagment">Użytkownicy</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="resourceManagment">Zasoby</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="resourceTypesManagment">Typy zasobów</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="atributesManagment">Atrybuty</Nav.Link>
        </Nav.Item>
        <Nav.Item>
          <Nav.Link eventKey="homePageManagment">Strona główna</Nav.Link>
        </Nav.Item>
      </Nav>
    </Row>
    <Tab.Content className ="mt-4">
      <Tab.Pane eventKey="reservations"></Tab.Pane>
      <Tab.Pane eventKey="users"></Tab.Pane>
      <Tab.Pane eventKey="resourceManagment">
        <ResourceManagment />
      </Tab.Pane>
      <Tab.Pane eventKey="atributesManagment">
        <AttributesManagment />
      </Tab.Pane>
      <Tab.Pane eventKey="resourceTypesManagment">
        <ResourceTypesManagment />
      </Tab.Pane>
      <Tab.Pane eventKey="usersManagment">
        <UsersManagment />
      </Tab.Pane>
      <Tab.Pane eventKey="reservationsManagment">
        <ReservationsManagment />
      </Tab.Pane>
      <Tab.Pane eventKey="homePageManagment">
        <HomePageManagment />
      </Tab.Pane>
    </Tab.Content>
  </Tab.Container>
);

export default AdminPanel;
