using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassroomProject
{
    public class Classroom
    {
        public readonly List<Student> students;
        private int capacity;

        private Classroom()
        {
            this.students = new List<Student>();
        }
        public Classroom(int capacity)
            :this()
        {
            this.Capacity = capacity;
        }
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
            }
        }
        public int Count => this.students.Count;

        public string RegisterStudent(Student student)
        {
            if (this.students.Count + 1 <= this.Capacity)
            {
                this.students.Add(student);
                return $"Added student {student.FirstName} {student.LastName}";
            }
            else
            {
                return "No seats in the classroom";
            }
        }

        public string DismissStudent(string firstName, string lastName)
        {
            Student student = this.students.FirstOrDefault(n => n.FirstName == firstName && n.LastName == lastName);
            if (student != null)
            {
                this.students.Remove(student);
                return $"Dismissed student {student.FirstName} {student.LastName}";
            }
            else
            {
                 return "Student not found";
            }
        }

        public string GetSubjectInfo(string subject)
        {
            List<Student> list = this.students.Where(x => x.Subject == subject).ToList();
            StringBuilder sb = new StringBuilder();
            if (list.Count > 0)
            {
                sb.AppendLine($"Subject: {subject}");
                sb.AppendLine("Students:");
                foreach (Student student in list)
                {
                    sb.AppendLine($"{student.FirstName} {student.LastName}");
                }
                return sb.ToString().TrimEnd();
            }
            else
            {
               sb.AppendLine("No students enrolled for the subject");
                return sb.ToString().TrimEnd();
            }
        }
        public int GetStudentsCount()
        {
            return this.students.Count;
        }

        public Student GetStudent(string firstName, string lastName)
        {
            Student student = this.students.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
            return student;
        }

    }
}
