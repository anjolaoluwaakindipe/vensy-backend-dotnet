namespace Vensy.Contracts.Authentication.Request;


public record RegisterRequest(string Email, string Password, string Firstname, string Lastname, string Username);