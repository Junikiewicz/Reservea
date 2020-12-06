import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import { ResourceTypeForListResponse } from "../../../api/dtos/resources/resourceTypes/resourceTypeForListResponse";
import {
  deleteResourceTypeRequest,
  resourcesTypesListRequest,
} from "../../../api/clients/resourcesClient";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import { toast } from "react-toastify";

function ResourceTypes() {
  const [resourceTypesList, setResourceTypesList] = useState<
    Array<ResourceTypeForListResponse>
  >([]);

  const deleteResourceType = async (resourrceTypeId: number) => {
    deleteResourceTypeRequest(resourrceTypeId)
      .then(() => {
        let newArray = [...resourceTypesList];
        newArray = newArray.filter((x) => x.id != resourrceTypeId);
        setResourceTypesList(newArray);
        toast.info("Obiekt oznaczony jako usunięty");
      })
      .catch(() => {});
  };

  useEffect(() => {
    resourcesTypesListRequest()
      .then((response: Array<ResourceTypeForListResponse>) => {
        setResourceTypesList(response);
      })
      .catch(() => {});
  }, []);

  return (
    <div>
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
              <td width="100px">{element.id}</td>
              <td>{element.name}</td>
              <td width="100px">
                <Link
                  className="customLink"
                  to={`/edit-resource-type/${element.id}`}
                >
                  <FontAwesomeIcon
                    size="lg"
                    icon={faEdit}
                    style={{ cursor: "pointer" }}
                  />
                </Link>
                <FontAwesomeIcon
                  onClick={() => {
                    deleteResourceType(element.id);
                  }}
                  className="ml-3 trashClickableIcon"
                  size="lg"
                  icon={faTrashAlt}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button
        href="/add-resource-type"
        className="float-right"
        variant="success"
      >
        Dodaj nowy
      </Button>
    </div>
  );
}
export default ResourceTypes;
