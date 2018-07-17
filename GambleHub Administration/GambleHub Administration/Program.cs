using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
            string ip = "127.0.0.1";
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
                if (command.Contains("Editmoney") || command.Contains("editmoney"))
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
                        Console.WriteLine("Mauvaise commande (Editmoney pseudo montant)", Console.ForegroundColor = ConsoleColor.Red);
                    }
                    MySqlCommand addcmd = new MySqlCommand("UPDATE users SET balance = '" + montant + "' WHERE username = '" + addmail + "'", connection);
                    try
                    {
                        addcmd.ExecuteNonQuery();
                        Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                        goto start;
                    }
                    catch
                    {
                        Console.WriteLine("Commande non éxécutée, pseudo non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                }
                else
                {
                    if (command == "help")
                    {
                        Console.WriteLine("- editmoney pseudo montant" +
                            "\r\n"+
                            "- ban [PSEUDO] " +
                            "\r\n" +
                            "- forgive [PSEUDO] \r\n" +
                            "- sendnotif\r\n" +
                            "- sendglobal" +

                            "\r\n- playban [PSEUDO]" +
                            "\r\n- playforgive [PSEUDO]" +
                            "\r\n- help", Console.ForegroundColor = ConsoleColor.Cyan);
                        goto start;

                    }
                    else
                    {
                        if (command.Contains("ban"))
                        {
                            string[] splitter = command.Split(' ');
                            string toban = splitter[1];
                            MySqlCommand cmds = new MySqlCommand("UPDATE users SET ban = 1 WHERE username = '" + toban + "'", connection);
                            try
                            {
                                cmds.ExecuteNonQuery();
                                Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                goto start;
                            }
                            catch
                            {
                                Console.WriteLine("Commande non éxécutée, pseudo non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                goto start;
                            }


                        }
                        else
                        {
                            if (command.Contains("forgive"))
                            {
                                string[] splitter = command.Split(' ');
                                string toban = splitter[1];
                               
                                MySqlCommand cmds = new MySqlCommand("UPDATE users SET ban = '0' WHERE username = '" + toban + "'", connection);
                                try
                                {
                                    cmds.ExecuteNonQuery();
                                    Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                    goto start;
                                }
                                catch
                                {
                                    Console.WriteLine("Commande non éxécutée, mail non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    goto start;
                                }
                            }
                            else
                            {
                                if(command.Contains("setrank"))
                                {
                                    string[] splitter = command.Split(' ');
                                    string tograde = splitter[1];
                                    MySqlCommand gradecmd = new MySqlCommand("UPDATE users SET vip = '" + splitter[2] + "' WHERE username = '" + tograde + "'", connection);
                                    try
                                    {
                                        gradecmd.ExecuteNonQuery();
                                        Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                        goto start;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Commande non éxécutée, pseudo non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        goto start;
                                    }
                                }
                                else
                                {
                                    if (command.Contains("playban"))
                                    {
                                        string[] splitter = command.Split(' ');
                                        string tograde = splitter[1];
                                        MySqlCommand gradecmd = new MySqlCommand("UPDATE users SET playban = '1' WHERE username = '" + tograde + "'", connection);
                                        try
                                        {
                                            gradecmd.ExecuteNonQuery();
                                            Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                            goto start;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Commande non éxécutée, pseudo non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            goto start;
                                        }
                                    }
                                    else
                                    {
                                        if (command.Contains("playforgive"))
                                        {
                                            string[] splitter = command.Split(' ');
                                            string tograde = splitter[1];
                                            MySqlCommand gradecmd = new MySqlCommand("UPDATE users SET playban = '0' WHERE username = '" + tograde + "'", connection);
                                            try
                                            {
                                                gradecmd.ExecuteNonQuery();
                                                Console.WriteLine("Commande éxécutée", Console.ForegroundColor = ConsoleColor.Cyan);
                                                goto start;
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Commande non éxécutée, pseudo non trouvé ?", Console.ForegroundColor = ConsoleColor.Red);
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                goto start;
                                            }
                                        }
                                        else
                                        {
                                            if (command.Contains("sendnotif"))
                                            {
                                                string tousername = "";
                                                string texte = "";
                                                Console.WriteLine("A quel joueur souhaitez vous envoyer une notification ? 'back' pour annuler", Console.ForegroundColor = ConsoleColor.Cyan);

                                                string whatuser = Console.ReadLine() ; // sendnotif username [texte]
                                                if(whatuser == "back")
                                                {
                                                    goto start;
                                                }
                                                else
                                                {
                                                    tousername = whatuser;
                                                    Console.WriteLine("Quel texte souhaitez vous envoyer a " + tousername + "  ? 'back' pour annuler");
                                                    string whattext = Console.ReadLine();
                                                    if(whattext == "back")
                                                    {
                                                        goto start;
                                                        
                                                    }
                                                    else
                                                    {
                                                        texte = whattext;
                                                        TcpClient client = new TcpClient(ip, 9856);
                                                        NetworkStream n = client.GetStream();
                                                        string msg = "SendNotifRequest:|"+ tousername + "|"+texte;
                                                        byte[] message = Encoding.Unicode.GetBytes(msg);
                                                        n.Write(message, 0, message.Length);
                                                        Console.WriteLine("Tentative d'envoi de la notification en cour...", Console.ForegroundColor = ConsoleColor.Cyan);
                                                        goto start;
                                                    }

                                                }
                                            }
                                        }
                                    }


                                }
                                //else
                                //{
                                //    Console.WriteLine("Commande inexistante, tapez 'help' pour accéder a la liste des commandes", Console.ForegroundColor = ConsoleColor.Red);
                                //    Console.ForegroundColor = ConsoleColor.Cyan;
                                //    goto start;
                                //}
                            }
                            
                        }
                    }
                }
            }
            }
    }
}
