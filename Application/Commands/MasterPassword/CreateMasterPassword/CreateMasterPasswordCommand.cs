using MediatR;

namespace Application.Commands.MasterPassword.CreateMasterPassword;

public class CreateMasterPasswordCommand : IRequest
{
    public long ChatId { get; set; }
    public string? StepData { get; set; }
}