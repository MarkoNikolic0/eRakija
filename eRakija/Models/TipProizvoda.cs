namespace eRakija.Models
{
    public class TipProizvoda
    {

    [Key]
    public int Id { get; set; }
    public required string Naziv {get; set;}

    [JsonIgnore]
    public List<Proizvod>? proizvod {get; set;}
    }
}