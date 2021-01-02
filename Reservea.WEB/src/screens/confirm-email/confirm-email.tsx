import React, { useEffect, useState } from "react";
import { confirmEmailRequest } from "../../api/clients/userClient";
import LoadingSpinner from "../../components/loading-spinner/loading-spinner";

function ConfirmEmail(props: any): JSX.Element {
  const [showSpinner, setShowSpinner] = useState<boolean>(true);

  useEffect(() => {
    setShowSpinner(true);
    const params = new URLSearchParams(props.location.search);
    const token = params.get("token");
    const id = params.get("id");
    confirmEmailRequest(token!, Number(id))
      .then(() => {
        setShowSpinner(false);
      })
      .catch(() => {});
  }, []);

  if (showSpinner) {
    return <LoadingSpinner />;
  }

  return (
    <span>
      Dziękujemy za potwierdzenie adresu email. Możesz się teraz zalogować
    </span>
  );
}

export default ConfirmEmail;
