//// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// wwwroot/js/pretty-calendar.js
//document.addEventListener('DOMContentLoaded', function () {
//    var calendarEl = document.getElementById('calendar');

//    // Fetch event data and weekdays from the server
//    fetch('/Events/GetEvents')
//        .then(response => response.json())
//        .then(data => {
//            var events = data.events.map(event => {
//                return [event.day, event.time, event.title, event.color];
//            });

//            var weekdays = data.weekdays;

//            // Initialize PrettyCalendar with the fetched events and weekdays
//            var prettyCal = new PrettyCalendar(events, "myCal", true, weekdays);

//            // Render the calendar
//            prettyCal.render();
//        })
//        .catch(error => console.error('Error fetching events:', error));
//});
