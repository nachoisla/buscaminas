using System;

namespace BuscaMinas
{
    class Program
    {
        static public void IniciarJuego()
        {
            string nivel;
            Mina[,] tablero;
            byte minas;
            byte contMinas;
            int contDescubiertas = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Bienvenido al buscaminas");
                Console.WriteLine("Por favor, seleccione el nivel de dificultad");
                Console.WriteLine("Principiante (P), Intermedio (I) o Experto (E)");

                nivel = Console.ReadLine();

            } while (nivel != "P" && nivel != "p" && nivel != "I" && nivel != "i" && nivel != "E" && nivel != "e");

            if (nivel == "P" || nivel == "p")
            {
                tablero = new Mina[12, 10];
                minas = 10;
                contMinas = minas;
                CrearTablero(tablero, minas, contMinas, contDescubiertas);
            }

            if (nivel == "I" || nivel == "i")
            {
                tablero = new Mina[20, 18];
                minas = 40;
                contMinas = minas;
                CrearTablero(tablero, minas, contMinas, contDescubiertas);
            }

            if (nivel == "E" || nivel == "e")
            {
                tablero = new Mina[20, 32];
                minas = 99;
                contMinas = minas;
                CrearTablero(tablero, minas, contMinas, contDescubiertas);
            }
        }

        static public void CrearTablero(Mina[,] tablero, byte minas, byte contMinas, int contDescubiertas)
        {
            Console.Clear();

            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = new Mina();
                }
            }

            Random rnd = new Random();

            int num0 = minas;

            while (num0 > 0)
            {
                int num1 = rnd.Next(3, tablero.GetLength(0) - 1);
                int num2 = rnd.Next(1, tablero.GetLength(1) - 1);

                if (tablero[num1, num2]._esMina == false)
                {
                    tablero[num1, num2]._esMina = true;
                }
                else
                {
                    num0++;
                }

                num0--;
            }

            for (byte i = 3; i < tablero.GetLength(0) - 1; i++)
            {
                for (byte j = 1; j < tablero.GetLength(1) - 1; j++)
                {
                    if (tablero[i - 1, j]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                    if (tablero[i - 1, j - 1]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                    if (tablero[i, j - 1]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                    if (tablero[i + 1, j - 1]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                    if (tablero[i + 1, j]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                    if (tablero[i + 1, j + 1]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                    if (tablero[i - 1, j + 1]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                    if (tablero[i, j + 1]._esMina == true)
                    {
                        tablero[i, j]._cantMinas++;
                    }
                }
            }

            ImprimirTablero(tablero, minas, contMinas);

            EleccionJugada(tablero, minas, contMinas, contDescubiertas);
        }

        static public void ImprimirTablero(Mina[,] tablero, byte minas, byte contMinas)
        {
            Console.Clear();

            for (byte i = 0; i < tablero.GetLength(0); i++)
            {
                for (byte j = 0; j < tablero.GetLength(1); j++)
                {
                    if (i == 1 && j == 1)
                    {
                        Console.Write("M");
                    }
                    else if (i == 1 && j == 2)
                    {
                        Console.Write(":");
                    }
                    else if (i == 1 && j == 3)
                    {
                        Console.Write($"{contMinas}");
                    }
                    else if (contMinas > 9 && i == 1 && j == tablero.GetLength(1) - 1)
                    {
                        Console.Write(" ");
                    }
                    /*else if (tablero[i, j]._esMina == true)
                    {
                        Console.Write("M");
                    }*/
                    else if (tablero[i, j]._bandera == true)
                    {
                        Console.Write("B");
                    }
                    else if (tablero[i, j]._esMina == false && tablero[i, j]._descubierta == true)
                    {
                        Console.Write($"{tablero[i, j]._cantMinas}");
                    }
                    else if (tablero[i, j]._esMina == true && tablero[i, j]._descubierta == true)
                    {
                        Console.Write("M");
                    }
                    else if ((i != 0 && i != 1 && i != 2 && i != tablero.GetLength(0) - 1) && (j != 0 && j != tablero.GetLength(1) - 1))
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
        }

        static public void EleccionJugada(Mina[,] tablero, byte minas, byte contMinas, int contDescubiertas)
        {
            string opcion;
            byte coord_i;
            byte coord_j;

            do
            {
                Console.WriteLine("Elija si quiere descubrir una celda (D) o marcarla con una bandera / desmarcarla (B)");

                opcion = Console.ReadLine();

            } while (opcion != "D" && opcion != "d" && opcion != "B" && opcion != "b");

            do
            {
                Console.WriteLine($"Elija la fila (entre 1 y {tablero.GetLength(0) - 4})");

                string y = Console.ReadLine();

                if (y.Length == 0)
                {
                    y = "0";
                }

                coord_i = byte.Parse(y);

            } while (coord_i < 1 || coord_i > tablero.GetLength(0) - 4);

            do
            {
                Console.WriteLine($"Elija la columna (entre 1 y {tablero.GetLength(1) - 2})");

                string z = Console.ReadLine();

                if (z.Length == 0)
                {
                    z = "0";
                }

                coord_j = byte.Parse(z);

            } while (coord_j < 1 || coord_j > tablero.GetLength(0) - 2);

            Jugada(opcion, tablero, minas, contMinas, contDescubiertas, coord_i, coord_j);
        }

        static public void Jugada(string opcion, Mina[,] tablero, byte minas, byte contMinas, int contDescubiertas, byte coord_i, byte coord_j)
        {
            if ((opcion == "D" || opcion == "d") && tablero[coord_i + 2, coord_j]._esMina == true)
            {
                for (byte i = 0; i < tablero.GetLength(0); i++)
                {
                    for (byte j = 0; j < tablero.GetLength(1); j++)
                    {
                        if (tablero[i, j]._esMina == true)
                        {
                            tablero[i, j]._descubierta = true;
                        }
                    }
                }

                ImprimirTablero(tablero, minas, contMinas);

                Console.WriteLine("Pisó una mina. Game over.");

                Environment.Exit(0);
            }

            if ((opcion == "D" || opcion == "d") && tablero[coord_i + 2, coord_j]._esMina == false)
            {
                tablero[coord_i + 2, coord_j]._descubierta = true;

                contDescubiertas++;

                if (contDescubiertas == ((tablero.GetLength(0) - 4) * (tablero.GetLength(1) - 2)) - minas)
                {
                    Console.WriteLine("Felicitaciones!!! Ganó!!!");

                    Environment.Exit(0);
                }
            }

            if ((opcion == "B" || opcion == "b") && tablero[coord_i + 2, coord_j]._esMina == true && tablero[coord_i + 2, coord_j]._bandera == false)
            {
                tablero[coord_i + 2, coord_j]._bandera = true;

                contMinas--;
                minas--;

                if (minas == 0)
                {
                    Console.WriteLine("Felicitaciones!!! Ganó!!!");

                    Environment.Exit(0);
                }
            }
            else if ((opcion == "B" || opcion == "b") && tablero[coord_i + 2, coord_j]._esMina == true && tablero[coord_i + 2, coord_j]._bandera == true)
            {
                tablero[coord_i + 2, coord_j]._bandera = false;

                contMinas++;
                minas++;
            }

            if ((opcion == "B" || opcion == "b") && tablero[coord_i + 2, coord_j]._esMina == false && tablero[coord_i + 2, coord_j]._bandera == false)
            {
                tablero[coord_i + 2, coord_j]._bandera = true;

                contMinas--;
            }
            else if ((opcion == "B" || opcion == "b") && tablero[coord_i + 2, coord_j]._esMina == false && tablero[coord_i + 2, coord_j]._bandera == true)
            {
                tablero[coord_i + 2, coord_j]._bandera = false;

                contMinas++;
            }

            ImprimirTablero(tablero, minas, contMinas);

            EleccionJugada(tablero, minas, contMinas, contDescubiertas);
        }

        static void Main(string[] args)
        {
            IniciarJuego();
        }
    }
}
