import React from "react";
import { FormControl, Button, Form, Nav, Navbar, Container } from "react-bootstrap";

const NavbarLayout = () => (
  <Navbar bg="dark" variant="dark">
      <Container>
      <Navbar.Brand>Reservea</Navbar.Brand>
    <Nav className="mr-auto">
      <Nav.Link href="/">Strona główna</Nav.Link>
      <Nav.Link href="/reservations">Rezerwacje</Nav.Link>
      <Nav.Link href="/admin-panel">Panel administratora</Nav.Link>
    </Nav>
    <Form inline>
      <FormControl type="text" placeholder="Email" className="mr-sm-2 bg-dark text-light" />
      <FormControl type="password" placeholder="Hasło" className="mr-sm-2 bg-dark text-light" />
      <Button variant="outline-secondary text-light">Zaloguj</Button>
    </Form>
      </Container>
  </Navbar>
);

export default NavbarLayout;