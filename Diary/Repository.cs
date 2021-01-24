using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Diary.Models.Conventers;
using Diary.Models;

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

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDBContext())
            {
                //wyszukaj jaki rekord w bazie zaaktualizować
                var studentToUpdate = context.Students.Find(student.Id);
                studentToUpdate.Activities = student.Activities;
                studentToUpdate.Comments = student.Comments;
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.GroupId = student.GroupId;

                //pobierz oceny z bazy
                var studentRatings = context
                    .Ratings
                    .Where(x => x.StudentId == student.Id)
                    .ToList();

                

            }

        }

        private static void UpdateRate (Student student, List<Rating> ratings, ApplicationDBContext context, List<Rating> studentRatings, Subject subject)
        {
            //mając dane studenta w aplikacji i z bazy porównujemy oceny które się zmieniły dla posczególnych przedmiotów
            //stare oceny
            var mathRatings = studentRatings
                    .Where(x => x.SubjectId == (int) Subject.Math)
                    .Select(x => x.Rate); //wybierz tylko oceny 

        //nowe oceny z aplikacji
        var newMathRatings = ratings
            .Where(x => x.SubjectId == (int)Subject.Math)
            .Select(x => x.Rate); //wybierz tylko oceny 

        //teraz trzeba porównać oceny nowe e starymi
        //ze starych ocen wyklucz te które są nowe
        var mathRatingsToDelete = mathRatings.Except(newMathRatings).ToList();
        //z nowych wyklucz stare oceny 
        var mathRatingsToAdd = newMathRatings.Except(mathRatings).ToList();

        //usuń z bazy oceny
        mathRatingsToDelete.ForEach(x =>
                {
                    var ratingToDelete = context.Ratings.First(y =>
                        y.Rate == x && //gdzie równe oceny 
                        y.StudentId == student.Id && //ten sam student i przedmiot
                        y.SubjectId == (int)Subject.Math);

        context.Ratings.Remove(ratingToDelete);
                });

                //dodaj do bazy oceny
                mathRatingsToAdd.ForEach(x =>
                {

                    var ratingToAdd = new Rating
                    {
                        Rate = x, //nowa ocena 
                        StudentId = student.Id,
                        SubjectId = (int)Subject.Math
                    };

    context.Ratings.Add(ratingToAdd);
                });

        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDBContext())
            {
                var dbStudents = context.Students.Add(student); //dodajemy do bazy studenta a następnie pobieramy jego id
                //zapisz każdą ocenę do bazy    
                ratings.ForEach(x =>
                {
                    x.StudentId = dbStudents.Id;
                    context.Ratings.Add(x);
                });

            }
        }



        

    }

}
