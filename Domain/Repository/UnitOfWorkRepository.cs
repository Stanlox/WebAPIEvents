using Domain.EFData;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private ApplicationDbContent dbcontent;
        private IEventRepository eventRepository;
        public UnitOfWorkRepository(ApplicationDbContent dbcontent)
        {
            this.dbcontent = dbcontent;
        }
        public IEventRepository EventRepository
        {
            get
            {
                return eventRepository = eventRepository ?? new EventRepository(dbcontent);
            }
        }
    }
}
