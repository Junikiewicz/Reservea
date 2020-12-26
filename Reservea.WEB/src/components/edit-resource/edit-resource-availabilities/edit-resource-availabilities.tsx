import { faTrashAlt } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Datetime from "react-datetime";
import React from "react";
import { Button, Form, FormCheck, FormControl, Table } from "react-bootstrap";
import { ResoucerTypeAvaliabilitiesResponse } from "../../../api/dtos/resources/resources/resoucerTypeAvaliabilitiesResponse";
import LoadingSpinner from "../../loading-spinner/loading-spinner";
import "react-datetime/css/react-datetime.css";
import "./edit-resource-availabilities.css";

function EditResourceAvailabilities({
  resourceAvailabilities,
  register,
  addNewResourceAvailability,
  removeResourceAvailability,
}: any) {
  if (!resourceAvailabilities) {
    return <LoadingSpinner />;
  }

  return (
    <div>
      <Table striped bordered hover variant="dark">
        <thead>
          <tr>
            <th>Start</th>
            <th>Koniec</th>
            <th>Powtarzająca się</th>
            <th>Interwał (minuty)</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {resourceAvailabilities.map((element: any, index: number) => (
            <tr key={element.customId}>
              <td>
                <FormControl
                  name={`resourceAvailabilities[${index}].start`}
                  className="bg-dark text-light"
                  type="datetime-local"
                  ref={register()}
                  defaultValue={element.start}
                ></FormControl>
              </td>
              <td>
                <FormControl
                  name={`resourceAvailabilities[${index}].end`}
                  className="bg-dark text-light"
                  type="datetime-local"
                  ref={register()}
                  defaultValue={element.end}
                ></FormControl>
              </td>
              <td>
                <FormControl
                  name={`resourceAvailabilities[${index}].isReccuring`}
                  className="bg-dark text-light"
                  type="checkbox"
                  ref={register()}
                  defaultChecked={element.isReccuring}
                ></FormControl>
              </td>
              <td>
                <FormControl
                  name={`resourceAvailabilities[${index}].interval`}
                  ref={register()}
                  className="bg-dark text-light"
                  defaultValue={element.interval}
                  style={{ width: "150px" }}
                  type="number"
                ></FormControl>
              </td>
              <td>
                <FontAwesomeIcon
                  className="trashClickableIcon"
                  size="lg"
                  icon={faTrashAlt}
                  onClick={() => removeResourceAvailability(index)}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button
        onClick={addNewResourceAvailability}
        className="float-right mb-2"
        variant="success"
      >
        Dodaj nową
      </Button>
    </div>
  );
}

export default EditResourceAvailabilities;
