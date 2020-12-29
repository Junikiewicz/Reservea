import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import LoadingSpinner from "../../components/loading-spinner/loading-spinner";
import {
  getReservationsList,
  getUserReservations,
  ReservationForListResponse,
} from "../../api/clients/reservationsClient";
import {
  ReservationStatus,
  getReservationStatusName,
} from "../../common/enums/reservationStatus";
import { Link } from "react-router-dom";
import { Container } from "react-bootstrap";

function UserReservations() {
  const [reservationsList, setReservationsLIst] = useState<
    Array<ReservationForListResponse>
  >([]);
  const [showSpinner, setShowSpinner] = useState<boolean>(true);

  useEffect(() => {
    setShowSpinner(true);
    getUserReservations()
      .then((response: Array<ReservationForListResponse>) => {
        setReservationsLIst(response);
        setShowSpinner(false);
      })
      .catch(() => {});
  }, []);

  if (showSpinner) {
    return <LoadingSpinner />;
  }

  return (
    <div>
      <div className="pageHeader">
        <Container className="text-center">
          <h1>Twoje rezerwacje</h1>
        </Container>
      </div>
      <div className="pageContent mt-3">
        <Container className="mt-3">
          <Table striped bordered hover variant="dark" className="text-center">
            <thead>
              <tr>
                <th>Id</th>
                <th>Zasób</th>
                <th>Termin</th>
                <th>Status</th>
                <td></td>
              </tr>
            </thead>
            <tbody>
              {reservationsList.map((element) => (
                <tr>
                  <td width="100px">{element.id}</td>
                  <td>{element.resourceName}</td>
                  <td width="400px">{`${new Date(
                    element.start
                  ).toLocaleString()} - ${new Date(
                    element.end
                  ).toLocaleString()}`}</td>
                  <td>
                    {getReservationStatusName(element.reservationStatusId)}
                  </td>
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
        </Container>
      </div>
    </div>
  );
}
export default UserReservations;
