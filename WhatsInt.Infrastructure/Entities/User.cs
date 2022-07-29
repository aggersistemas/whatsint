using System.Net;
using Infrastructure.Entities.Generic;
using WhatsInt.Infrastructure.Exceptions;

namespace WhatsInt.Infrastructure.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public static User Create(string userName, string userEmail, string userPassword)
        {
            var errorList = new List<string>();

            var invalidName = string.IsNullOrEmpty(userName);

            if (invalidName) errorList.Add("Invalid user");

            var invalidEmail = string.IsNullOrEmpty(userEmail) || !userEmail.Contains("@") || !userEmail.Contains(".");

            if (invalidEmail) errorList.Add("Invalid Email");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            return new User()
            {
                Name = userName,
                Email = userEmail,
                Password = userPassword
            };
        }

    }
}
