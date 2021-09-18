using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using Application.Activities;

namespace API.Controllers
{
    public class ActivitiesController : BaseAPIController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")] //let user select single activity through id
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id}); //set Id to id when initialise 'Details' class
        }

        public async Task<IActionResult> CreateActivity(Activity activity){ 
            //IActionResult used for command with HTTP return type
            return Ok(await Mediator.Send(new Create.Command {Activity=activity}));
        }

        [HttpPut("{id}")]
        public async  Task<IActionResult> EditActivity(Guid id, Activity activity){
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity=activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id){
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}