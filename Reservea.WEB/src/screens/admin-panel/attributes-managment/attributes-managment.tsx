import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faTrashAlt } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import {
  addAttributeRequest,
  attributesListRequest,
  deleteAttributeRequest,
  updateAttributeRequest,
} from "../../../api/clients/resourcesClient";
import { AttributeForListResponse } from "../../../api/dtos/resources/attributes/attributeForListResponse";
import AddAttributeModal from "./add-attribute-modal";
import { Button } from "react-bootstrap";
import EditAttributeModal from "./edit-attribute-modal";
import { toast } from "react-toastify";
import LoadingSpinner from "../../../components/loading-spinner/loading-spinner";

function Attributes() {
  const [attributesList, setAttributesList] = useState<
    Array<AttributeForListResponse>
  >([]);
  const [showSpinner, setShowSpinner] = useState<boolean>(true);
  const [showAddAttribute, setShowAddAttribute] = useState(false);
  const [showEditAttribute, setShowEditAttribute] = useState(false);
  const [
    editedAttribute,
    setEditedAttribute,
  ] = useState<AttributeForListResponse>({ id: 0, name: "" });

  const handleCloseAddAttribute = () => setShowAddAttribute(false);
  const handleChooseAddAttribute = (attributeName: string) => {
    addAttributeRequest(attributeName)
      .then((response: AttributeForListResponse) => {
        let newAttributesList = [...attributesList];
        newAttributesList.push(response);
        setAttributesList(newAttributesList);
      })
      .catch(() => {});
    setShowAddAttribute(false);
  };
  const handleShowAddAtribute = () => setShowAddAttribute(true);

  const handleCloseEditAttribute = () => setShowEditAttribute(false);
  const handleChooseEditAttribute = (
    attributeName: string,
    attributeId: number
  ) => {
    updateAttributeRequest(attributeId, attributeName)
      .then(() => {
        let newAttributesList = [...attributesList];
        newAttributesList[
          newAttributesList.findIndex((x) => x.id == attributeId)
        ].name = attributeName;
        setAttributesList(newAttributesList);
      })
      .catch(() => {});
    setShowEditAttribute(false);
  };
  const handleShowEditAtribute = (index: number) => {
    setEditedAttribute(attributesList[index]);
    setShowEditAttribute(true);
  };

  const deleteAttribute = async (attributeId: number) => {
    deleteAttributeRequest(attributeId)
      .then(() => {
        let newArray = [...attributesList];
        newArray = newArray.filter((x) => x.id != attributeId);
        setAttributesList(newArray);
        toast.info("Obiekt oznaczony jako usunięty");
      })
      .catch(() => {});
  };

  useEffect(() => {
    setShowSpinner(true);
    attributesListRequest()
      .then((response: Array<AttributeForListResponse>) => {
        setAttributesList(response);
        setShowSpinner(false);
      })
      .catch(() => {});
  }, []);

  if (showSpinner) {
    return <LoadingSpinner />;
  }

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
          {attributesList.map((element, index) => (
            <tr>
              <td width="100px">{element.id}</td>
              <td>{element.name}</td>
              <td width="100px">
                <FontAwesomeIcon
                  className="customLink"
                  size="lg"
                  onClick={() => {
                    handleShowEditAtribute(index);
                  }}
                  icon={faEdit}
                  style={{ cursor: "pointer" }}
                />
                <FontAwesomeIcon
                  onClick={() => {
                    deleteAttribute(element.id);
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
        onClick={handleShowAddAtribute}
        className="float-right"
        variant="success"
      >
        Dodaj nowy
      </Button>
      <AddAttributeModal
        show={showAddAttribute}
        handleClose={handleCloseAddAttribute}
        handleChoose={handleChooseAddAttribute}
      />
      <EditAttributeModal
        show={showEditAttribute}
        handleClose={handleCloseEditAttribute}
        handleChoose={handleChooseEditAttribute}
        oldValue={editedAttribute}
      />
    </div>
  );
}

export default Attributes;
