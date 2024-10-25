using CreditCard.Application.Queries.GetAllCreditCard;
using CreditCard.Application.ViewModels;
using CreditCard.Domain.Repositories;
using MediatR;

namespace CreditCard.Application.Queries.GetAllCreditCardsQ
{
    public class GetAllCreditCardsQueryHandler : IRequestHandler<GetAllCreditCardsQuery, IEnumerable<CreditCardViewModel>>
    {
        private readonly ICreditCardRepository _repository;

        public GetAllCreditCardsQueryHandler(
            ICreditCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CreditCardViewModel>> Handle(GetAllCreditCardsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllAsync();

            if (entity == null) return new List<CreditCardViewModel>();

            var viewModel = CreditCardViewModel.MapFromDomain(entity);

            return viewModel;
        }
    }
}
