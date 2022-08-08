using WhatsInt.Model.Generic;

namespace WhatsInt.Model.Dto
{
    public class AnswerDto : InteractDto
    {
        public string? IdQuestion { get; set; }
        public string? IdNextQuestion { get; set; }
    }
}
