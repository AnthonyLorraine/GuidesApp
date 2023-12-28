using System;
using Microsoft.AspNetCore.Identity;

namespace GuidesApp.Services.AuthAPI.Services
{
    public class CustomPasswordValidator<TUser> : PasswordValidator<TUser> where TUser : class
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {
            if (password == null)
            {
                throw new Exception("Password provided is null");
            }

            var result = await base.ValidateAsync(manager, user, password);

            var errors = result.Errors.ToList();

            if (password.Length < 13)
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordTooShort",
                    Description = "Password must be at least 13 characters long."
                });
            }

            if (password.Length > 35)
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordTooLong",
                    Description = "Password cannot exceed 35 characters."
                });
            }

            return errors.Count != 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }
    }
}

