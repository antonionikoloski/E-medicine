namespace E_medicine.Models
{
    public class DisaseDrugs
    {
        public int Id { get; set; }
        public int DisaseId { get; set; }
        public Disase? Disase { get; set; }
        public int DrugId { get; set; }
        public Drugs? Drugs { get; set; }
    }
}
