using Domain.EFData;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContent dbcontent;
        public EventRepository(ApplicationDbContent dbcontent)
        {
            this.dbcontent = dbcontent;
        }
        public async Task<IEnumerable<Event>> GetAsync()
        {
            return await dbcontent.Event.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await dbcontent.Event.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var eventToBeDeleted = await dbcontent.Event.FindAsync(id);
            if (eventToBeDeleted != null)
            {
                dbcontent.Event.Remove(eventToBeDeleted);
                await dbcontent.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Event newEvent)
        {
            await dbcontent.Event.AddAsync(newEvent);
            dbcontent.SaveChanges();
        }

        public async Task UpdateAsync(Event eventToBeUpdated)
        {
            dbcontent.Attach(eventToBeUpdated);
            dbcontent.Entry(eventToBeUpdated).State = EntityState.Modified;
            await dbcontent.SaveChangesAsync();
        }
    }
}
