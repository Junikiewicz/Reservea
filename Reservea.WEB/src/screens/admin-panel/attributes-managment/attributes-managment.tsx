import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt } from "@fortawesome/free-solid-svg-icons";
import React from "react";
import Table from "react-bootstrap/esm/Table";

const Attributes = () => (
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
        <td>Ilość miejsc</td>
        <td><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
      <tr>
        <td>2</td>
        <td>Konie mechaniczne</td>
        <td><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
      <tr>
        <td>3</td>
        <td>Pojemność silnika</td>
        <td><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
    </tbody>
  </Table>
);

export default Attributes;
