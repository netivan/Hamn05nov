﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hamnen
{
    class Kaj : Mother
    {
        //const int xPosKaj = 9, yPosKaj = 4;
        public static int colore = 2;
        //public string  kaj1 = ".......................................................................";
       
        public static string kaj = new string('.', 64); // kaj initialiseras med 64 "." med hjälp av strängklasskonstruktören

        public static int insertBåt(string HP)     // HP innehåller:  LLLL (lastfartyg)  M (motorbåt) .ect ect
        {
            string freePlats = new string('.', HP.Length);
            if (HP == "H")   // hamnplats är H dvs är en roddbåt som ockuperar 0,5 plats
            {
                int postoH = kaj.IndexOf("H");   // kollar om det finns en H i kajen 
                if (postoH >= 0)              // om postoH >=0 då finns en halvplast tillgänglig
                {
                    replaceIkay(postoH, "F");     //  H blir F vid 2 roddbåtar som ockuperar postoH
                    return postoH;
                }
            }
            int posto = kaj.IndexOf(freePlats);    // om det inte finns plats posto = -1

            if (posto >= 0)
            {
                replaceIkay(posto, HP);
            }
            return posto;
        }
        public static void RemoveBåt(int plats, int aPlats)
        {
            if (aPlats == 0)  //Roddbåt
            {
                if (kaj.Substring(plats, 1) == "F")
                    replaceIkay(plats, "H");
                else
                    replaceIkay(plats, ".");
            }
            else
            {
                string sx = new string('.', aPlats);
                replaceIkay(plats, sx);
            }
        }
        private static void replaceIkay(int pl, string ss)     //ss är det som ska hamna eller sättas i strängen kaj ( . eller ex LLLL )
        {
            string x = kaj.Remove(pl, ss.Length);
            kaj = x.Insert(pl, ss);

            Console.ForegroundColor = (ConsoleColor)colore++; //Färgen ändras i följd
            if (colore == 15) colore = 2;
            Console.SetCursorPosition(pl + 1 + xPosKaj, yPosKaj);
            Console.WriteLine(ss);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static double ledigaPlatser()
        {
            double count = 0;
            foreach (char x in kaj)
            {
                if (x == '.') count++;
                if (x == 'H') count += 0.5;
            }
            return count;
        }
    }

}