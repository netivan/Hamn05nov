using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hamnen
{
    public enum TYP  //sorters båtar
    {
        Roddbåt = 1,
        Motorbåt = 2,
        Segelbåt = 3,
        Lastfartyg = 4,
        Katamaran = 5
    }
    [Serializable()]           //!! Indikerar att en klass kan serieseras. Klassen kan inte ärvas.
    struct Övrigt
    {
        public string Beskrivning;
        public int value;
        public string mått;
    }
    [Serializable()]
    class Båt
    {
        public ConsoleColor färg;//Färg som används för att skriva ut båten på skärmen
        int maxTyp = 4; //antal båttyper, om  lika med 5 inkluderar det katamaranen 
        public TYP typ;             //typ av båt : 1,2,3,4,5 
        public string båtId { get; set; }
        public int vikt;           //kg
        public int maxHastighet;  // Km/h
        public string hamnplats;   //H: halv hamnplats, F:  hamnplats
        public int dagarIhamnen;  //dagar med stopp i hamnen
        public Övrigt övrigt;
        //------
        public int aDag;   //ankomstdag till hamnen
        public int kajPlats;   // Första platsen i kajen 
        public int antalPlatser;    //  antal hamnplatser båten ockuperar
        static Random r = new Random();

        public Båt(int day)  //Konstruktör av båtklassen
        {     //Skapa båten som anländer till hamnen med slumpmässiga parametrar

            // Ett slumpmässigt tal mellan 1 och 4 / 5 genereras och omvandlas till båttyp(enum: TYP) "
            typ = (TYP)r.Next(1, maxTyp + 1);   //  motorbåt (exempel)
                                                // int typ = r.Next(1,maxTyp + 1)
            aDag = day;  // Ankomstdag av båten
            switch (typ)
            {
                case (TYP)1:
                    // case 1:
                    infoRoddbåt();
                    break;
                case (TYP)2:
                    infoMotorbåt();
                    break;
                case (TYP)3:
                    infoSegelbåt();
                    break;
                case (TYP)4:
                    infoLastfartyg();
                    break;
                case (TYP)5:
                    infoKatamaran();
                    break;
                    //default:
                    //  break;
            }
        }
        private void infoRoddbåt()
        {
            vikt = r.Next(10, 31) * 10;  //båtens vikt 100 kg
            maxHastighet = r.Next(1, 4); //båtens hastighet i knop
            dagarIhamnen = 1; //Roddbåtar stannar I hamnen 1 dag
            övrigt.Beskrivning = "Antal passagerare :";
            övrigt.value = r.Next(1, 7);//Max antal passagerare (1 til 6)
            övrigt.mått = " ";
            båtId = "R-" + RandomCode(3);
            antalPlatser = 0;    //  halv plats ockuperad
            hamnplats = "H"; //'H' anger en halv plats upptagen av en båt, 'F' full plats upptagen av två båtar
            färg = ConsoleColor.DarkRed;
        }

        private void infoMotorbåt()
        {
            vikt = r.Next(20, 300) * 10;  //båtens vikt fr. 200 upp till 3000kg
            maxHastighet = r.Next(1, 61); //båtens hastighet i knop
            dagarIhamnen = 3; //Motorbåtar stannar i hamnen 3 dagar
            övrigt.Beskrivning = " hästkrafter ";
            övrigt.value = r.Next(11, 1000);//Max antal hästkrafter(10 til 1000hk)
            övrigt.mått = "hk";
            båtId = "M-" + RandomCode(3);
            antalPlatser = 1;  // 
            hamnplats = "M";
            färg = ConsoleColor.Green;
        }
        private void infoSegelbåt()
        {
            vikt = r.Next(80, 600) * 10;  //båtens vikt fr. 800 kg upp till 6000kg
            maxHastighet = r.Next(1, 61); //båtens hastighet i knop
            dagarIhamnen = 4; //Segelbåtar stannar i hamnen 4 dagar
            övrigt.Beskrivning = "Båtlängd ";
            övrigt.value = r.Next(11, 61);//Max längd(10 till 60 fot)
            övrigt.mått = "fot";
            båtId = "S-" + RandomCode(3);
            antalPlatser = 2;
            hamnplats = "SS";
            färg = ConsoleColor.Blue;
        }
        private void infoLastfartyg()
        {
            vikt = r.Next(300, 2000) * 10;  //båtens vikt fr. 3000 kg upp till 20000kg
            maxHastighet = r.Next(0, 21); //båtens hastighet i knop
            dagarIhamnen = 6; //Lastfartyg stannar i hamnen 6 dagar
            övrigt.Beskrivning = "Last : ";
            övrigt.value = r.Next(11, 61);//Max antal lastade containers(0 till 500 containrar)
            övrigt.mått = "containers ";
            båtId = "L-" + RandomCode(3);
            antalPlatser = 4;
            hamnplats = "LLLL";
            färg = ConsoleColor.Magenta;
        }
        private void infoKatamaran()
        {
            vikt = r.Next(120, 800) * 10;  //båtens vikt fr. 120 kg upp till 8000kg
            maxHastighet = r.Next(0, 13); //båtens hastighet i knop
            dagarIhamnen = r.Next(0, 11); //Katamaranen stannar i hamnen fr. 1 till 10 dagar
            övrigt.Beskrivning = "Antal bäddplatser ";
            övrigt.value = r.Next(0, 3);//Max antal bäddplatser(1 till 4 bäddplatser)
            övrigt.mått = "Persons";
            båtId = "K-" + RandomCode(3);
            antalPlatser = 3;
            hamnplats = "KKK";

        }

        public static string RandomCode(int length)       //  vi får ett slumpmässigt  kod av typbåt
        {

            //string a = "ABCDEFGHALMNOPQRSTUVZ";   
            //return a.Substring(r.Next(1, a.Length), 1) + a.Substring(r.Next(1, a.Length), 1) + a.Substring(r.Next(1, a.Length), 1);

            string s = "";
            for (int i = 0; i < length; i++)
                s += (Char)r.Next('A', 'Z'); //genererar ett slumpmässigt tal mellan 65(A) och 90(Z)
            return s;

            
        }
    }
} //=======