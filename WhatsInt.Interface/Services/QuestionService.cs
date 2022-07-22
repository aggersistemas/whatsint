using Infrastructure.Repository;
using System.Net;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Interface.Exceptions;
using WhatsInt.Interface.Helpers;
using WhatsInt.Model;

namespace WhatsInt.Interface.Services
{
    public class QuestionService
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IHttpContextAccessor _context;

        public QuestionService(IRepository<Question> questionRepository, IHttpContextAccessor context)
        {
            _questionRepository = questionRepository;
            _context = context; 
        }

        internal async Task<QuestionDto?> FindQuestionById(string id)
        {
            var questionFound = await _questionRepository.FindOne(x => x.Id == id);

            if (questionFound == null)
                throw new AppException(HttpStatusCode.NotFound, "Question not found");

            return MapperHelper.Map<QuestionDto?>(questionFound); 
        }
    }

}
