using Microsoft.AspNetCore.Identity;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = "DuplicateEmail",
            Description = "Этот Email уже используется"
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = "DuplicateUserName",
            Description = "Пользователь с таким Email уже зарегистрирован"
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = "PasswordTooShort",
            Description = $"Пароль должен содержать минимум {length} символов"
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = "PasswordRequiresNonAlphanumeric",
            Description = "Пароль должен содержать хотя бы один специальный символ"
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = "PasswordRequiresDigit",
            Description = "Пароль должен содержать хотя бы одну цифру"
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = "PasswordRequiresUpper",
            Description = "Пароль должен содержать хотя бы одну заглавную букву"
        };
    }
}