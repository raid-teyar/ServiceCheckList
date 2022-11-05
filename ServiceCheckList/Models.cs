using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceCheckList
{

    public class DataClass
    {
        public bool Startup { get; set; }
        public int TicketsId { get; set; } = 1;
        //public bool NewInstall { get; set; }

        public List<Ticket> Tickets = new List<Ticket>();

    }

    public class Ticket
    {
        public List<TaskClass> Tasks = new List<TaskClass>();
        public NoteClass Note { get; set; }
        public NoteClass DropOffNote { get; set; }
        public bool Completed { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TicketNo { get; set; }
        public bool TicketDisabled { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool NewInstall { get; set; }

        public string SysKey { get; set; } = "";
    }

    public class NoteClass
    {
        public string Text { get; set; }
        public DateTime LastEventDate = new DateTime(1901, 1, 1, 0, 0, 0);
    }

    public class TaskClass
    {
        public string TaskName { get; set; } = "";
        public int Status { get; set; } = 0;
        public int ThreeState { get; set; } = 0;
        public DateTime? EventDate { get; set; } = null;
    }

    public class ThreadParameter
    {
        public string ProcessName { get; set; }
        public string Path { get; set; }
    }

    //public class JsonRoot
    //{
    //    public int locked { get; set; }
    //    public string password { get; set; }
    //    public int changepass { get; set; }
    //    public int status_code { get; set; }
    //    public string ticket_status { get; set; }
    //}
}
