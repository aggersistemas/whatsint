﻿using Infrastructure.Entities.Generic;
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

        public static Answer Create(string answerDescription, string answerOrder)
        {
            List<string> errorList = new();

            var invalidAnswer = string.IsNullOrEmpty(answerDescription);

            if (invalidAnswer) errorList.Add("Invalid Description");

            var invalidOrder = string.IsNullOrEmpty(answerOrder);

            if (invalidOrder) errorList.Add("Invalid Order");

            if (errorList.Count > 0) throw new AppException(HttpStatusCode.BadRequest, string.Join("\n", errorList));

            return new()
            {
                Description = answerDescription,
                Order = answerOrder
            };
        }
    }
}
