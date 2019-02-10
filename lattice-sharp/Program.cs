using System;

namespace lattice_sharp
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using(var game = new Lattice())
                game.Run();
        }
    }
}