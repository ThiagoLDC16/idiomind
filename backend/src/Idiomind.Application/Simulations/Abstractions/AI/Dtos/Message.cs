namespace Idiomind.Application.Simulations.Abstractions.AI.Dtos;

public sealed record Message(
    MessageSender Sender,
    string Content
);