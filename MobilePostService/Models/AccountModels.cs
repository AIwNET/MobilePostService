using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MobilePostService.Models
{
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bieżące hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Pamiętasz mnie?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }

        [StringLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków")]
        [Required(ErrorMessage = "Imię jest wymagana.")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków")]
        [Required(ErrorMessage = "Nazwisko jest wymagana.")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane.")]
        [StringLength(40, ErrorMessage = "Miasto może mieć maksymalnie 40 znaków.")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

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
    }
}
