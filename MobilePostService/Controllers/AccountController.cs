using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MobilePostService.Models;
using System.Configuration;
using MobilePostService.Repositories;

namespace MobilePostService.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private PostmanRepository _postmanRepo;

        public AccountController()
        {
            _postmanRepo = new PostmanRepository();
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // Dotarcie do tego miejsca wskazuje, że wystąpił błąd, wyświetl ponownie formularz
            ModelState.AddModelError("", "Podana nazwa użytkownika lub hasło są niepoprawne.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Parcel");
        }

        //
        // GET: /Account/Register

        [Authorize(Roles = "Administrator")]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Próba zarejestrowania użytkownika
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    int userId = WebSecurity.GetUserId(model.UserName);
                    DateTime registrationDate = DateTime.Now;

                    string roleName = ConfigurationManager.AppSettings["PostmanRole"];

                    if (!Roles.RoleExists(roleName))
                    {
                        Roles.CreateRole(roleName);
                    }

                    Roles.AddUserToRole(model.UserName, roleName);

                    var postmen = new Postman();

                    postmen.FirstName = model.FirstName;
                    postmen.LastName = model.LastName;
                    postmen.Email = model.Email;
                    postmen.UserId = userId;
                    postmen.City = model.City;
                    postmen.Phone = model.Phone;
                    postmen.IsConfirmed = false;
                    postmen.RegistrationDate = registrationDate;

                    // Zapisanie usługodawcy w bazie danych
                    _postmanRepo.Add(postmen);
                    _postmanRepo.SaveChanges();

                    TempData["Message"] = "Użytkownik został zarejestrowany.";
                    return RedirectToAction("Index", "Parcel");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // Dotarcie do tego miejsca wskazuje, że wystąpił błąd, wyświetl ponownie formularz
            return View(model);
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Hasło zostało zmienione."
                : message == ManageMessageId.SetPasswordSuccess ? "Hasło zostało ustawione."
                : message == ManageMessageId.RemoveLoginSuccess ? "Logowanie zewnętrzne zostało usunięte."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // Metoda ChangePassword spowoduje wyjątek, zamiast zwrócić wartość false w pewnych scenariuszach błędu.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bieżące hasło jest niepoprawne lub nowe hasło jest nieprawidłowe.");
                    }
                }
            }
            else
            {
                // Użytkownik nie ma lokalnego hasła, więc usuń wszelkie błędy sprawdzania poprawności spowodowane brakiem
                // pola OldPassword
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Nie można utworzyć konta lokalnego. Być może istnieje już konto o nazwie „{0}”.", User.Identity.Name));
                    }
                }
            }

            // Dotarcie do tego miejsca wskazuje, że wystąpił błąd, wyświetl ponownie formularz
            return View(model);
        }

        #region Pomocnicy
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Na stronie http://go.microsoft.com/fwlink/?LinkID=177550 znajduje się
            // pełna lista kodów stanu.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Nazwa użytkownika już istnieje. Wprowadź inną nazwę użytkownika.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Nazwa użytkownika dla tego adresu e-mail już istnieje. Wprowadź inny adres e-mail.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Podane hasło jest nieprawidłowe. Wprowadź prawidłową wartość hasła.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Podany adres e-mail jest nieprawidłowy. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Podana odpowiedź dla funkcji odzyskiwania hasła jest nieprawidłowa. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Podane pytanie dla funkcji odzyskiwania hasła jest nieprawidłowe. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Podana nazwa użytkownika jest nieprawidłowa. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.ProviderError:
                    return "Dostawca uwierzytelniania zwrócił błąd. Sprawdź wpis i spróbuj ponownie. Jeśli problem nie zniknie, skontaktuj się z administratorem systemu.";

                case MembershipCreateStatus.UserRejected:
                    return "Żądanie utworzenia użytkownika zostało anulowane. Sprawdź wpis i spróbuj ponownie. Jeśli problem nie zniknie, skontaktuj się z administratorem systemu.";

                default:
                    return "Wystąpił nieznany błąd. Sprawdź wpis i spróbuj ponownie. Jeśli problem nie zniknie, skontaktuj się z administratorem systemu.";
            }
        }
        #endregion
    }
}
