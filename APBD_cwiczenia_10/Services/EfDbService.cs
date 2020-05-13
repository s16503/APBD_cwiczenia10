using APBD_cwiczenia_10.Models;
using APBD_cwiczenia_10.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_cwiczenia_10.Services
{
    public class EfDbService :IDbService
    {
        public s16503Context _context { get; set; }

        public EfDbService(s16503Context context)
        {
            _context = context;
        }

        public List<Student> GetStudents()
        {
            return _context.Student.ToList();
        }

        public Student UpdateStudent(string index, UpdateStudentRequest request)
        {
           Student s = (from student in _context.Student 
                        where student.IndexNumber == index 
                        select student).First();


                if(request.FirstName != null)
                s.FirstName = request.FirstName;

                if (request.LastName != null)
                    s.LastName = request.LastName;

                if (request.IdEnrollment != null)
                    s.IdEnrollment = request.IdEnrollment;

                if (request.Password != null)
                    s.Password = request.Password;

                if (request.BirthDate != null)
                    s.BirthDate = request.BirthDate;

            

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }


            return s;
        }

        public void DeleteStudent(string index)
        {

            var student = _context.Student.Find(index);
            if (student == null)
                throw new Exception("nie można usunąć, nie znaleziono indeksu");


            _context.Student.Remove(student);
            _context.SaveChanges();
        }
    }
}
