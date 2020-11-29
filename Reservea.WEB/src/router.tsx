import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import AdminPanel from "./screens/admin-panel/admin-panel";
import Reservations from "./screens/reservations/reservations";
import Home from "./screens/home/home";
import EditResource from "./components/edit-resource/edit-resource";

const Router = () => (
  <BrowserRouter>
    <Switch>
      <Route path="/edit-resource/:id" component={EditResource} />
      <Route path="/admin-panel/:tab" component={AdminPanel}>
      </Route>
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
  </BrowserRouter>
);

export default Router;
