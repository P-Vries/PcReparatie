namespace PcReparatie_MK_2.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Klanten")]
    public partial class Klanten
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klanten()
        {
            Reparaties = new HashSet<Reparaty>();
        }

        public int Id { get; set; }

        [Required]
        public string VoorNaam { get; set; }

        [StringLength(50)]
        public string TussenVoegsel { get; set; }

        [Required]
        public string AchterNaam { get; set; }

        public int RolId { get; set; }

        public virtual Rollen Rollen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reparaty> Reparaties { get; set; }
        [NotMapped]
        public string VolledigeNaam { get { return $"{AchterNaam} {VoorNaam}"; } }
    }
}
