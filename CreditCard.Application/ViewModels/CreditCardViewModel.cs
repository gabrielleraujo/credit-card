using System.Text.Json.Serialization;
using CreditCard.Domain.Models.Entities;

namespace CreditCard.Application.ViewModels;

public record CreditCardViewModel : BaseEntityViewModel
{
    public CreditCardViewModel(
        Guid id, DateTime createAt, DateTime? lastUpdate,
        Guid customerId,
        Guid proposalCreditId,
        decimal creditLimitReleased,
        string name,
        string description,
        string flag,
        bool isActive
    )
        : base(id, createAt, lastUpdate)
    {
        CustomerId = customerId;
        ProposalCreditId = proposalCreditId;
        CreditLimitReleased = creditLimitReleased;
        Name = name;
        Description = description;
        Flag = flag;
        isActive = isActive;
    }

    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; private set; }

    [JsonPropertyName("proposalCreditId")]
    public Guid ProposalCreditId { get; private set; }

    [JsonPropertyName("credit-limit-released")]
    public decimal CreditLimitReleased { get; private set; }

    [JsonPropertyName("name")]
    public string Name { get; private set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; private set; } = string.Empty;

    [JsonPropertyName("flag")]
    public string Flag { get; private set; } = string.Empty;

    [JsonPropertyName("is-active")]
    public bool IsActive { get; private set; }

    public static CreditCardViewModel MapFromDomain(CreditCardEntity entity) => 
        new CreditCardViewModel(
            entity.Id,
            entity.CreateAt,
            entity.LastUpdate,
            entity.Customer.Id,
            entity.ProposalCreditId,
            entity.CreditLimitReleased,
            entity.Name,
            entity.Description,
            entity.Flag,
            entity.IsActive
        );

    public static IEnumerable<CreditCardViewModel> MapFromDomain(IList<CreditCardEntity> entity)
    {
        foreach (var item in entity)
        {
            yield return MapFromDomain(item);
        }
    }
}
