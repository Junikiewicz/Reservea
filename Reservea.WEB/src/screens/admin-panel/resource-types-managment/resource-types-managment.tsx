import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import { ResourceTypeForListResponse } from "../../../api/dtos/resources/resourceTypes/resourceTypeForListResponse";
import { resourcesTypesListRequest } from "../../../api/clients/resourcesClient";

function ResourceTypes() {
  const [resourceTypesList, setResourceTypesList] = useState<
    Array<ResourceTypeForListResponse>
  >([]);

  useEffect(() => {
    resourcesTypesListRequest()
      .then((response: Array<ResourceTypeForListResponse>) => {
        setResourceTypesList(response);
      })
      .catch(() => {});
  }, []);

  return (
    <Table striped bordered hover variant="dark" className="text-center">
      <thead>
        <tr>
          <th>Id</th>
          <th>Nazwa</th>
          <td></td>
        </tr>
      </thead>
      <tbody>
        {resourceTypesList.map((element) => (
          <tr>
            <td>{element.id}</td>
            <td>{element.name}</td>
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
export default ResourceTypes;
