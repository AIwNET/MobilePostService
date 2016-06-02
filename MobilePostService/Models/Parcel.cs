using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobilePostService.Models
{
    public class Parcel
    {
        [ScaffoldColumn(false)]
        public long Id { get; set; }

        [StringLength(50, ErrorMessage="Imię może mieć maksymalnie 50 znaków")]
        [Required(ErrorMessage="Nazwa jest wymagana.")]
        [Display(Name="Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwa miasta jest wymagana.")]
        [StringLength(50, ErrorMessage = "Nazwa miasta może mieć maksymalnie 50 znaków")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana.")]
        [StringLength(50, ErrorMessage = "Wprowadzony ciąg znaków może mieć maksymalnie 50 znaków")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        [StringLength(6, ErrorMessage = "Wprowadzony ciąg znaków może mieć maksymalnie 6 znaków")]
        [Display(Name = "Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[0-9]{2}\-[0-9]{3}$", ErrorMessage="Kod pocztowy jest niepoprawny.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Telefon komórkowy jest wymagany.")]
        [StringLength(11, ErrorMessage = "Wprowadzony ciąg znaków może mieć maksymalnie 11 znaków")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon komórkowy")]
        [RegularExpression(@"^([0-9]{9})|(([0-9]{3}-){2}[0-9]{3})$", ErrorMessage = "Numer jest niepoprawny.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "Wprowadzony ciąg znaków może mieć maksymalnie 100 znaków")]
        [Display(Name = "Adres e-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[_a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "Adres e-mail jest niepoprawny.")]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data rejestracji")]
        public DateTime RegistrationDate { get; set; }
    }
}