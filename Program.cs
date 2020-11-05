using System;
using System.Collections.Generic;
using System.Threading;
using static System.Console;

using System.Linq;
using System.Text;

namespace Hamnen
{
    class Program
    {
        static void Main(string[] args)
        {

            int avvisadeBåtar;
            int kPlats;

            Skärm.Titel();    //   skriver ut titeln på console

            for (int day = 1; day < 100; day++)
            {

                avvisadeBåtar = 0;
                Register.skrivaAvvisadeBåtar(0);
                Skärm.skrivaDay(day);     // skriver ut day inkommande och utgående båtar
                Skärm.cancelday();
                do { } while (Register.båtarnaLämnarHamnen(day));//Alla båtar som behöver åka tas bort

                for (int i = 0; i < 5; i++)           // det finns 5 båtar som kommer in varje dag
                {
                    Båt b = new Båt(day);    // structorn anråpas   // skapar objekt båt.  ( b är en slumpmässig båt ) 
                    
                    kPlats = Kaj.insertBåt(b.hamnplats);
                    if (kPlats >= 0)          //båten har placerats i kajen
                    {
                        b.kajPlats = kPlats;
                        Register.HamnRegister.Add(b);
                        Skärm.skrivaIngående($"{b.typ} [{b.båtId}] -> plats {b.kajPlats}");      //  dessa är båtarna som kom till hamnen

                        Register.SkrivutHamnaregister(day);
                    }
                      else
                    {
                        avvisadeBåtar++;
                        Register.skrivaAvvisadeBåtar(avvisadeBåtar);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Skärm.skrivaIngående($"{b.typ} har ingen plats på kajen");
                        Console.Beep();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Skärm.pausa(500)) break;         //Thread.Sleep(1000);

                }


                if (Skärm.pausa(1000)) break;        //Thread.Sleep(1800);   // ny dag
            }

            
            Disk.sparaRegisterIfilen(Register.HamnRegister); //spara Register på disken


            //!! Register.HamnRegister = Disk.läsRegisterFrånFil();    //läser registret från disken
        }
    }
}

