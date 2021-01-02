import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import AdminPanel from "./screens/admin-panel/admin-panel";
import Reservations from "./screens/reservations/reservations";
import Home from "./screens/home/home";
import EditResource from "./components/edit-resource/edit-resource";
import EditAccount from "./components/edit-account/edit-account";
import AddResource from "./components/add-resource/add-resource";
import EditResourceType from "./components/edit-resource-type/edit-resource-type";
import AddResourceType from "./components/add-resource-type/add-resource-type";
import ResourceTypeTimeline from "./screens/resource-type-timeline/resource-type-timeline";
import Register from "./screens/register/register";
import UserReservations from "./screens/user-reservations/user-reservations";
import NavbarLayout from "./components/navbar/navbar";
import { Container } from "react-bootstrap";
import ConfirmEmail from "./screens/confirm-email/confirm-email";
import ResetPassword from "./screens/reset-password/reset-password"
import CreateResetPassword from "./screens/create-reset-password/create-reset-password"

const Router = () => (
  <BrowserRouter>
    <NavbarLayout />
    <Container className="mt-4" style={{ overflow: "hidden" }}>
      <Switch>
        <Route path="/edit-resource/:id" component={EditResource} />
        <Route path="/edit-account/:id" component={EditAccount} />
        <Route
          path="/createReservation/:resourceTypeID"
          component={ResourceTypeTimeline}
        />
        <Route path="/confirm-email" component={ConfirmEmail} />
        <Route path="/reset-password" component={ResetPassword} />
        <Route path="/create-reset-password" component={CreateResetPassword} />
        <Route path="/user-reservations" component={UserReservations} />
        <Route path="/register" component={Register} />
        <Route path="/edit-resource-type/:id" component={EditResourceType} />
        <Route path="/add-resource" component={AddResource} />
        <Route path="/add-resource-type" component={AddResourceType} />
        <Route path="/admin-panel/:tab" component={AdminPanel}></Route>
        <Route path="/admin-panel/">
          <AdminPanel />
        </Route>
        <Route path="/reservations">
          <Reservations />
        </Route>
        <Route path="/">
          <Home />
        </Route>
      </Switch>
    </Container>
  </BrowserRouter>
);

export default Router;
