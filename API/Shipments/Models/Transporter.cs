using System.ComponentModel.DataAnnotations.Schema;

namespace Shipments.Models
{
    public class Transporter
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("corporateid")]
        public int CorporateId { get; set; }

        [Column("managerid")]
        public int ManagerId { get; set; }
    }
}
