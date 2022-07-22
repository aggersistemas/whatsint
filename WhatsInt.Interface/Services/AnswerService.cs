using Infrastructure.Repository;
using WhatsInt.Infrastructure.Entities.Generic;
using WhatsInt.Interface.Exceptions;
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
            var answerDb = MapperHelper.Map<Answer>(answer);

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
    }
}
