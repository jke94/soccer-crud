﻿namespace SoccerCrud.WebApi.Contracts
{
    public class CreateUserResponse
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
