using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedHome.Application.Common.User;
using SharedHome.Shared.Time;
using SharedHome.Domain.Primivites;

namespace SharedHome.Infrastructure.EF.Interceptors
{
    public sealed class AuditableInterceptor : SaveChangesInterceptor
    {
        private const string System = "SYSTEM";

        private readonly ITimeProvider _time;
        private readonly ICurrentUser _currentUser;

        public AuditableInterceptor(ITimeProvider time, ICurrentUser currentUser)
        {
            _time = time;
            _currentUser = currentUser;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _time.CurrentDate();
                        entry.Entity.CreatedBy = GetCreatedBy(entry);
                        entry.Entity.CreatedByFullName = GetCreatedByFullName(entry);
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = _time.CurrentDate();
                        entry.Entity.ModifiedBy = _currentUser.UserId == Guid.Empty ? null : _currentUser.UserId;
                        entry.Entity.ModifiedByFullName = GetModifiedByFullName();
                        break;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private string GetCreatedByFullName(EntityEntry<IAuditableEntity> entry)
        {
            if (!string.IsNullOrEmpty(_currentUser?.FirstName))
            {
                return $"{_currentUser.FirstName} {_currentUser.LastName}";
            }

            if (string.IsNullOrEmpty(entry.Entity.CreatedByFullName))
            {
                return System;
            }

            // Used for initializers
            return entry.Entity.CreatedByFullName;
        }

        public Guid? GetCreatedBy(EntityEntry<IAuditableEntity> entry)
        {
            if (entry.Entity.CreatedBy != Guid.Empty)
            {
                return entry.Entity.CreatedBy;
            }

            if (_currentUser.UserId == Guid.Empty)
            {
                return null;
            }

            return _currentUser.UserId;
        }

        private string GetModifiedByFullName()
        {
            if (!string.IsNullOrEmpty(_currentUser?.FirstName))
            {
                return $"{_currentUser.FirstName} {_currentUser.LastName}";
            }

            return System;
        }
    }
}
