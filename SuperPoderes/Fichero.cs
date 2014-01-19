using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace SuperPoderes
{
    class SPFile
    {
        private static readonly Lazy<SPFile> _file = new Lazy<SPFile>(() => new SPFile());
        private List<String> roles = new List<string>();
        private String cadRegExp;

        private SPFile()
        {
            Stopwatch timer = new Stopwatch();
            //Inicializamos el objeto fichero
            openData();
            //Realizamos la lectura del mismo y realizamos la separación por los condicionantes
            
            //Ejemplo con Linq
            timer.Start();
            separateRolesLinq();
            timer.Stop();
            Console.WriteLine("Tiempo de ejecución con Linq: {0}", timer.Elapsed.ToString());
            
            //Ejemplo bucle for
            timer.Restart();
            separateRolesFor();
            timer.Stop();
            Console.WriteLine("Tiempo de ejecución con un bucle for: {0}", timer.Elapsed.ToString());

            //Ejemplo bucle foreach
            timer.Restart();
            separateRolesForEach();
            timer.Stop();
            Console.WriteLine("Tiempo de ejecución con un bucle foreach: {0}", timer.Elapsed.ToString());
            
            //Ejemplo Expresiones regulares

            //roles.All(x => { x = "," + x; return true; });
            //myCollection = myCollection.Select(m => m.Substring(0, 8)).ToArray();

            //cadRegExp = string.Join(",", roles);
            //timer.Restart();
            //separateRolesRegularExp();
            //timer.Stop();
            //Console.WriteLine("Tiempo de ejecución con expresiones regulares: {0}", timer.Elapsed.ToString());

            Console.WriteLine("Pulsa una tecla para finalizar.");
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
                roles = System.IO.File.ReadAllLines(filePath).ToList();
            }
            else
            {
                Console.WriteLine("La ruta configurada no existe: {0}. ", filePath);
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
        private void separateRolesLinq()
        {
                //Si en el futuro queremos cambiar la condición de separación sólo tenemos que modificar esta función.             
                //Extraemos los villanos
                List<String> villains = roles.Where(x => x.ToUpper().Contains('D')).ToList();
                generateDat("Villanos.dat", villains);
        }

        private void separateRolesFor()
        {           
            List<String> villains = new List<string>();
            List<String> heroes = new List<string>();
            //Extraemos los villanos
            for (int i = 0; i < roles.Count; i++)
            {
                if (roles[i].ToUpper().Contains("D"))
                {
                    villains.Add(roles[i]);
                }
            }
            generateDat("VillanosFor.dat", villains);
        }

        private void separateRolesForEach()
        {
            List<String> villains = new List<string>();
            List<String> heroes = new List<string>();
            //Extraemos los villanos
            foreach (string name in roles)
                if (name.ToUpper().Contains("D"))
                {
                    villains.Add(name);
                }

            generateDat("VillanosForEach.dat", villains);
         }

        //private void separateRolesRegularExp()
        //{
        //    string pattern = @",*d*,";
        //    List<String> villains = new List<string>();
        //    List<String> heroes = new List<string>();
        //    Match match;

        //    Regex intAll = new Regex(pattern, RegexOptions.IgnoreCase);
        //    match = intAll.Match(cadRegExp);
        //    int matches = 0;
        //    while (match.Success)
        //    {
        //        matches++;
        //        // Do nothing with the match except get the next match.
        //        match = match.NextMatch();
        //    }

        //    //foreach (string name in roles)
        //    //{
        //    //    if (Regex.IsMatch(name, pattern))
        //    //    {
        //    //        villains.Add(name);
        //    //    }
        //    //    else
        //    //    {
        //    //        heroes.Add(name);
        //    //    }  
        //    //}  
        //}
    }
}
