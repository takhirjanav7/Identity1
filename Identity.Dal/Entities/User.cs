﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Dal.Entities;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public UserRole Role { get; set; }
    public string UserName { get; set; }

    
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}

public enum UserRole
{
    User,
    Admin,
    SuperAdmin
}

