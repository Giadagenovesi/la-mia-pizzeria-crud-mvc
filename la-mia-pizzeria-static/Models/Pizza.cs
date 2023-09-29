namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Immagine { get; set; }
        public string Gusto {  get; set; }
        public string Ingredienti { get; set; } 
        public float Prezzo { get; set; }

        public Pizza(string immagine, string gusto, string ingredienti, float prezzo)
        {
            this.Immagine = immagine;
            this.Gusto = gusto;
            this.Ingredienti = ingredienti;
            this.Prezzo = prezzo;   
        }
    }
}
