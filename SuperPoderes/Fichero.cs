using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace SuperPoderes
{
    class SPFile
    {
        private static readonly Lazy<SPFile> _file = new Lazy<SPFile>(() => new SPFile());
        private List<String> roles = new List<string>();

        private SPFile()
        {
            //Inicializamos el objeto fichero
            openData();
            //Realizamos la lectura del mismo y realizamos la separación por los condicionantes
            separateRoles();
            Console.ReadKey();
        }


        public static SPFile instancia
        {
            get
            {
                return _file.Value;
            }
        }

        private void openData()
        {
            String filePath;

            filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];

            if (existsFilePath(filePath))
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    roles.Add(line);
                }
                file.Close();
            }
            else
            {
                System.Console.WriteLine("File path doesn´t exists.");
                System.Console.ReadKey();
            }

        }

        private void generateDat(string outPath, List<String> names)
        {
            System.IO.File.WriteAllLines(outPath, names);
        }


        private Boolean existsFilePath(string path)
        {
            return File.Exists(path);
        }

        /*
         * El cliente nos ha dicho que los Villanos siempre tienen una “D” en el nombre y tendremos que fiarnos de
         * esa regla. Suponemos que en un futuro cercano se darán cuenta de que esa regla es incorrecta y nos
         * pedirán corregirla.
         * -> Apunte, sobreentiendo que se habla de "D" y de "d", si fuera sólo D, bastaría con quitar el ToUpper de la 
         * expresión lambda.
         */
        private void separateRoles()
        {
                //Si en el futuro queremos cambiar la condición de separación sólo tenemos que modificar esta función.             
                //Extraemos los villanos
                List<String> villains = roles.Where(x => x.ToUpper().Contains('D')).ToList();
                generateDat("Villanos.dat", villains);
                //Extraemos los héroes en base a los villanos encontrados. 
                List<String> heroes = roles.Where(x => !villains.Contains(x)).ToList();
                generateDat("SuperHeroes.dat", heroes);
        }
    }
}
