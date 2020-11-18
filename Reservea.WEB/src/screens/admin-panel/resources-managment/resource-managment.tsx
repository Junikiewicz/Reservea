import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import { resourcesListRequest } from "../../../api/clients/resourcesClient";
import { ResourceForListResponse } from "../../../api/dtos/resources/resources/resourceForListResponse";

function ResourceManagment() {
  const [resourcesList, setResourcesList] = useState<
    Array<ResourceForListResponse>
  >([]);

  useEffect(() => {
    resourcesListRequest()
      .then((response: Array<ResourceForListResponse>) => {
        setResourcesList(response);
      })
      .catch(() => {});
  }, []);

  return (
    <Table striped bordered hover variant="dark" className="text-center">
      <thead>
        <tr>
          <th>Id</th>
          <th>Nazwa</th>
          <td>Status</td>
          <td>Typ</td>
        </tr>
      </thead>
      <tbody>
        {resourcesList.map((element) => (
          <tr key={element.id}>
            <td>{element.id}</td>
            <td>{element.name}</td>
            <td>{element.resourceStatusId}</td>
            <td>{element.resourceTypeId}</td>
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
        ))}
      </tbody>
    </Table>
  );
}
export default ResourceManagment;
