namespace PcReparatie_MK_2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Gebruikt")]
    public partial class Gebruikt
    {
        public int Id { get; set; }

        [Required]
        public string NaamOnderdeel { get; set; }

        public decimal PrijsOnderdeel { get; set; }

        public int ReparatieId { get; set; }

        public virtual Reparaty Reparaty { get; set; }
    }
}
