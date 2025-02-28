using MediatR;

namespace Application.Commands.MasterPassword;

public class MasterPasswordCommandHandler : IRequestHandler<MasterPasswordCommand>
{
    public async Task Handle(MasterPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}