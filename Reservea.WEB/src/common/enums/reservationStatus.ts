export enum ReservationStatus {
  New = 1,
  Cancelled = 2,
  Finished = 3,
}

export const getReservationStatusName = (
  reservationStatusId: Number
): string => {
  switch (reservationStatusId) {
    case ReservationStatus.Cancelled: {
      return "Anulowana";
    }
    case ReservationStatus.New: {
      return "Nowa";
    }
    case ReservationStatus.Finished: {
      return "Zakończona";
    }
    default: {
      return "Unrecognized";
    }
  }
};
