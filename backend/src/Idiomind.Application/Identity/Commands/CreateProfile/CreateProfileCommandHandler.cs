namespace Idiomind.Application.Identity.Commands.CreateProfile;

public sealed class CreateProfileCommandHandler(
    IAppDbContext context,
    IRequestContext requestContext
) : IRequestHandler<CreateProfileCommand, Response>
{
    public async Task<Response> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        if (requestContext.UserId is null)
            return new UnauthorizedResponse(IdentityMessages.UserNotFound);

        var alreadyExists = await context.UserProfiles
            .AnyAsync(p => p.UserId == requestContext.UserId, cancellationToken);

        if (alreadyExists)
            return new ConflictResponse(IdentityMessages.ProfileAlreadyExists);

        var profile = new UserProfile(
            userId: requestContext.UserId.Value,
            nativeLanguage: request.NativeLanguage,
            learningLanguage: request.LearningLanguage
        );

        await context.UserProfiles.AddAsync(profile, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new CreatedResponse();
    }
}
