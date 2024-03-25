﻿namespace MVC_tasks_2.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User> ();
    }
}
