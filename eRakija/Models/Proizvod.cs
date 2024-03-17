namespace eRakija.Models
{
    public class Proizvod
    {
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public required int Cena { get; set; }

    public string? Slika { get; set; }
    public string? Opis { get; set; } 

    public int? Kolicina { get; set; }
    public TipProizvoda? tipProizvoda { get; set; }
    }
}