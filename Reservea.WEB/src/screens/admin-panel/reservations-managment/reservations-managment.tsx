import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import LoadingSpinner from "../../../components/loading-spinner/loading-spinner";
import {
  getReservationsList,
  ReservationForListResponse,
} from "../../../api/clients/reservationsClient";
import {
  ReservationStatus,
  getReservationStatusName,
} from "../../../common/enums/reservationStatus";
import { Link } from "react-router-dom";

function ReservationsManagment() {
  const [reservationsList, setReservationsLIst] = useState<
    Array<ReservationForListResponse>
  >([]);
  const [showSpinner, setShowSpinner] = useState<boolean>(true);

  useEffect(() => {
    setShowSpinner(true);
    getReservationsList()
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
    <Table striped bordered hover variant="dark" className="text-center">
      <thead>
        <tr>
          <th>Id</th>
          <th>Zasób</th>
          <th>Użytkownik</th>
          <th>Termin</th>
          <th>Status</th>
          <td></td>
        </tr>
      </thead>
      <tbody>
        {reservationsList.map((element) => (
          <tr>
            <td width="100px">{element.id}</td>
            <td width="100px">
              <Link className="customLink" to={`/edit-resource/${element.resourceId}`}>
                {element.resourceId}
              </Link>{" "}
            </td>
            <td width="100px">{element.userId}</td>
            <td width="400px">{`${new Date(
              element.start
            ).toLocaleString()} - ${new Date(
              element.end
            ).toLocaleString()}`}</td>
            <td>{getReservationStatusName(element.reservationStatusId)}</td>
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
export default ReservationsManagment;
