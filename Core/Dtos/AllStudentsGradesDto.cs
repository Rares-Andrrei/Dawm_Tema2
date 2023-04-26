using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class AllStudentsGradesDto
    {
        public List<GradesByStudent> Grades { get; set; } = new();
        public AllStudentsGradesDto(List<Student> students) 
        {
            foreach (var student in students)
            {
                Grades.Add(new GradesByStudent(student));
            }
        }
    }
}
