import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React from "react";
import Table from "react-bootstrap/esm/Table";

const ReservationsManagment = () => (
  <Table striped bordered hover variant="dark" className="text-center">
    <thead>
      <tr>
        <th>Id</th>
        <th>Zasób</th>
        <th>Użytkownik</th>
        <th>Start</th>
        <th>Status</th>
        <td></td>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>1</td>
        <td>Stół #1</td>
        <td>zenondanonek@gmail.com</td>
        <td>11-02-2023 15:34</td>
        <td>Aktywna</td>
        <td>
          <FontAwesomeIcon
            size="lg"
            icon={faEdit}
            style={{ cursor: "pointer" }}
          />
          <FontAwesomeIcon
            size="lg"
            icon={faTrashAlt}
            style={{ cursor: "pointer" }}
          />
        </td>
      </tr>
      <tr>
        <td>2</td>
        <td>Stół #1</td>
        <td>jkowalski@gmail.com</td>
        <td>11-02-2023 11:13</td>
        <td>Zakończona</td>
        <td>
          <FontAwesomeIcon
            size="lg"
            icon={faEdit}
            style={{ cursor: "pointer" }}
          />
          <FontAwesomeIcon
            size="lg"
            icon={faTrashAlt}
            style={{ cursor: "pointer" }}
          />
        </td>
      </tr>
      <tr>
        <td>3</td>
        <td>Stół #3</td>
        <td>jkowalski@gmail.com</td>
        <td>14-02-2013 05:54</td>
        <td>Anulowana</td>
        <td>
          <FontAwesomeIcon
            size="lg"
            icon={faEdit}
            style={{ cursor: "pointer" }}
          />
          <FontAwesomeIcon
            size="lg"
            icon={faTrashAlt}
            style={{ cursor: "pointer" }}
          />
        </td>
      </tr>
    </tbody>
  </Table>
);

export default ReservationsManagment;
