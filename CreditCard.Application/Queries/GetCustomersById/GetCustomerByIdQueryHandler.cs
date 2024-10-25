using CreditCard.Application.ViewModels;
using CreditCard.Domain.Repositories;
using MediatR;

namespace CreditCard.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>
    {
        private readonly ICreditCardRepository _repository;

        public GetCustomerByIdQueryHandler(
            ICreditCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerViewModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindCustomerByAsync(x => x.Id == request.CustomerId);

            if (entity == null) return null;

            var viewModel = CustomerViewModel.MapFromDomain(entity);

            return viewModel;
        }
    }
}
