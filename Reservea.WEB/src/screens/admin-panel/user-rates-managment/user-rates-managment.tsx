import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import { Button, Form } from "react-bootstrap";
import {
  getAllUserRatesRequest,
  updateUserRatesRequest,
} from "../../../api/clients/cmsClients";
import LoadingSpinner from "../../../components/loading-spinner/loading-spinner";
import { toast } from "react-toastify";

function Attributes() {
  const [userRates, setUserRates] = useState<Array<any>>([]);
  const [showSpinner, setShowSpinner] = useState<boolean>(true);
  let userRatesToUpdate: Array<any> = [];

  const updateUserRates = () => {
    updateUserRatesRequest(userRatesToUpdate)
      .then(() => {
        userRatesToUpdate = [];
        toast.success("Udało się zaaktualizować widoczność opinii!");
      })
      .catch(() => {});
  };

  useEffect(() => {
    setShowSpinner(true);
    getAllUserRatesRequest()
      .then((response: Array<any>) => {
        setUserRates(response);
        setShowSpinner(false);
      })
      .catch(() => {});
  }, []);

  const onUserRateChange = (elementId: number, value: boolean) => {
    if (userRatesToUpdate.some((x) => x.id === elementId)) {
      userRatesToUpdate = userRatesToUpdate.filter((x) => x.id !== elementId);
    } else {
      userRatesToUpdate.push({ id: elementId, isVisible: value });
    }
  };

  if (showSpinner) {
    return <LoadingSpinner />;
  }

  return (
    <div>
      <Table striped bordered hover variant="dark" className="text-center">
        <thead>
          <tr>
            <th>Id</th>
            <th>Id użytkownika</th>
            <th>Treść</th>
            <td>Zgoda na publikacje</td>
            <td>Czy widoczna?</td>
          </tr>
        </thead>
        <tbody>
          {userRates.map((element, index) => (
            <tr>
              <td width="100px">{element.id}</td>
              <td width="100px">{element.userId}</td>
              <td>{element.feedback}</td>
              <td width="100px">
                <Form.Control
                  className="bg-dark text-light"
                  type="checkbox"
                  disabled={true}
                  defaultChecked={element.isAllowedToBeShared}
                />
              </td>
              <td width="100px">
                <Form.Control
                  name={`roles[${index}].isChecked`}
                  className="bg-dark text-light"
                  onChange={(e: any) => {
                    onUserRateChange(element.id, e.target.checked === true);
                  }}
                  type="checkbox"
                  defaultChecked={element.isVisible}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button
        onClick={updateUserRates}
        className="float-right"
        variant="success"
      >
        Zapisz zmiany
      </Button>
    </div>
  );
}

export default Attributes;
