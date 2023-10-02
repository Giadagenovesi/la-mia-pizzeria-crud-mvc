using la_mia_pizzeria_static.Validation;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Url(ErrorMessage = "Devi inserire un link valido ad un'immagine")]
        public string Immagine { get; set; }

        [Required(ErrorMessage = "Una pizza deve per forza avere un gusto")]
        [MaxLength(100, ErrorMessage = "La massima lunghezza del campo è di 100 caratteri")]
        public string Gusto {  get; set; }

        [Required(ErrorMessage = "Non può esistere una pizza senza ingredienti")]
        [MinimumWord]
        [MaxLength(200, ErrorMessage = "La massima lunghezza del campo è di 200 caratteri")]
        public string Ingredienti { get; set; }

        [Required(ErrorMessage = "Non siamo un ente di beneficenza, inserisci un prezzo per questa pizza!")]
        public float Prezzo { get; set; }

        public Pizza() { }

        public Pizza(string immagine, string gusto, string ingredienti, float prezzo)
        {
            this.Immagine = immagine;
            this.Gusto = gusto;
            this.Ingredienti = ingredienti;
            this.Prezzo = prezzo;   
        }
    }
}
