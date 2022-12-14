using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;
using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Utils;

namespace ShiftsTrackerRestApi.Models;

public class User
{
    public int Id { get; set; }
    public string? NfcCardId { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Email { get; set; }
    public Role Role { get; set; }
    
    public void LastNameValidation()
    {
        if (string.IsNullOrEmpty(LastName)) throw new ArgumentNullException( nameof(LastName), "Last name cannot be null or empty.");
        
        if (LastName.Length > Constants.MAX_NAME_LENGTH) throw new ArgumentOutOfRangeException(nameof(LastName.Length),"Last name cannot be longer than 50 characters");
        
        if(LastName.Length < Constants.MIN_NAME_LENGTH) throw new ArgumentOutOfRangeException(nameof(FirstName.Length),"Last name cannot be shorter than 3 characters");
        
        if (LastName.Any(char.IsDigit)) throw new ValidationException("Last name cannot contain digits");

        if (LastName.Any(char.IsPunctuation)) throw new ValidationException("Last name cannot contain punctuation");
    }
    
    public void FirstNameValidation()
    {
        if (string.IsNullOrEmpty(FirstName)) throw new ArgumentNullException( nameof(FirstName), "First name cannot be null or empty.");
        
        if (FirstName.Length > Constants.MAX_NAME_LENGTH) throw new ArgumentOutOfRangeException(nameof(FirstName.Length),"First name cannot be longer than 50 characters");
        
        if(FirstName.Length < Constants.MIN_NAME_LENGTH) throw new ArgumentOutOfRangeException(nameof(FirstName.Length),"First name cannot be shorter than 3 characters");
        
        if (FirstName.Any(char.IsDigit)) throw new ValidationException("First name cannot contain digits");

        if (FirstName.Any(char.IsPunctuation)) throw new ValidationException("First name cannot contain punctuation");

    }
    
    public void EmailValidation()
    {
        var email = Email?.Trim();
        if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email),"Email cannot be empty");
        if (email.Length > Constants.MAX_EMAIL_LENGTH) throw new ArgumentOutOfRangeException(nameof(email.Length),"Email cannot be longer than 50 characters");
        Regex regex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        Match match = regex.Match(email ?? string.Empty);
        if (!match.Success) throw new ArgumentException("Email is not valid");
        
    }
    
    public void RoleValidation()
    {
        if (false == Enum.IsDefined(typeof(Role), Role)) throw new ArgumentException("Role is not valid");
    }

    public void Validator()
    {
        FirstNameValidation();
        LastNameValidation();
        EmailValidation();
        RoleValidation();
    }
}