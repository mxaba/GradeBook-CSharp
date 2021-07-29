using System;

namespace GradeBook {
    public class Statistics{
        public double Average {
            get {
                return Sum / Count ;
            }
        }
        public double High;
        public double Low;
        public char Letter {
            get {
                switch (Average) {
                case var d when d >= 90.00:
                    return 'A';
                case var d when d >= 80.00:
                    return 'B';
                case var d when d >= 70.00:
                    return 'C';
                case var d when d >= 60.00:
                    return 'D';
                default:
                    return 'F';
            }
            }
        }

        public double Sum;
        public int Count;

        public Statistics() {
            Sum = 0.00;
            Count = 0;
            High = double.MinValue;
            Low = double.MaxValue;
        }

        public void Add(double number) {
            Sum += number;
            Count += 1;
            High = Math.Max(number, High);
            Low = Math.Min(number, Low);
        }
    }
    
}