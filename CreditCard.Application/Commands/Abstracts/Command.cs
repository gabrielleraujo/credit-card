using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace CreditCard.Application.Commands;

public abstract class Command
{
    protected Command()
    {
        ValidationResult = new ValidationResult();
    }

    [JsonIgnore]
    public ValidationResult ValidationResult { get; protected set; }

    public abstract bool IsValid();
}