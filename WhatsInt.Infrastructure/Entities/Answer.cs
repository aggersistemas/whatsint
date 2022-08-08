using System.Net;
using WhatsInt.Infrastructure.Entities.Generic;
using WhatsInt.Infrastructure.Exceptions;

namespace WhatsInt.Infrastructure.Entities
{
    public class Answer : Interact
    {
        public string? IdQuestion { get; set; }
        public string? IdNextQuestion { get; set; }

        public static Answer CreateOrUpdate(string answerDescription, int answerOrder,  string questionId, string? answerId = null)
        {
            List<string> errorList = new();

            var invalidAnswer = string.IsNullOrEmpty(answerDescription);

            if (invalidAnswer) errorList.Add("Invalid Description");

            var invalidOrder = answerOrder == 0;

            if (invalidOrder) errorList.Add("Invalid Order");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            return new()
            {
                Id = answerId ?? string.Empty,
                Description = answerDescription,
                Order = answerOrder,
                IdQuestion = questionId,
            };
        }
    }
}
