﻿namespace BusinessLight.Domain.Application
{
    using System;

    public interface IApplicationUserRole
    {
        /// <summary>UserId for the user that is in the role</summary>
        Guid UserId { get; set; }

        /// <summary>RoleId for the role</summary>
        Guid RoleId { get; set; }
    }
}