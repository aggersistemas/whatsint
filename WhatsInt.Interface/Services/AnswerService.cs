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
            var answerDb = Answer.Create(answer.Description, answer.Order);

            await _answerRepository.Add(answerDb);

            return MapperHelper.Map<AnswerDto>(answerDb);
        }

        internal async Task<AnswerDto> Update(AnswerDto answer)
        {
           var findAnswer = await _answerRepository.FindOne(x => x.Id == answer.Id);

           if (findAnswer == null) throw new AppException(System.Net.HttpStatusCode.NotFound, "Answer not found");

            var answerUpdate = MapperHelper.Map<Answer>(answer);

            await _answerRepository.Update(answerUpdate);

            return MapperHelper.Map<AnswerDto>(answer);
        }

        internal async Task<AnswerDto> Find(string idAnswer)
        {
            var answerFound = await _answerRepository.FindOne(x => x.Id == idAnswer);

            if(answerFound == null)
                throw new AppException(HttpStatusCode.NotFound, "Answer not found");

            return MapperHelper.Map<AnswerDto>(answerFound);
        }
    }
}
