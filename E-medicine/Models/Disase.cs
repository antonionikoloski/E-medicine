namespace E_medicine.Models
{
    public class Disase
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Organs { get; set; }
       
        public ICollection <DisaseDrugs> DisaseDrugs{ get; set; }


    }
}
