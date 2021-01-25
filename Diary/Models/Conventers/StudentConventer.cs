using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Conventers
{
    //przykład f. rozserzającej
    //public static class StringExtensions //robimy klasę rozszerzającąstatyczną
    //{
    //    public static string ReturnOne(this string model) //funkcja rozszerza stringa o funkcję zwracającą 1
    //    {
    //        return "1";
    //    }
    //}

    public static class StudentConventer
    {
        public static StudentWrapper ToWrapper(this Student model)
        {
            return new StudentWrapper
            {
                ID = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                Group = new GroupWrapper 
                         { ID = model.Group.Id,
                           Nazwa = model.Group.Name 
                         },
                Math = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Math) //łaczy wszystkie oceny po przecinku              
                        .Select(y => y.Rate)),
                Physics = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Physics) //łaczy wszystkie oceny po przecinku              
                        .Select(y => y.Rate)),
                PolishLang = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.PolishLang) //łaczy wszystkie oceny po przecinku              
                        .Select(y => y.Rate)),
                ForeignLang = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.ForeignLang) //łaczy wszystkie oceny po przecinku              
                        .Select(y => y.Rate)),
                Technology = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Technology) //łaczy wszystkie oceny po przecinku              
                        .Select(y => y.Rate)),

            };
        } 

        //konwenter odwrotny, Dao Data Access Object 
        public static Student ToDao(this StudentWrapper model)
        {
            return new Student
            {
                Id = model.ID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                GroupId = model.Group.ID,
                Comments = model.Comments,
                Activities = model.Activities
            };
        }

        public static List<Rating> ToRatingDao(this StudentWrapper model) //dodatkowy konwenter ocen
        {
            var ratings = new List<Rating>();

            if (string.IsNullOrWhiteSpace(model.Math))
            {

                model.Math.Split(',').ToList().ForEach(x =>
                    ratings.Add(new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.ID,
                        SubjectId = (int)Subject.Math
                    }));
            }

            if (string.IsNullOrWhiteSpace(model.Physics))
            { 
                model.Math.Split(',').ToList().ForEach(x =>
                    ratings.Add(new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.ID,
                        SubjectId = (int)Subject.Physics
                    }));
            }

            if (string.IsNullOrWhiteSpace(model.PolishLang))
            { 
                model.Math.Split(',').ToList().ForEach(x =>
                    ratings.Add(new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.ID,
                        SubjectId = (int)Subject.PolishLang
                    }));
            }

            if (string.IsNullOrWhiteSpace(model.ForeignLang))
            { 
                model.Math.Split(',').ToList().ForEach(x =>
                    ratings.Add(new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.ID,
                        SubjectId = (int)Subject.ForeignLang
                    }));
            }

            if (string.IsNullOrWhiteSpace(model.Technology))
            {

                model.Math.Split(',').ToList().ForEach(x =>
                    ratings.Add(new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.ID,
                        SubjectId = (int)Subject.Technology
                    }));
            }

            return ratings;

        } 
    }
}
