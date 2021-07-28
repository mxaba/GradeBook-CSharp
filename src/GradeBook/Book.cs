using System;
using System.Collections.Generic;

namespace GradeBook {

    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NameObject {

        public NameObject(string name){
            Name = name;
        }
        public string Name {
            get;
            set;
        }
    }

    public interface IBook {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NameObject {
        public Book(string name) : base(name){
        }

        public abstract void AddGrade(double grade);
    }
    
    public class InMemoryBook : Book {
        
        private List<double> grades;

        public const string CATEGORY = "Science";

        public event GradeAddedDelegate GradeAdded;


        public InMemoryBook(string name) : base(name) {
            grades = new List<double>();
            Name = name;
        }

        public void AddGrade(char letter) {
            switch(letter) {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                case 'F':
                    AddGrade(50);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }
        public override void AddGrade(double grade){
            if(grade <= 100 && grade >= 0){
                grades.Add(grade);
                if (GradeAdded != null){
                    GradeAdded(this, new EventArgs());
                }
            } else {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public Statistics GetStatistics() {
            var result = new Statistics();
            
            result.Average = 0.00;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            for(var index = 0; index < grades.Count; index++) {
                result.High = Math.Max(grades[index], result.High);
                result.Low = Math.Min(grades[index], result.Low);
                result.Average += grades[index];
            }

            switch (result.Average) {
                case var d when d >= 90.00:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.00:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.00:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.00:
                    result.Letter = 'D';
                    break;
                case var d when d >= 50.00:
                    result.Letter = 'F';
                    break;
                default:
                    result.Letter = 'H';
                    break;
            }

            result.Average /= grades.Count;
            
            return result;
        }
    }
}