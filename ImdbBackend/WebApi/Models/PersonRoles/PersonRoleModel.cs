﻿
using System.Globalization;
using WebApi.Models.Ratings;
using WebApi.Models.Roles;
using WebApi.Models.TitlePrincipals;

namespace WebApi.Models.PersonRoles
{
    public class PersonRoleModel
    {
        public string? Url { get; set; }
        
        public int RoleId { get; set; }

        public string personUrl { get; set; } = string.Empty;

        public RoleModel? Role { get; set; }

    }
}
