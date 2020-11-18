import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import { attributesListRequest } from "../../../api/clients/resourcesClient";
import { AttributeForListResponse } from "../../../api/dtos/resources/attributes/attributeForListResponse";

function Attributes() {
  const [attributesList, setAttributesList] = useState<
    Array<AttributeForListResponse>
  >([]);

  useEffect(() => {
    attributesListRequest()
      .then((response: Array<AttributeForListResponse>) => {
        setAttributesList(response);
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
        {attributesList.map((element) => (
          <tr>
            <td>{element.id}</td>
            <td>{element.name}</td>
            <td>
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

export default Attributes;
