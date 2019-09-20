namespace PcReparatie_MK_2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;

    public partial class Reparaty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reparaty()
        {
            Gebruikts = new HashSet<Gebruikt>();
        }

        public int Id { get; set; }

        [Required]
        public string Titel { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDatum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDatum { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        [DataType(DataType.MultilineText)]
        public string Procedure { get; set; }

        public int KlantID { get; set; }

        public int MedewerkerID { get; set; }

        [DisplayFormat(DataFormatString = "{0:c2}", ApplyFormatInEditMode = true)]
        public decimal? Arbeidsloon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gebruikt> Gebruikts { get; set; }

        public virtual Klanten Klanten { get; set; }

        public virtual Medewerker Medewerker { get; set; }
        [NotMapped]
        public string OutOfTime { get { return outOfTime = (StartDatum.Date < DateTime.Now.Date ? "OutOfTime" : "DefaultTime"); } }
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public decimal? TotaalPrijs { get{
                decimal? tempPrijs = 0;
                DataBase tempdb = new DataBase();
                foreach (var item in this.Gebruikts)
                {
                        tempPrijs = tempPrijs + item.PrijsOnderdeel;                  
                }
                tempPrijs = tempPrijs + this.Arbeidsloon;
                return tempPrijs;
            } }
        [NotMapped]
        public string StatusKleur { get {
                string Class = "Awaiting";
                switch (this.Status)
                {
                    case "In afwachting":
                        Class = "Awaiting";
                        break;
                    case "In behandeling":
                        Class = "Processing";
                        break;
                    case "Wachten op onderdelen":
                        Class = "Parts";
                        break;
                    case "Klaar":
                        Class = "Done";
                        break;
                }
                return Class;
            } }
        
        private string outOfTime;
    }
}
