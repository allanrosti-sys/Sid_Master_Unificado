using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.ClickUp.Control.Model
{
    public class Task
    {
        public string id { get; set; }
        public object custom_id { get; set; }
        public string name { get; set; }
        public string text_content { get; set; }
        public string description { get; set; }
        //public Status status { get; set; }
        public string orderindex { get; set; }
        public string date_created { get; set; }
        public string date_updated { get; set; }
        public object date_closed { get; set; }
        public bool archived { get; set; }
        // public Creator creator { get; set; }
        public List<object> assignees { get; set; }
        public List<object> watchers { get; set; }
        // public List<Checklist> checklists { get; set; }
        public List<object> tags { get; set; }
        public object parent { get; set; }
        public object priority { get; set; }
        public string due_date { get; set; }
        public string start_date { get; set; }
        public object points { get; set; }
        public int time_estimate { get; set; }
        //public List<CustomField> custom_fields { get; set; }
        //public List<Dependency> dependencies { get; set; }
        public List<object> linked_tasks { get; set; }
        public string team_id { get; set; }
        public string url { get; set; }
        public string permission_level { get; set; }
        //public List list { get; set; }
        // public Project project { get; set; }
        // public Folder folder { get; set; }
        // public Space space { get; set; }
    }

    public class Task_List 
    {
        public List<Task> Tasks { get; set; }

    }
}
