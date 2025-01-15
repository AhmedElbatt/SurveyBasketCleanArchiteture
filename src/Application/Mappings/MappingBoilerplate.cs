namespace Application.Mappings;
public class MappingBoilerplate : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<src, dest>()
        //    .Map(dest => dest.Answers, src => src.Answers.Select(answer => new Answer { Content = answer }));
    }
}
