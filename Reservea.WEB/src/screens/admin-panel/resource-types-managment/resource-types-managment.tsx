import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React from "react";
import Table from "react-bootstrap/esm/Table";

const ResourceTypes = () => (
  <Table striped bordered hover variant="dark" className="text-center">
    <thead>
      <tr>
        <th>Id</th>
        <th>Nazwa</th>
        <td></td>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>1</td>
        <td>Stoły do pinponga</td>
        <td><FontAwesomeIcon size="lg" icon={faEdit} style={{cursor:"pointer"}}/><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
      <tr>
        <td>2</td>
        <td>Korty tenisowe</td>
        <td><FontAwesomeIcon size="lg" icon={faEdit} style={{cursor:"pointer"}}/><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
      <tr>
        <td>3</td>
        <td>Siłownia</td>
        <td><FontAwesomeIcon size="lg" icon={faEdit} style={{cursor:"pointer"}}/><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
    </tbody>
  </Table>
);

export default ResourceTypes;
