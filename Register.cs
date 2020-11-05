using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Threading;
using System.Linq;

namespace Hamnen
{
    class Register : Mother
    {
        public static List<Båt> HamnRegister = new List<Båt>();   // registret av hamnen i vilken alla inkommande båtar läggs in. ( i början är tom)
        public static void SkrivutHamnaregister(int day)
        {
            /* var = IEnumerable*/
            var dagensRegister = from b in HamnRegister   // dagenRegister är en lista , det finns alla båtar som är i hamnen en x dag.   
                                                          //where (day - b.aDag) < b.dagarIhamnen     // (pågående dag - ankomstdagen av båten) ska vara < dagarna båten ska stanna i hamnen
                                 orderby b.kajPlats
                                 select b;              
                                                       //  HamnRegister.sort()


            flotta(day);      // räcknar båtarna enligt typ som finns i kajen en x dag   (ex. Motorbåtar[5])
            statistik(day);

            int ypos = yPosHmReg + 1;

            string sPlats, övrigt;
            foreach (var b in dagensRegister)       // skriver ut alla båtar som finns i listan q 
            {
                sPlats = b.kajPlats.ToString();
                if (b.antalPlatser > 1)
                    sPlats += "-" + (b.kajPlats + b.antalPlatser - 1).ToString();

                övrigt = b.övrigt.Beskrivning + b.övrigt.value.ToString() + b.övrigt.mått;

                SetCursorPosition(xPosHmReg, ypos++);
                Write("{0,-5} [{1,5}] ", sPlats, b.båtId);
                Console.ForegroundColor = b.färg;
                Write(" {0,-10}  ", b.typ);
                Console.ForegroundColor = ConsoleColor.White;  
                Write("{0,-15} ", övrigt);
            }
            SetCursorPosition(xPosHmReg, ypos++);
            WriteLine($"                                                                  ");
        }
                               
        public static bool båtarnaLämnarHamnen(int day)
        {   // metoden letar efter en båt som måste ut, tar bort den från kaj och registret
            foreach (var b in HamnRegister)
            {
                if ((day - b.aDag) == b.dagarIhamnen)
                {
                    Kaj.RemoveBåt(b.kajPlats, b.antalPlatser);
                    HamnRegister.Remove(b); //Båten tas bort från hamnaregistret
                    Skärm.skrivautgående($"{b.typ} [{b.båtId}]  plats {b.kajPlats} -> ");
                    SkrivutHamnaregister(day); //hamnaregistret skrivs om utan denna båt
                    Skärm.pausa(300);
                    return true; //"true" för att indikera att andra båtar kan åka ut
                }
            }
            return false;//"false"  eftersom det inte finns några båtar som måste ut
        }

        public static void flotta(int day)
        {
            var q1 = from b in HamnRegister         // delat i grupp alla båttyper.   q1 är 4 tabeller dvs en tabel för varje båt i vilken finns alla egenskaper av varje båt.
                                                    //where (day - b.aDag) < b.dagarIhamnen
                     group b by b.typ;

            int totalBåtar = (from t in q1
                              select t.Count()).Sum();         // från t (tabell) som finns i q1, (count) räcknar alla båtar som är i varje tabell.  Sum()  summerar

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(xPosFlotta, yPosFlotta);

            Console.Write($"Båtar på kajen {totalBåtar} => ");

            foreach (var z in q1)
            {
                Console.Write("{0}:{1} ", z.Key, z.Count());
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void statistik(int day)
        {
            var totalVikt = (from b in HamnRegister
                                 //where (day - b.aDag) < b.dagarIhamnen
                             select b.vikt).Sum();

            var medelHastighet = (from b in HamnRegister
                                      //where (day - b.aDag) < b.dagarIhamnen
                                  select b.maxHastighet).Average();

            double ledigaPlatser = Kaj.ledigaPlatser();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(xPosStatistik, yPosStatistik);
            // Console.Write($"Lediga Platser: {ledigaPlatser}  Total Vikt: {totalVikt}  Medelhastighet: {medelHastighet}  ");
            Console.Write("STATISTIK => Lediga Platser: {0,2}  Total Vikt: {1,-7}  Medelhastighet: {2, 6:N1}  ", ledigaPlatser, totalVikt, medelHastighet);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
      public static void skrivaAvvisadeBåtar(int a)
        {
            Console.SetCursorPosition(xPosAvvisade, yPosAvvisade);
            Console.Write("Avvisade båtar {0}  ", a);
        }

    }
}
