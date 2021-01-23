import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/esm/Table";
import { UserForListResponse } from "../../../api/dtos/user/auth/userForLIstRensponse";
import {
  deleteUserRequest,
  getUsersRequest,
} from "../../../api/clients/userClient";
import { getAccountStatusName } from "../../../common/enums/accountStatus";
import { getRoleName } from "../../../common/enums/role";
import LoadingSpinner from "../../../components/loading-spinner/loading-spinner";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";

function UsersManagment() {
  const [userList, setUserList] = useState<Array<UserForListResponse>>([]);
  const [showSpinner, setShowSpinner] = useState<boolean>(true);

  useEffect(() => {
    setShowSpinner(true);
    getUsersRequest()
      .then((response: Array<UserForListResponse>) => {
        setUserList(response);
        setShowSpinner(false);
      })
      .catch(() => {});
  }, []);

  const deleteUser = (userId: number) => {
    deleteUserRequest(userId)
      .then(() => {
        let newUserList = [...userList];
        newUserList = newUserList.filter((x) => x.id != userId);
        setUserList(newUserList);
        toast.success("Dane użytkownika zostały zaanonimizowane");
      })
      .catch(() => {});
  };

  if (showSpinner) {
    return <LoadingSpinner />;
  }

  return (
    <Table striped bordered hover variant="dark" className="text-center">
      <thead>
        <tr>
          <th>Id</th>
          <th>Email</th>
          <td>Status</td>
          <td>Role</td>
        </tr>
      </thead>
      <tbody>
        {userList.map((element) => (
          <tr>
            <td>{element.id}</td>
            <td>{element.email}</td>
            <td>{getAccountStatusName(element.isActive)}</td>
            <td>{element.roles.map((x) => getRoleName(x)).join(", ")}</td>
            <td>
              <Link className="customLink" to={`/edit-account/${element.id}`}>
                <FontAwesomeIcon
                  size="lg"
                  icon={faEdit}
                  style={{ cursor: "pointer" }}
                />
              </Link>
              <FontAwesomeIcon
                size="lg"
                icon={faTrashAlt}
                onClick={() => {
                  deleteUser(element.id);
                }}
                style={{ cursor: "pointer" }}
              />
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
}
export default UsersManagment;
