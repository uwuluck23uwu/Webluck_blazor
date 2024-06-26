﻿using Microsoft.AspNetCore.Identity;
using Tangy_Models;

namespace WebLucky_Serwer.Service.IService
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAll();
        Task<bool> Add(RoleDTO roleDto);
        Task<bool> Update(RoleDTO roleDto);
        Task<bool> Delete(RoleDTO roleDto);
        Task<IdentityRole> Find(string id);
    }
}
