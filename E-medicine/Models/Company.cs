namespace E_medicine.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? num_empl { get; set; }

        public string? image { get; set; }

        public string? country { get; set; }
        public IList<Drugs>? Drugs { get; set; }

    }
}
