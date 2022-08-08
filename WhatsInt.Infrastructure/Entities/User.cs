using System.Net;
using System.Text.RegularExpressions;
using WhatsInt.Infrastructure.Entities.Generic;
using WhatsInt.Infrastructure.Exceptions;
using WhatsInt.Infrastructure.Helpers;
using WhatsInt.Infrastructure.Resources;

namespace WhatsInt.Infrastructure.Entities
{
    public class User : Entity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public static User CreateOrUpdate(string? userName, string? userEmail, string? userPassword, string userId = "")
        {
            var errorMessages = new List<string>();

            var invalidPassword = string.IsNullOrEmpty(userPassword) || userPassword.Length < 6;

            if (invalidPassword)  errorMessages.Add(Messages.PasswordValidation);

            var nameLength = userName?.Trim().Split(' ').Length;

            if (nameLength is null or <= 1)  errorMessages.Add(Messages.NameValidation);

            var invalidEmail = userEmail == null || !Regex.IsMatch(userEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,10})+)$");

            if (invalidEmail)  errorMessages.Add(Messages.EmailValidation);

            var messageInline = string.Join(Environment.NewLine, errorMessages);

            if (errorMessages.Count > 0) throw new AppException(HttpStatusCode.BadRequest, messageInline);

            var passwordToSave = userPassword?.Base64Decode().Encrypt();

            return new User
            {
                Id = userId,
                Name = userName,
                Email = userEmail,
                Password = passwordToSave
            };
        }

    }
}
