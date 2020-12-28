using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Diary.Models.Conventers;

namespace Diary
{
    //klasa agregująca zapytania na bazie
    class Repository
    {
        public List<Group> GetGroups()
        {
            //pobieranie danych z bazy - tworzymy context bay Entity FW
            using (var context = new ApplicationDBContext())
            {
                return context.Groups.ToList();//pobierz tabele Groups
            }
        }

        public List<StudentWrapper> GetStudents(int groupId) //groupId zwraca studentów z danej ustawionej grupy
        {
            //pobieranie danych z bazy - tworzymy context bay Entity FW
            using (var context = new ApplicationDBContext())
            {
                var students = context.Students
                     .Include(x => x.Group)
                     //.Where(x => x.GroupId == groupId || groupId == 0) //selekcja ale tutaj chcemy uzyskać wszystkie grupy jak groupid=0
                     .Include(x => x.Ratings)
                     //.ToList();//pobierz tabele Students ora pobierz dokładne ingo o grupie id i nazwa i ocenach
                     .AsQueryable(); // czeka na poniższe wrunki i dopiero po ich sprawdzeniu bedzie wykonany ToList i zwróci powyse kwerenda wyniki

                if (groupId != 0)
                {
                    students = students.Where(x => x.GroupId == groupId); //dodaje do powyższej kerendy ten warunek i dopiero poniżej zostanie wykonane
                }

                return students
                    .ToList() //zwraca wykonanie całej powyższej kwerendy 
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

        public void DeleteStudent(int id)
        {
            using (var contex = new ApplicationDBContext())
            {
                var studentToDelete = contex.Students.Find(id);
                contex.Students.Remove(studentToDelete);
                contex.SaveChanges();
            }
        }
    }
}
