using System;
using System.Collections.Generic;

namespace Reservea.Common.Mails.Models
{
    public class ReservationConfirmationMailTemplateModel : BaseMailModel
    {
        public string Name { get; set; }
        public IEnumerable<ReservationModel> Reservations { get; set; }
        public string ReservationsListUrl { get; set; }
    }

    public class ReservationModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ResourceName { get; set; }
    }
}
