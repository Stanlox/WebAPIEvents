using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IEventRepository
    {
        /// <summary>
        /// Gets a list of all events.
        /// </summary>
        /// <returns>all events</returns> 
        Task<IEnumerable<Event>> GetAsync();

        /// <summary>
        /// Save a new event.
        /// </summary>
        /// <param name="newEvent">Input Event.</param>
        Task AddAsync(Event newEvent);

        /// <summary>
        /// Delete event by id.
        /// </summary>
        /// <param name="id">Input id.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Gets event by id.
        /// </summary>
        /// <param name="id">Input id.</param>
        Task<Event> GetEventByIdAsync(int id);

        /// <summary>
        /// Update already existing event
        /// </summary>
        /// <param name="model">Input event to be updated</param>
        /// <returns>Updated task</returns>
        Task UpdateAsync(Event model);
    }
}
