import { apiClient } from "./apiClient";

export interface ReservationRequest {
  start: Date;
  end: Date;
  resourceId: number;
}

const dateToISOLikeButLocal = (date: Date) => {
  const offsetMs = date.getTimezoneOffset() * 60 * 1000;
  const msLocal = date.getTime() - offsetMs;
  const dateLocal = new Date(msLocal);
  const iso = dateLocal.toISOString();
  const isoLocal = iso.slice(0, 19);
  return isoLocal;
};

export interface ReservationForListResponse {
  id: number;
  resourceId: number;
  resourceName: string;
  start: Date;
  end: Date;
  reservationStatusId: number;
  userId: number;
  username: string;
}

export const createReservation = async (data: Array<ReservationRequest>) => {
  let request = data.map((x) => ({
    start: dateToISOLikeButLocal(x.start),
    end: dateToISOLikeButLocal(x.end),
    resourceId: x.resourceId,
  }));

  await apiClient.post("/api/reservation/Reservations", request);
};

export const getResourceTypeReservations = async (
  resourceTypeId: number
): Promise<Array<ReservationRequest>> => {
  let response = await apiClient.get<Array<ReservationRequest>>(
    "/api/reservation/Reservations",
    {
      params: { resourceTypeId },
    }
  );

  return response.data;
};

export const getReservationsList = async (): Promise<
  Array<ReservationForListResponse>
> => {
  let response = await apiClient.get<Array<ReservationForListResponse>>(
    "/api/reservation/Reservations/all"
  );

  return response.data;
};
