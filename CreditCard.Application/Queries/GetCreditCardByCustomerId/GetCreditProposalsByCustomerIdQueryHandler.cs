using CreditCard.Application.Queries.GetAllCreditCard;
using CreditCard.Application.ViewModels;
using CreditCard.Domain.Repositories;
using MediatR;

namespace CreditCard.Application.Queries.GetCreditCardByCustomerId
{
    public class GetCreditCardByCustomerIdQueryHandler : IRequestHandler<GetCreditCardByCustomerIdQuery, IEnumerable<CreditCardViewModel>>
    {
        private readonly ICreditCardRepository _repository;

        public GetCreditCardByCustomerIdQueryHandler(
            ICreditCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CreditCardViewModel>> Handle(GetCreditCardByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllCreditCardsByCustomerId(request.CustomerId);

            if (entity == null) return new List<CreditCardViewModel>();

            var viewModel = CreditCardViewModel.MapFromDomain(entity);

            return viewModel;
        }
    }
}
