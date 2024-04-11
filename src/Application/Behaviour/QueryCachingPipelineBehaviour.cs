using Application.Common;
using MediatR;

namespace Application.Behaviour
{
    internal sealed class QueryCachingPipelineBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery, IRequest<TResponse>
    {
        private readonly ICacheService _cacheService;

        public QueryCachingPipelineBehaviour(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return await _cacheService.GetOrCreateAsync(
                request.Key,
                _ => next(),
                request.Expiration,
                cancellationToken);
        }
    }
}