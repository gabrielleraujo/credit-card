using CreditCard.Application.ViewModels;
using MediatR;

namespace CreditCard.Application.Queries.GetAllCreditCard
{
    public class GetAllCreditCardsQuery: IRequest<IEnumerable<CreditCardViewModel>>
    {
    }
}