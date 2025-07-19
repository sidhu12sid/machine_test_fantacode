﻿namespace login_app.Interfaces
{
    public interface IPasswordHashingService
    {
        string HashPassword(string? password);
        bool VerifyPassword(string? password, string? passwordHash);
    }
}
