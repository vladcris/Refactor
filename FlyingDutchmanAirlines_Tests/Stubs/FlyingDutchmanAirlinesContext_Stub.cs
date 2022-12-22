using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlyingDutchmanAirlines_Tests.Stubs
{
    public class FlyingDutchmanAirlinesContext_Stub : FlyingDutchmanAirlinesContext
    {
        public FlyingDutchmanAirlinesContext_Stub(DbContextOptions<FlyingDutchmanAirlinesContext> opt) : base(opt)
        {
            base.Database.EnsureDeleted();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> pendingChanges = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added);
            
            IEnumerable<Booking> bookings = pendingChanges.Select(e => e.Entity).OfType<Booking>();

            if(bookings.Any(e => e.CustomerId != 1))
                throw new Exception("Error");

            await base.SaveChangesAsync(cancellationToken);
            return 1; 

            //Method1
            // return (base.Bokings.First().CustomerId != 1) 
            //             ? throw new Exception("Error")
            //             : await base.SaveChangesAsync(cancellationToken);

            //Method2
            // Func<Task<int>>[] functions = {
            //     () => throw new Exception("Error"),
            //     async () => await base.SaveChangesAsync(cancellationToken)
            // };
            // return await functions[(int) base.Bokings.First().CustomerId!].Invoke();

            //Method3
            // switch(base.Bokings.First().CustomerId)
            // {
            //     case 1:
            //         return await base.SaveChangesAsync(cancellationToken);
            //     default:
            //         throw new Exception("Error");
            // }
                        
            //throw new CouldNotAddBookingToDatabaseException();
        }
    }
}