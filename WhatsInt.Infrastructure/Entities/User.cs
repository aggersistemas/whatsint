using System.Net;
using Infrastructure.Entities.Generic;
using WhatsInt.Infrastructure.Exceptions;
using WhatsInt.Common.Helpers;

namespace WhatsInt.Infrastructure.Entities
{
    public class User : Entity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public static User CreateOrUpdate(string? userName, string? userEmail, string? userPassword, string userId = "")
        {
            var errorList = new List<string>();

            var invalidName = string.IsNullOrEmpty(userName);

            if (invalidName) errorList.Add("Invalid user");

            var invalidEmail = string.IsNullOrEmpty(userEmail) || !userEmail.Contains("@") || !userEmail.Contains(".");

            if (invalidEmail) errorList.Add("Invalid Email");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            var passwordEncrypted = userPassword.Base64Decode();

            return new User()
            {
                Id = userId,
                Name = userName,
                Email = userEmail,
                Password = passwordEncrypted
            };
        }

    }
}
