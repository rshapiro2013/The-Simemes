using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Tasks
{
    public class TaskEventProgress
    {
        public int EventID;
        public List<TaskProgress> Progress = new List<TaskProgress>();
        public bool Claimed;
    }
}
