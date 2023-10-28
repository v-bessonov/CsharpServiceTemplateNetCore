﻿using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Member : Entity<Guid>
{
    public FirstName FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Email { get; set; }

    public Member(Guid id, FirstName firstName, string lastName, string email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}