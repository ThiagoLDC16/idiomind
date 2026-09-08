namespace Idiomind.Domain.Simulations.Entities;

public sealed class UserProfile : BaseEntity
{
    public Guid UserId { get; private set; }
    public string NativeLanguage { get; private set; }
    public string LearningLanguage { get; private set; }
    public Guid? NextRecommendedSimulation { get; private set; }

    public UserProfile(Guid userId, string nativeLanguage, string learningLanguage)
    {
        UserId = userId;
        NativeLanguage = nativeLanguage;
        LearningLanguage = learningLanguage;
    }
}