﻿namespace Application.Features.Questions.Shared;
public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
{
    public QuestionRequestValidator()
    {
        RuleFor(x => x.Content)
         .Length(3, 1000);

        RuleFor(x => x.Answers)
            .NotNull();

        RuleFor(x => x.Answers)
         .Must(x => x.Count > 1)
         .When(x => x.Answers != null)
         .WithMessage("Question must have at least one answer");


        RuleFor(x => x.Answers)
         .Must(x => x.Distinct().Count() == x.Count)
         .When(x => x.Answers != null)
         .WithMessage("Duplicated answers for the same question not possible");
    }
}
