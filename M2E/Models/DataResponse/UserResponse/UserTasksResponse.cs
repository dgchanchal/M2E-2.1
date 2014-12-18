using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse.UserResponse
{
    public class UserTasksResponse
    {
        public string UnreadTasks { get; set; }
        public string CountLabelType { get; set; }
        public List<UserTaskList> TaskList { get; set; }
    }
}