namespace PcReparatie_MK_2.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Rollen")]
    public partial class Rollen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rollen()
        {
            Klantens = new HashSet<Klanten>();
            Medewerkers = new HashSet<Medewerker>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Naam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Klanten> Klantens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medewerker> Medewerkers { get; set; }
    }
}
