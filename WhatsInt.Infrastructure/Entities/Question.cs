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
        public Question Create(string question)
        {
            List<string> errorList = new();

            var invalidQuestion = string.IsNullOrEmpty(question);

            if (invalidQuestion) errorList.Add("Question not found.");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            return new Question()
            {
                Description = question
            };

        }
    }
}
