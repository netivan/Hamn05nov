using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Hamnen
{
    class Disk
    {

        public static void sparaRegisterIfilen(List<Båt> reg)
        {  //portregistret sparas i "Register.bin" -filen i binärt format

            Stream stream = File.Open("Registen.bin", FileMode.Create);

            var bin = new BinaryFormatter();
            // objektet som ska sparas måste serialiseras med "Serialize" metoden
            bin.Serialize(stream, reg);

            stream.Close();
            //!! Attributet [Serializable ()] måste rapporteras i Båt-klassen och i strukturen. .
            //!! Indikerar att en klass kan serieseras. Klassen kan inte ärvas.
        }

        public static List<Båt> läsRegisterFrånFil()
        {
            try
            {
                Stream stream = File.Open("Registen.bin", FileMode.Open);
                var bin = new BinaryFormatter();
                // Metoden "Deserialize" deserialiserar den binära filen för att få originalobjektet
                List<Båt> reg = (List<Båt>)bin.Deserialize(stream);
                stream.Close();
                return reg;  //Returnerar objektet List <Bat> (hamnenRegister)
            }
            catch
            {
                return null; //om filen inte hittas returnerar den "null"
            }
        }
    }
}
