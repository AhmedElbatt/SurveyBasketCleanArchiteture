namespace Application.Mappings;
public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<CreateQuestionCommand, Question>()
        //    .Map(dest => dest.Answers, src => src.Answers.Select(x => new Answer { Content = x }));
    }
}
