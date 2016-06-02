using System;
using System.ComponentModel.DataAnnotations;

namespace MobilePostService.Models
{
    [MetadataType(typeof(PostmanMetaData))]
    public class Postman
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int UserId { get; set; }
    }

    public class PostmanMetaData
    {
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Telefon komórkowy")]
        public string Phone { get; set; }

        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Potwierdzony")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "Data rejestracji")]
        public DateTime RegistrationDate { get; set; }
    }
}