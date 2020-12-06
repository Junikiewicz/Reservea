export enum ResourceStatus {
  New = 1,
  Active = 2,
  NotActive = 3,
  Removed = 4,
}

export const getResourceStatusName = (
  resourceStatusId: Number
): string => {
  switch (resourceStatusId) {
    case ResourceStatus.Active: {
      return "Aktywny";
    }
    case ResourceStatus.New: {
      return "Nowy";
    }
    case ResourceStatus.NotActive: {
      return "Nieaktywny";
    }
    case ResourceStatus.Removed: {
      return "Usunięty";
    }
    default: {
      return "Unrecognized";
    }
  }
};
