using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Class provides methods for handeling occuring exceptions
    /// </summary>
    public class clsExceptionHandler
    {
        /// <summary>
        /// Tries to handle a occured exception by writing it into a log file. If a exception occurrs while handeling, the exception is written in the console. 
        /// </summary>
        /// <param name="sClass"> The class in which the exception occurred. </param>
        /// <param name="sMethod"> The method in which the exception occurred. </param>
        /// <param name="sMessage"> The exception message. </param>
        public static void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                System.IO.File.WriteAllText("Errors.txt", Environment.NewLine + sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
