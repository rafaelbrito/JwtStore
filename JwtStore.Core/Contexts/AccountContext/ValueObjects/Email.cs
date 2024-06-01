using JwtStore.Core.Contexts.SharedContext.Exceptions;
using JwtStore.Core.Contexts.SharedContext.Extensions;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    public string Address { get; }
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new();

    public void ResetVerification() 
        => Verification = new Verification();

    protected Email()
    {
    }
    public Email(string address)
    {
        Address = address.ToLower().Trim();
        InvalidEmailException.ThrowIfInvalid(Address);
    }
   
    public static implicit operator string(Email email)
        => email.ToString();

    public static implicit operator Email(string address)
        => new Email(address);
    public override string ToString()
        => Address;
}