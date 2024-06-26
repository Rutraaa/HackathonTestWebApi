﻿using Supabase.Postgrest.Attributes;

namespace BackEnd.Contracts.Consumer;

public class CreatingUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool isSupplier { get; set; }
}