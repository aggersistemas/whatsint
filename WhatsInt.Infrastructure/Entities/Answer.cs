using Infrastructure.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhatsInt.Infrastructure.Exceptions;

namespace WhatsInt.Infrastructure.Entities.Generic
{
    public class Answer : Interact
    {
        public string IdQuestion { get; set; }
        public string IdNextQuestion { get; set; }

        public Answer Created(string answer)
        {
            List<string> errorList = new();

            var invalidAnswer = string.IsNullOrEmpty(answer);

            if (invalidAnswer) errorList.Add("Answer empty. Try again");

            if (string.IsNullOrEmpty(IdQuestion))
                errorList.Add("Question not found");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            return new()
            {
                IdQuestion = IdQuestion,
                IdNextQuestion = IdNextQuestion
            };
        }

    }
}
