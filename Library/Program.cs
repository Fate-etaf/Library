// See https://aka.ms/new-console-template for more information
string password = "Pass@1234";
string hash = BCrypt.Net.BCrypt.HashPassword(password);
Console.WriteLine(hash);
