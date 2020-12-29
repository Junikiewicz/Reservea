export enum Role {
    Admin = 1,
    Employee = 2,
    Customer = 3
  }
  

  export const getRoleName = (
    roleId: Number
  ): string => {
    switch (roleId) {
      case Role.Admin: {
        return "Administrator";
      }
      case Role.Employee: {
        return "Pracownik";
      }
      case Role.Customer: {
        return "Klient";
      }
      default: {
        return "Unrecognized";
      }
    }
  };
  