namespace Algorithms

{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int number, int n2)
        {
            X = number / n2;
            Y = number % n2;
        }

    }

}
