using Infrastructure.Repository;
using System.Net;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Infrastructure.Exceptions;
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

        public async Task<QuestionDto> Created(QuestionDto question)
        {
            if(question.Description == string.Empty)
                throw new AppException(HttpStatusCode.NotAcceptable, "Question is empty");

            var teste = _questionRepository.ToList();

            var questionFound = await _questionRepository.FindOne(x => x.Order == question.Order);

            if(questionFound != null)
                throw new AppException(HttpStatusCode.Conflict, "Order already exists");

            //var questionDb = MapperHelper.Map<Question>(question);

            var questionDb = Question.Create(question.Description);

            await _questionRepository.Add(questionDb);

            return MapperHelper.Map<QuestionDto>(questionDb);
            
        }

        internal async Task<QuestionDto> Update(QuestionDto question)
        {
            var questionUpdate = await _questionRepository.FindOne(x => x.Id == question.Id);

            var teste = _questionRepository.ToList();

            if (questionUpdate == null)
                throw new AppException(HttpStatusCode.NotFound, "Question not found");

            var questionDb = Question.Update(question.Description, question.Order, question.Id);

            await _questionRepository.Update(questionDb);

            return MapperHelper.Map<QuestionDto>(questionDb);

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
