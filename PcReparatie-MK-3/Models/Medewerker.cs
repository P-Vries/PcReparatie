namespace PcReparatie_MK_2.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Medewerker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medewerker()
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
    }
}
