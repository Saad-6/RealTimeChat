// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using RealTimeChat.Models;

namespace RealTimeChat.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public string? AppUserName { get; set; }

            public string ?ProfileUrl { get; set; }
            public string? FullName { get; set; }
            
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.AppUserName = Input.AppUserName;
                user.ProfileUrl = Input.ProfileUrl;
                user.Email = Input.Email;
                user.FullName = Input.FullName;
                
                if(Input.ProfileUrl == null)
                {
                    Input.ProfileUrl ="data:image/png;base64,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";

                }
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
    }
}
