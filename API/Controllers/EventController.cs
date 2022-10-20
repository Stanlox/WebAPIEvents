using AutoMapper;
using Domain.Commands;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Queries;
using Domain.ViewModels;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IValidator<EventViewModel> validator;
        private IUnitOfWork unitOfWork;
        private readonly ISender sender;

        public EventController(IMapper mapper, IValidator<EventViewModel> validator, IUnitOfWork unitOfWork, ISender sender)
        {
            this.sender = sender;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            var events = await this.sender.Send(new GetEventsQuery());
            return Ok(this.mapper.Map<IEnumerable<EventViewModel>>(events));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var uniqueEvent = await this.sender.Send(new GetEventByIdQuery(id));
            if (uniqueEvent == null)
            {
                return BadRequest("Event is not found");
            }
            return Ok(mapper.Map<EventViewModel>(uniqueEvent));
        }

        [HttpPost, Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Event>> Add([FromBody] EventViewModel model)
        {
            var validationResult = await this.validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            var eventToBeAdded = mapper.Map<Event>(model);
            var newEvent = await this.sender.Send(new AddEventCommand(eventToBeAdded));
            return Ok(newEvent);
        }


        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<HttpResponseMessage>> Update(int id, [FromBody] EventViewModel model)
        {
            var eventToBeUpdated = await this.unitOfWork.EventRepository.GetEventByIdAsync(id);
            if (eventToBeUpdated == null)
            {
                return BadRequest("Event with Id = " + id.ToString() + " not found to update");
            }
            else
            {
                mapper.Map(model, eventToBeUpdated);
                await this.unitOfWork.EventRepository.UpdateAsync(eventToBeUpdated);
            }

            return Ok(mapper.Map<EventViewModel>(eventToBeUpdated));
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<HttpResponseMessage>> Delete(int id)
        {
            var eventToDelete = await this.unitOfWork.EventRepository.GetEventByIdAsync(id);
            if (eventToDelete == null)
            {

                return BadRequest("Event with Id = " + id.ToString() + "not found to update");
            }
            else
            {
                await this.unitOfWork.EventRepository.DeleteAsync(id);
            }

            return Ok();
        }
    }
}
