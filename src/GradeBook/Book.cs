using System;
using System.IO;
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

    public abstract class Book : NameObject, IBook {
        public Book(string name) : base(name){
        }
        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book {
        public DiskBook(string name) : base(name){
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt")){   
                writer.WriteLine(grade);
                if (GradeAdded != null) {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStatistics() {
            var results = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt")) {
                var line = reader.ReadLine();
                
                while (line != null){
                    var number = double.Parse(line);
                    results.Add(number);

                    line = reader.ReadLine();
                }
            }

            return results;
        }
    }
    
    public class InMemoryBook : Book {
        
        private List<double> grades;

        public const string CATEGORY = "Science";

        public override event GradeAddedDelegate GradeAdded;


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

        public override Statistics GetStatistics() {
            var result = new Statistics();
            
            for(var index = 0; index < grades.Count; index++) {
                result.Add(grades[index]);
            }            
            
            return result;
        }
    }
}