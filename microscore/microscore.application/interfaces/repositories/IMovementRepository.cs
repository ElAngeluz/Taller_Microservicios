﻿using microscore.application.interfaces.abstractapp;
using microscore.domain.entities.Accounts;

namespace microscore.application.interfaces.repositories
{
    public interface IMovementRepository : IGenericRepositoryAsync<Movement>
    {
        Task<IEnumerable<Movement>> GetAllAsync(DateTime DateTrx, string ClientName);
    }
}
