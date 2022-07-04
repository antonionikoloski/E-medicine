namespace E_medicine.Models
{
    public class Drugs
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        


        //bolesti
        public string? DosageForm { get; set; }

        public string? Strenght { get; set; }

        public ICollection<DisaseDrugs>? DisaseDrugs { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public string image { get; set; }


    }
}
