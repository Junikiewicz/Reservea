import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import AdminPanel from "./screens/admin-panel/admin-panel";
import Reservations from "./screens/reservations/reservations";
import Home from "./screens/home/home";
import EditResource from "./components/edit-resource/edit-resource";
import AddResource from "./components/add-resource/add-resource";
import EditResourceType from "./components/edit-resource-type/edit-resource-type";
import AddResourceType from "./components/add-resource-type/add-resource-type";
import ResourceTypeTimeline from "./screens/resource-type-timeline/resource-type-timeline";

const Router = () => (
  <BrowserRouter>
    <Switch>
      <Route path="/edit-resource/:id" component={EditResource} />
      <Route
        path="/createReservation/:resourceTypeID"
        component={ResourceTypeTimeline}
      />
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
  </BrowserRouter>
);

export default Router;
