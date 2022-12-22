using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace U2_W5_D4_BACK.Models
{
    public class Scarpe
    {
        public int IDProdotto { get; set; }

        [Display(Name = "Nome Prodotto")]
        [Required(ErrorMessage = "Nome Obbligatorio")]
        public string NomeProdotto { get; set; }

        [Display(Name = "Immagine Prodotto")]
        [Required(ErrorMessage = "Immagine Obbligatoria")]
        public string ImmagineCover { get; set; }

        [Display(Name = "Immagine Prodotto ")]
        [Required(ErrorMessage = "Immagine Obbligatoria")]
        public string ImmagineDescrizione1 { get; set; }

        [Display(Name = "Immagine Prodotto")]
        [Required(ErrorMessage = "Immagine Obbligatoria")]
        public string ImmagineDescrizione2 { get; set; }

        [Display(Name = "Descrizione Prodotto")]
        [Required(ErrorMessage = "Descrizione Obbligatoria")]
        public string Descrizione { get; set; }

        [Display(Name = "Prezzo Prodotto")]
        [Required(ErrorMessage = "Prezzo Obbligatorio")]
        public decimal PrezzoProdotto { get; set; }
        
        [Display(Name = "Stato Prodotto")]
        [Required(ErrorMessage = "Stato Obbligatorio")]
        public bool ProdottoAttivo { get; set; }

        public static List<Scarpe> listaScarpe = new List<Scarpe>();

    }
}