using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhatsInt.Infrastructure.Entities.Generic;
using WhatsInt.Infrastructure.Exceptions;

namespace WhatsInt.Infrastructure.Entities
{
    public class Question : Interact
    {
        public static Question Create(string question)
        {
            List<string> errorList = new();

            var invalidQuestion = string.IsNullOrEmpty(question);

            if (invalidQuestion) errorList.Add("Invalid Question !");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            return new Question()
            {
                Description = question
            };

        }

        public static Question Update(string description, int order, string id)
        {
            List<string> errorList = new();

            var invalidQuestion = string.IsNullOrEmpty(description);

            if (invalidQuestion) errorList.Add("Invalid Question!");

            var invalidOrder = order == 0;

            if (invalidOrder) errorList.Add("Invalid Order!");

            var invalidId = string.IsNullOrEmpty(id);

            if (invalidId) errorList.Add("Invalid Id!");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            return new Question()
            {
                Description = description,
                Order = order,
                Id = id
            };

        }

    }
}
