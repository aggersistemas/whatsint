using Infrastructure.Repository;
using System.Net;
using WhatsInt.Infrastructure.Entities.Generic;
using WhatsInt.Infrastructure.Exceptions;
using WhatsInt.Interface.Helpers;
using WhatsInt.Model;

namespace WhatsInt.Interface.Services
{
    public class AnswerService
    {
        private readonly IRepository<Answer> _answerRepository;
        private readonly IHttpContextAccessor _context;

        public AnswerService(IRepository<Answer> answerRepository, IHttpContextAccessor context)
        {
            _answerRepository = answerRepository;
            _context = context;
        }

        internal async Task<AnswerDto> Create(AnswerDto answer)
        {
            var answerDb = await _answerRepository.FindOne(x => x.Order == answer.Order && x.IdQuestion == answer.IdQuestion);

            if (answerDb != null) throw new AppException(HttpStatusCode.Conflict, "This order already exists");

            answerDb = Answer.CreateOrUpdate(answer.Description, answer.Order, answer.IdQuestion);

            await _answerRepository.Add(answerDb);

            return MapperHelper.Map<AnswerDto>(answerDb);
        }

        internal async Task<AnswerDto> Update(AnswerDto answer)
        {
            await FindById(answer.Id);

            var answerUpdate = Answer.CreateOrUpdate(answer.Description, answer.Order, answer.IdQuestion, answer.Id);

            await _answerRepository.Update(answerUpdate);

            return MapperHelper.Map<AnswerDto>(answerUpdate);
        }

        internal async Task<AnswerDto> Find(string idAnswer)
        {
            var answerFound = await FindById(idAnswer);

            return MapperHelper.Map<AnswerDto>(answerFound);
        }

        private async Task<Answer> FindById(string answerId)
        {
            var findAnswer = await _answerRepository.FindOne(x => x.Id == answerId);

            if (findAnswer == null) throw new AppException(HttpStatusCode.NotFound, "Answer not found");

            return findAnswer;
        }
    }
}
