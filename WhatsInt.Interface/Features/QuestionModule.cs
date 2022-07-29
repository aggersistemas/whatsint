﻿using Carter;
using WhatsInt.Interface.Services;
using WhatsInt.Model;

namespace WhatsInt.Interface.Features
{
    public class QuestionModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string basePath = "/question";

            app.MapPost($"{basePath}/create", CreateQuestion);
            //app.MapPut($"{basePath}/update", UpdateQuestion);
            app.MapGet($"{basePath}/find", FindQuestion);
        }

        private async Task<IResult> FindQuestion(HttpContext context, QuestionService service, string id)
        {
            var question = await service.FindQuestionById(id);

            return Results.Ok(question);
        }

        private async Task<IResult> CreateQuestion(HttpContext context, QuestionService service, QuestionDto question)
        {
            var newQuestion = await service.Created(question);

            return Results.Created("Question Created", newQuestion);
        }

    }
}