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
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly IUnitOfWork iUnitOfWork;

        public GetEventByIdHandler(IUnitOfWork iUnitOfWork)
        {
            this.iUnitOfWork = iUnitOfWork;
        }
        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.iUnitOfWork.EventRepository.GetEventByIdAsync(request.id);
        }
    }
}
