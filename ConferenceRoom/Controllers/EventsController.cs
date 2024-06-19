// Controllers/EventsController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace ConferenceRoom.Controllers
{
    public class EventsController : Controller
    {
        private static List<object> _events = new List<object>
        {
            new { day = "Sunday", time = "3:00pm", title = "big party", color = "#c0c0c0" },
            new { day = "Monday", time = "12:00pm", title = "Another event", color = "#8FD8D8" },
            new { day = "Thursday", time = "5:00pm", title = "This is what happens when", color = "orange" },
            new { day = "Thursday", time = "5:30pm", title = "two events are side by side", color = "purple" }
        };

        [HttpGet]
        public JsonResult GetEvents()
        {
            var weekdays = new List<string>();
            var startDate = new DateTime(2024, 4, 13); // Example start date
            for (int i = 0; i < 7; i++)
            {
                weekdays.Add(startDate.AddDays(i).ToString("ddd (MMM dd)"));
            }

            return Json(new { events = _events, weekdays = weekdays });
        }
    }
}
