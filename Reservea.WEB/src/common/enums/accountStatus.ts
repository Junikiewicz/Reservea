export const getAccountStatusName = (isActive: boolean): string => {
  if (isActive) {
    return "Aktywne";
  } else {
    return "Nieaktywne";
  }
};
