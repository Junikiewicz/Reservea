import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React from "react";
import Table from "react-bootstrap/esm/Table";

const UsersManagment = () =>(
    <Table striped bordered hover variant="dark" className="text-center">
    <thead>
      <tr>
        <th>Id</th>
        <th>Email</th>
        <td>Status</td>
        <td>Rola</td>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>1</td>
        <td>jkowalski@gmail.com</td>
        <td>Aktywny</td>
        <td>Administrator</td>
        <td><FontAwesomeIcon size="lg" icon={faEdit} style={{cursor:"pointer"}}/><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
      <tr>
        <td>2</td>
        <td>znowak@gmail.com</td>
        <td>Aktywny</td>
        <td>Moderator</td>
        <td><FontAwesomeIcon size="lg" icon={faEdit} style={{cursor:"pointer"}}/><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
      <tr>
        <td>3</td>
        <td>pkolodziej@gmail.com</td>
        <td>Nieaktywny</td>
        <td>Klient</td>
        <td><FontAwesomeIcon size="lg" icon={faEdit} style={{cursor:"pointer"}}/><FontAwesomeIcon size="lg" icon={faTrashAlt} style={{cursor:"pointer"}}/></td>
      </tr>
    </tbody>
  </Table>
)

export default UsersManagment;