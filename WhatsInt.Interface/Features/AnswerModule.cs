﻿using Carter;
using WhatsInt.Interface.Services;
using WhatsInt.Model;


namespace WhatsInt.Interface.Features
{
    public class AnswerModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string basepath = "/answer";

            app.MapPost($"{basepath}/create", CreateAnswer);
        }

        private async Task<IResult> CreateAnswer(HttpContext context, AnswerService service, AnswerDto answer)
        {
            var createdAnswer = await service.Create(answer);

            return Results.Created("", createdAnswer);
        }

    }

}
