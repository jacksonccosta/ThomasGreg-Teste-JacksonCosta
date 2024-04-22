using MediatR;

namespace ThomasGregTest.Application;

public abstract class Request<TResponse> : IRequest<TResponse>
{ 
}
