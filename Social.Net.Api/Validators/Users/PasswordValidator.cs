using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Social.Net.Api.Validators.Users;

public partial class PasswordAttribute(int minLength = 6,
    int maxLength = 12,
    bool digit = true,
    bool uppercase = true,
    bool lowercase = true,
    bool symbol = true)
    : ValidationAttribute
{
    [GeneratedRegex(@"\d")]
    private static partial Regex DigitChecker();
    
    [GeneratedRegex("[A-Z]")]
    private static partial Regex LowercaseChecker();
    
    [GeneratedRegex("[a-z]")]
    private static partial Regex UppercaseChecker();
    
    [GeneratedRegex("[$#@!^&]")]
    private static partial Regex SymbolChecker();
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string password || string.IsNullOrWhiteSpace(password))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field is required.");
        }
    
        const string digitPattern = @"\d";
        const string upperPattern = @"[A-Z]";
        const string lowerPattern = @"[a-z]";
        const string symbolPattern = @"[$#@!^&]";
    
        if (password.Length < minLength)
        {
            return new ValidationResult($"The {validationContext.DisplayName} field length must be greater than {minLength} characters");
        }
        else if (password.Length > maxLength)
        {
            return new ValidationResult($"The {validationContext.DisplayName} field length must be less than {maxLength} characters");
        }
        else if (digit && !DigitChecker().IsMatch(password))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field must contain at least one digit");
        }
        else if (uppercase && !LowercaseChecker().IsMatch(password))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field must contain at least one uppercase alphabet");
        }
        else if (lowercase && !UppercaseChecker().IsMatch(password))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field must contain at least one lowercase alphabet");
        }
        else if (symbol && !SymbolChecker().IsMatch(password))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field must contain at least one symbol from the following: $, #, @, !, ^, &");
        }
    
        return ValidationResult.Success;
    }
}