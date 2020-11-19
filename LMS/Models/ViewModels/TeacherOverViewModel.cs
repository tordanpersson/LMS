﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModels
{
    public class TeacherOverViewModel
    {
        // Lists
        public List<Course> Courses { get; set; }
        // fields
        public ApplicationUser Teacher { get; set; }
        public string NextCourse { get; set; }

    }
}