using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class GetEventsHandler : IRequestHandler<GetEventsQuery, IEnumerable<Event>>
    {
        private readonly IUnitOfWork iUnitOfWork;
        public GetEventsHandler(IUnitOfWork iUnitOfWork)
        {
            this.iUnitOfWork = iUnitOfWork;
        }

        public async Task<IEnumerable<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return await this.iUnitOfWork.EventRepository.GetAsync();
        }
    }
}
