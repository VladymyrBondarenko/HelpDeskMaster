﻿namespace HelpDeskMaster.Persistence.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
