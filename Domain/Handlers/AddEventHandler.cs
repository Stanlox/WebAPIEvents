using Domain.Commands;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class AddEventHandler : IRequestHandler<AddEventCommand, Event>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Event> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            await this.unitOfWork.EventRepository.AddAsync(request.newEvent);

            return request.newEvent;
        }
    }
}
