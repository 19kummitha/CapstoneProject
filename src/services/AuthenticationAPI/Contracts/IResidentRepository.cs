﻿using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;

namespace AuthenticationAPI.Contracts
{
    public interface IResidentRepository
    {
        public Task<IEnumerable<GetResidentDto>> GetAllResidents();
    }
}
