import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import {
  deleteResourceRequest,
  resourcesListRequest,
} from "../../../api/clients/resourcesClient";
import { ResourceForListResponse } from "../../../api/dtos/resources/resources/resourceForListResponse";
import { Link } from "react-router-dom";
import { Button, Row } from "react-bootstrap";
import { toast } from "react-toastify";
import { ResourceStatus } from "../../../common/enums/resourceStatus";

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

  const deleteResource = async (resourrceId: number) => {
    deleteResourceRequest(resourrceId)
      .then(() => {
        let newArray = [...resourcesList];
        newArray = newArray.filter(x=>x.id != resourrceId);
        setResourcesList(newArray);
        toast.info("Obiekt oznaczony jako usunięty");
      })
      .catch(() => {});
  };

  return (
    <div>
      <Table striped bordered hover variant="dark" className="text-center">
        <thead>
          <tr>
            <th>Id</th>
            <th>Nazwa</th>
            <td>Status</td>
            <td>Typ</td>
            <td></td>
          </tr>
        </thead>
        <tbody>
          {resourcesList.map((element) => (
            <tr key={element.id}>
              <td width="100px">{element.id}</td>
              <td>{element.name}</td>
              <td>{element.resourceStatusId}</td>
              <td>{element.resourceTypeId}</td>
              <td width="100px">
                <Link
                  className="customLink"
                  to={`/edit-resource/${element.id}`}
                >
                  <FontAwesomeIcon
                    size="lg"
                    icon={faEdit}
                    style={{ cursor: "pointer" }}
                  />
                </Link>
                {element.resourceStatusId !== ResourceStatus.Removed && (
                  <FontAwesomeIcon
                    onClick={() => {
                      deleteResource(element.id);
                    }}
                    className="ml-3 trashClickableIcon"
                    size="lg"
                    icon={faTrashAlt}
                  />
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button href="/add-resource" className="float-right" variant="success">
        Dodaj nowy
      </Button>
    </div>
  );
}
export default ResourceManagment;
