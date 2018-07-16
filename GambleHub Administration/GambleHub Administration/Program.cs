using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GambleHub_Administration
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = new MySqlConnection("datasource=mysql-gamblehuboff.alwaysdata.net; port=3306;Initial Catalog='gamblehuboff_db';username=158469;password=K8LhE283et");
            Console.Title = "GambleHub Administration";
            try
            {
                connection.Open();
                Console.WriteLine("Connexion a la BDD établie, en attente de commande", Console.ForegroundColor = ConsoleColor.Cyan);
            }
            catch
            {
                Console.WriteLine("Connexion a la BDD impossible", Console.ForegroundColor = ConsoleColor.Red);
            }
            commands();


            void commands()
            {
                start:
                string command = Console.ReadLine();
                if (command.Contains("Addmoney"))
                {
                    string[] cmd = command.Split(' ');
                    string addmail = cmd[1];
                    int montant = 0;
                    try
                    {
                        montant = int.Parse(cmd[2]);
                    }
                    catch
                    {
                        Console.WriteLine("Mauvaise commande (Addmoney mail montant)", Console.ForegroundColor = ConsoleColor.Red);
                    }
                    MySqlCommand addcmd = new MySqlCommand("UPDATE users SET balance = '" + montant + "' WHERE email = '" + addmail + "'", connection);
                    try
                    {
                        addcmd.ExecuteNonQuery();
                        Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                        goto start;
                    }
                    catch
                    {
                        Console.WriteLine("Commande non éxécutée, mail non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                    }
                }
                else
                {
                    if (command == "help")
                    {
                        Console.WriteLine("Addmoney | ban | forgive | sendnotif | help", Console.ForegroundColor = ConsoleColor.Cyan);
                        goto start;

                    }
                    else
                    {
                        if (command.Contains("ban"))
                        {
                            string[] splitter = command.Split(' ');
                            string toban = splitter[1];
                            MySqlCommand cmds = new MySqlCommand("UPDATE users SET ban = 1 WHERE email = '" + toban + "'", connection);
                            try
                            {
                                cmds.ExecuteNonQuery();
                                Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                goto start;
                            }
                            catch
                            {
                                Console.WriteLine("Commande non éxécutée, mail non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                goto start;
                            }


                        }
                        else
                        {
                            if (command.Contains("forgive"))
                            {
                                string[] splitter = command.Split(' ');
                                string toban = splitter[1];
                               
                                MySqlCommand cmds = new MySqlCommand("UPDATE users SET ban = '0' WHERE email = '" + toban + "'", connection);
                                try
                                {
                                    cmds.ExecuteNonQuery();
                                    Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                    goto start;
                                }
                                catch
                                {
                                    Console.WriteLine("Commande non éxécutée, mail non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                    goto start;
                                }
                            }
                            else
                            {
                                if(command.Contains("setrank"))
                                {
                                    string[] splitter = command.Split(' ');
                                    string tograde = splitter[1];
                                    MySqlCommand gradecmd = new MySqlCommand("UPDATE users SET vip = '" + splitter[2] + "' WHERE email = '" + tograde + "'", connection);
                                    try
                                    {
                                        gradecmd.ExecuteNonQuery();
                                        Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                        goto start;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Commande non éxécutée, mail non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                        goto start;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Commande inexistante, tapez 'help' pour accéder a la liste des commandes", Console.ForegroundColor = ConsoleColor.Red);
                                    goto start;
                                }
                            }
                            
                        }
                    }
                }
            }
            }
    }
}
