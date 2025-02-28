using MediatR;

namespace Application.Commands.MasterPassword;

public class MasterPasswordCommand : IRequest
{
    public long ChatId { get; set; }
    public string? StepData { get; set; }
}