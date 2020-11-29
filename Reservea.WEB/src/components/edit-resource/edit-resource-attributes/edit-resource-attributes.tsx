import React from "react";
import { Form, Table } from "react-bootstrap";
import { ResourceAttributeResponse } from "../../../api/dtos/resources/resourceAttributes/resourceAttributeResponse";
import LoadingSpinner from "../../loading-spinner/loading-spinner";

function EditResourceAttributes({ resourceAttributes, register }: any) {
  if (!resourceAttributes) {
    return <LoadingSpinner />;
  }

  return (
    <Table striped bordered hover variant="dark">
      <thead>
        <tr>
          <th>Nazwa</th>
          <th>Wartość</th>
        </tr>
      </thead>
      <tbody>
        {resourceAttributes.map((element: ResourceAttributeResponse, index: number ) => (
          <tr>
            <td >{element.name}</td>
            <td><Form.Control
              name={`resourceAttributes[${index}].value`}
              ref={register()}
              className="bg-dark text-light"
              type="text"
              defaultValue={element.value}
            /></td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
}

export default EditResourceAttributes;
