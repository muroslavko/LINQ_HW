using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_Of_Students
{
    public class Test
    {
        public string Name { get; set; }
        public Category Categoty { get; set; }
        public ICollection<Question> Questions { get; set; }
        public int MaxTime { get; set; }
        public int PassMark { get; set; }

        public Test()
        {
                Questions = new List<Question>();
        }
    }
}
