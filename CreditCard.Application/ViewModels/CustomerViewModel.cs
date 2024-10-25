using System.Text.Json.Serialization;
using CreditCard.Domain.Models.Entities;

namespace CreditCard.Application.ViewModels;

public record CustomerViewModel : BaseEntityViewModel
{
    public CustomerViewModel(
        Guid id, DateTime createAt, DateTime? lastUpdate,
        string firstName, string lastName, string mainEmail)
        : base(id, createAt, lastUpdate)
    {
        FirstName = firstName;
        LastName = lastName;
        MainEmail = mainEmail;
    }

    [JsonPropertyName("first_name")]
    public string FirstName { get; private set; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string LastName { get; private set; } = string.Empty;

    [JsonPropertyName("main_email")]
    public string MainEmail { get; private set; } = string.Empty;
    
    public static CustomerViewModel MapFromDomain(CustomerEntity entity)
    {
        var CreditCardViewModel = new CustomerViewModel(
            entity.Id,
            entity.CreateAt,
            entity.LastUpdate,
            entity.Name.First,
            entity.Name.Last,
            entity.MainEmail.Text
        );
        return CreditCardViewModel;
    }

    public static IEnumerable<CustomerViewModel> MapFromDomain(IList<CustomerEntity> entity)
    {
        foreach (var item in entity)
        {
            yield return MapFromDomain(item);
        }
    }
}
