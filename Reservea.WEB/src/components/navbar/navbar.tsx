import React from "react";
import { Nav, Navbar, Container, Button } from "react-bootstrap";
import { checkIfLoggedIn } from "../../common/helpers/jwtTokenHelper";
import LogInForm from "../log-in-form/log-in-form";

function NavbarLayout(): JSX.Element {
  return (
    <Navbar bg="dark" variant="dark">
      <Container>
        <Navbar.Brand>Reservea</Navbar.Brand>
        <Nav className="mr-auto">
          <Nav.Link href="/">Strona główna</Nav.Link>
          <Nav.Link href="/reservations">Rezerwacje</Nav.Link>
          {checkIfLoggedIn() && (
            <Nav.Link href="/admin-panel">Panel administratora</Nav.Link>
          )}
        </Nav>
        <LogInForm></LogInForm>
      </Container>
    </Navbar>
  );
}

export default NavbarLayout;
