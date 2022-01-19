

var event_data = {
    "events": [

    ]
};
const modal = new bootstrap.Modal(document.getElementById("modal"),
    {
        keyboard: false,
        backdrop: "static"
    });
var nuevo = document.getElementById("nuevo");

var datos = [];









const listar = async () => {



    const res = await fetch('https://localhost:7157/Calendario/citas');

    const data = await res.json();


    datos = data;

    data.forEach(res => {

        var obj = {

            "idCita": res.idCita,
            "paciente": res.nombresPaciente,
            "doctor": res.nombresDoctor,    
            "seguro": res.nombresSeguros,
            "year": parseInt(res.anio),
            "month": parseInt(res.mes),
            "day": parseInt(res.dia),
            "hora": res.hora,
            "especialidad": res.nombresEspecialidad,
            "estado":res.estado

        }

        event_data.events.push(obj);



    });

    $('#next').click();
    $('#prev').click();




}

const mostrarModal = async (id) => {

 
    const unDato = datos.filter(x => x.idCita == id);


    document.getElementById("doctor").innerText = unDato[0].nombresDoctor;
    document.getElementById("seguro").innerText = unDato[0].nombresSeguros  ;
    document.getElementById("paciente").innerText = unDato[0].nombresPaciente;
    document.getElementById("especialidad").innerText = unDato[0].nombresEspecialidad
    document.getElementById("idCita").innerText = unDato[0].idCita;
    


    modal.show();

}



const salir = async () => {



    var salir = document.getElementById("salir");
    modal.hide();

}

listar();










(function ($) {

    const months = [
        "Enero",
        "Febrero",
        "Marzo",
        "Abril",
        "Mayo",
        "Junio",
        "Julio",
        "Agosto",
        "Septiembre",
        "Octubre",
        "Noviembre",
        "Diciembre"
    ];






    "use strict";

    // Setup the calendar with the current date
    $(document).ready(function () {
        var date = new Date();
        var today = date.getDate();
        // Set click handlers for DOM elements
        $(".right-button").click({ date: date }, next_year);
        $(".left-button").click({ date: date }, prev_year);
        $(".month").click({ date: date }, month_click);
        $("#add-button").click({ date: date }, new_event);
        // Set current month as active
        $(".months-row").children().eq(date.getMonth()).addClass("active-month");
        init_calendar(date);
        var events = check_events(today, date.getMonth() + 1, date.getFullYear());
        show_events(events, months[date.getMonth()], today);
    });

    // Initialize the calendar by appending the HTML dates
    function init_calendar(date) {
        $(".tbody").empty();
        $(".events-container").empty();
        var calendar_days = $(".tbody");
        var month = date.getMonth();
        var year = date.getFullYear();
        var day_count = days_in_month(month, year);
        var row = $("<tr class='table-row'></tr>");
        var today = date.getDate();
        // Set date to 1 to find the first day of the month
        date.setDate(1);
        var first_day = date.getDay();
        // 35+firstDay is the number of date elements to be added to the dates table
        // 35 is from (7 days in a week) * (up to 5 rows of dates in a month)
        for (var i = 0; i < 35 + first_day; i++) {
            // Since some of the elements will be blank, 
            // need to calculate actual date from index
            var day = i - first_day + 1;
            // If it is a sunday, make a new row
            if (i % 7 === 0) {
                calendar_days.append(row);
                row = $("<tr class='table-row'></tr>");
            }
            // if current index isn't a day in this month, make it blank
            if (i < first_day || day > day_count) {
                var curr_date = $("<td class='table-date nil'>" + "</td>");
                row.append(curr_date);
            }
            else {
                var curr_date = $("<td class='table-date'>" + day + "</td>");
                var events = check_events(day, month + 1, year);
                if (today === day && $(".active-date").length === 0) {
                    curr_date.addClass("active-date");
                    show_events(events, months[month], day);
                }
                // If this date has any events, style it with .event-date
                if (events.length !== 0) {
                    curr_date.addClass("event-date");
                }
                // Set onClick handler for clicking a date
                curr_date.click({ events: events, month: months[month], day: day }, date_click);
                row.append(curr_date);
            }
        }
        // Append the last row and set the current year
        calendar_days.append(row);
        $(".year").text(year);



    }

    // Get the number of days in a given month/year
    function days_in_month(month, year) {
        var monthStart = new Date(year, month, 1);
        var monthEnd = new Date(year, month + 1, 1);
        return (monthEnd - monthStart) / (1000 * 60 * 60 * 24);
    }

    // Event handler for when a date is clicked
    function date_click(event) {
        $(".events-container").show(250);
        $("#dialog").hide(250);
        $(".active-date").removeClass("active-date");
        $(this).addClass("active-date");
        show_events(event.data.events, event.data.month, event.data.day);
    };

    // Event handler for when a month is clicked
    function month_click(event) {
        $(".events-container").show(250);
        $("#dialog").hide(250);
        var date = event.data.date;
        $(".active-month").removeClass("active-month");
        $(this).addClass("active-month");
        var new_month = $(".month").index(this);
        date.setMonth(new_month);
        init_calendar(date);
    }

    // Event handler for when the year right-button is clicked
    function next_year(event) {



        $("#dialog").hide(250);
        var date = event.data.date;
        var new_year = date.getFullYear() + 1;
        $("year").html(new_year);
        date.setFullYear(new_year);
        init_calendar(date);
    }

    // Event handler for when the year left-button is clicked
    function prev_year(event) {
        $("#dialog").hide(250);
        var date = event.data.date;
        var new_year = date.getFullYear() - 1;
        $("year").html(new_year);
        date.setFullYear(new_year);
        init_calendar(date);
    }

    // Event handler for clicking the new event button
    function new_event(event) {
        // if a date isn't selected then do nothing
        if ($(".active-date").length === 0)
            return;
        // remove red error input on click
        $("input").click(function () {
            $(this).removeClass("error-input");
        })
        // empty inputs and hide events
        $("#dialog input[type=text]").val('');
        $("#dialog input[type=number]").val('');
        $(".events-container").hide(250);
        $("#dialog").show(250);
        // Event handler for cancel button
        $("#cancel-button").click(function () {
            $("#name").removeClass("error-input");
            $("#count").removeClass("error-input");
            $("#dialog").hide(250);
            $(".events-container").show(250);
        });
        // Event handler for ok button
        $("#ok-button").unbind().click({ date: event.data.date }, function () {
            var date = event.data.date;
            var name = $("#name").val().trim();
            var count = parseInt($("#count").val().trim());
            var day = parseInt($(".active-date").html());
            // Basic form validation
            if (name.length === 0) {
                $("#name").addClass("error-input");
            }
            else if (isNaN(count)) {
                $("#count").addClass("error-input");
            }
            else {
                $("#dialog").hide(250);
                // console.log("new event");
                new_event_json();
                date.setDate(day);
                init_calendar(date);
            }
        });
    }

    // Adds a json event to event_data
    function new_event_json() {


        var event = {
            "occasion": "name",
            "usuario": "Anthony Morante",
            "invited_count": "count",
            "year": 2021,
            "month": 11,
            "day": 28,
            "hora": "00:00",
        };
        event_data["events"].push(event);




        // var event = {
        //     "occasion": "Fiesta de pussy",
        //     "invited_count": "140",
        //     "year": "date.getFullYear()",
        //     "month": "date.getMonth() + 1",
        //     "day": "Miercoles",
        //     "hora": "00:00",
        // };
        // event_data["events"].push(event);
    }

    // Display all events of the selected date in card views
    async function show_events(events, month, day) {




        // Clear the dates container

        $(".events-container").empty();
        $(".events-container").show(250);
        // If there are no events for this date, notify the user
        if (events.length === 0) {
            var event_card = $("<div class='event-card'></div>");
            var event_name = $("<div class='event-name'>Sin citas para  " + month + " " + day + ".</div>");
            $(event_card).css({ "border-left": "10px solid #FF1744" });
            $(event_card).append(event_name);
            $(".events-container").append(event_card);
        }

        //aqui se manda a llamar a la funcion que muestra los eventos

        else {
            // Go through and add each event as a card to the events container
            for (var i = 0; i < events.length; i++) {
                var event_card = $("<div class='event-card'></div>");
                var datos = $(`<div class='row'> 


                         <div class='col-12'>
                         <strong class='ms-2'>Paciente:</strong> <label class='ml-2 '> ${events[i]["paciente"]} </label>
                         </div>    
                         <div class='col-12'>
                         <strong class='ms-2'>Doctor:</strong> <label class='ml-2 '> ${events[i]["doctor"]} </label>
                         </div>

                         <div class='col-12'>
                         <strong class='ms-2'>Especialidad:</strong> <label class='ml-2 '> ${events[i]["especialidad"]} </label>
                         </div>
                         <div class='col-12'>
                         <strong class='ms-2'>Seguro:</strong> <label class='ml-2 '> ${events[i]["seguro"]} </label>
                         </div>   
                         <div class='col-12'>
                         <strong class='ms-2'>Hora:</strong> <label class='ml-2 '> ${events[i]["hora"]} </label>
                         </div>


                       

                         <div class='col-12'>
                        

                         ${(events[i]["estado"] == 0) ? `<div class='ms-2 text-warning'> Pendiente </div>`

                       : (events[i]["estado"] == 1) ? `<div class='ms-2 text-success'> Atendido </div>`

                        : (events[i]["estado"] == 2) ? `<div class='ms-2 text-danger'> Cancelado </div>` : events[i]["estado"]


                          }

                         </div>


                               ${(events[i]["estado"] != 0) ? `<div> </div>` 

                    : ` 

                             <div class='col-sm-12  text-center  '>


                             <a id="nuevo" name="nuevo" type="button" class="btn btn-primary col-sm-4  ms-auto me-auto" onclick="mostrarModal(${events[i]["idCita"]})">Registar Cita</a>
                                     </div>

                             </div>

                            <div class='col-sm-12  text-center mt-2  '>


                             <a id="cancelar" name="cancelar" type="button" class="btn btn-danger col-sm-4  ms-auto me-auto" onclick="cancelar(${events[i]["idCita"]})">Cancelar</a>
                                     </div>

                            </div>

                 
                                


                                   





                                 `}

                       `);


                var event_name = $("<div class='event-name'>" + events[i]["occasion"] + ":</div>");
                var event_count = $("<div class='event-count'>" + events[i]["invited_count"] + " Invited</div>");
                var event_hora = $("<div class='event-count'>" + events[i]["hora"] + " Invited</div>");
                if (events[i]["cancelled"] === true) {
                    $(event_card).css({
                        "border-left": "10px solid #FF1744"
                    });
                    event_count = $("<div class='event-cancelled'>Cancelled</div>");
                }
                $(event_card).append(datos);
                $(".events-container").append(event_card);
            }
        }
    }

    // Checks if a specific date has any events


    function check_events(day, month, year) {



        var events = [];
        for (var i = 0; i < event_data["events"].length; i++) {
            var event = event_data["events"][i];
            if (event["day"] === day &&
                event["month"] === month &&
                event["year"] === year) {
                events.push(event);
            }
        }
        return events;
    }













})(jQuery);
