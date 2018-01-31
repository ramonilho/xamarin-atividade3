using System;
using System.Collections.Generic;
using Atividade3_Xamarin.Data;
using SQLite;
using Xamarin.Forms;

namespace Atividade3_Xamarin
{
    public class Student
    {
        private SQLiteConnection database;
        static object locker = new object();

        public Student()
        {
            // Creating connection with SQLite
            database = DependencyService.Get<ISQLite>().GetConnection();

            // Creating Students Table
            database.CreateTable<Student>();
        }

        #region Attributes
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }

        public string RM { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Approved { get; set; }
        #endregion

        #region Student Database Functions
        public int Save(Student student)
        {
            lock (locker)
            {
                return database.InsertOrReplace(student);
            }
        }
        public IEnumerable<Student> FetchStudentList()
        {
            lock (locker)
            {
                return (from s in database.Table<Student>() 
                        orderby s.Name
                        select s);
            }
        }
        public Student GetStudent(Guid Id)
        {
            lock (locker)
            {
                return database.Table<Student>().Where(s => s.Id == Id).FirstOrDefault();
            }
        }
        public int DeleteStudent(Guid Id)
        {
            lock (locker)
            {
                return database.Delete<Student>(Id);
            }
        }
        #endregion


    }
}
