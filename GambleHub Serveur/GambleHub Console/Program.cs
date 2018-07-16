using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using Discord.Net;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Net.Mail;
using System.IO;
using Discord.Rest;

namespace GambleHub_Console
{
    class Program
    {
       public static void Main(string[] args)
        {
           
            string username;
            string amount;
            Console.ForegroundColor = ConsoleColor.Cyan;
            //FileStream filestream = new FileStream("gamblelogs.txt", FileMode.Append);
            //var streamwriter = new StreamWriter(filestream);
            //streamwriter.AutoFlush = true;
            //Console.SetOut(streamwriter, Console.Out);
            //Console.SetError(streamwriter);
            string mail;
            string password;
            int refresh = 1;
            int joueurs = 0;
            int status;
            int statusdb;
            Console.WriteLine("[" + DateTime.Now + "] " +"Serveur en démarrage...");

            TcpListener listen = new TcpListener(IPAddress.Any, 9856);
            MySqlConnection connection = new MySqlConnection("datasource=mysql-gamblehuboff.alwaysdata.net; port=3306;Initial Catalog='gamblehuboff_db';username=158469;password=K8LhE283et");

            try
            {
                listen.Stop();
                listen.Start();

            }
            catch
            {
                status = 1;
                Console.WriteLine("[" + DateTime.Now + "] " + "Connexion au serveur impossible", Console.ForegroundColor = ConsoleColor.Red);
                Console.Read();

                Environment.Exit(0);

            }
            try
            {
                connection.Open();
            }
            catch
            {
                statusdb = 1;
                Console.WriteLine("[" + DateTime.Now + "] " + "Connexion a la base de donnée impossible", Console.ForegroundColor = ConsoleColor.Red);
                Console.Read();
                Environment.Exit(0);
            }



            Console.WriteLine("[" + DateTime.Now + "] " + "Serveur démarré, prêt à recevoir des connexions");
            status = 0;
            statusdb = 0;
            Console.WriteLine("[" + DateTime.Now + "] " + "Démarrage du Bot Discord...");





            DiscordSocketClient _client;
            MainAsync();
             async Task MainAsync()
            {
                _client = new DiscordSocketClient();

                _client.Log += Log;
                _client.MessageReceived += MessageReceived;
                _client.UserJoined += UserJoineds;
                string token = "NDY1OTQ5MTEzNjMyOTQ4MjI0.DipQdA.NhiHPATq34wlZhOeSFhBfUD10Vo"; // Remember to keep this private!
                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();
              
                
        // Block this task until the program is closed.
        await Task.Delay(-1);
            }

            async Task UserJoineds(SocketGuildUser user)
            {
              
            }
             async Task MessageReceived(SocketMessage message)
             {
                
                if (message.Content == "!list")
                {
                    string verifsolde = "SELECT * FROM retrait";

                    MySqlCommand cmds = new MySqlCommand(verifsolde, connection);
                    MySqlDataAdapter ad = new MySqlDataAdapter(cmds);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                                
                                foreach(DataRow row in dt.Rows)
                                {
                                     await message.Channel.SendMessageAsync("`------- NEW -------`");
                                   // string messages = "[" + i.ToString() + "] - " + dt[0] + "| " + "Solde Compte : " + reader.GetValue(1).ToString() + "|" + " Montant du retrait : " + reader.GetValue(2).ToString();
                        foreach (var item in row.ItemArray)
                        {
                            try
                            {
                                await message.Channel.SendMessageAsync(item.ToString());
                                
                            }
                            catch
                            {
                                Console.WriteLine("[" + DateTime.Now + "] " + "Impossible d'envoyer la liste des retraits", Console.ForegroundColor = ConsoleColor.Red);
                                await message.Channel.SendMessageAsync("Impossible d'envoyer la liste des retraits");
                            }

                        }
                    }
                    await message.Channel.SendMessageAsync("Fin de la liste !");
                            
                        
                        //else
                        //{
                        //    await message.Channel.SendMessageAsync("Pas de demandes de retrait");
                        //}
                    

                                
                }
                string mess = message.ToString();

                

       
                if (mess.Contains("!finish"))
                {
                    
                    string[] splitter = mess.Split(' ');
                    string email = splitter[1];
                    string cmd = "DELETE FROM retrait WHERE mail = '" + email + "'";
                    MySqlCommand rem = new MySqlCommand(cmd, connection);
                    try
                    {
                        rem.ExecuteNonQuery();
                        await message.Channel.SendMessageAsync("Terminé avec succès !");
                    }
                    catch
                    {
                        await message.Channel.SendMessageAsync("Demande de retrait non trouvée !");
                    }
                    //RequireUserPermission(GuildPermission.Administrator)


                }
            }
            
            

            Task Log(LogMessage msg)
             {
                Console.WriteLine(msg.ToString());
                return Task.CompletedTask;
             }

            


            

            

            Console.WriteLine("Bot Discord démarré");
            while (refresh == 1)
            {
                
                Console.Title = "GAMBLEHUB - SERVEUR | PORT 9856 | JOUEURS EN LIGNE : " + joueurs.ToString();
                

                TcpClient client = listen.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int data = stream.Read(buffer, 0, client.ReceiveBufferSize);
                string msg = Encoding.Unicode.GetString(buffer, 0, data);
                Console.WriteLine("Received : " + msg, Console.ForegroundColor = ConsoleColor.Cyan);

                
                if (msg.Contains("AuthRequest"))
                {
                    // exemple : AuthRequest:|mail@gmail.com|alexandre    
                    // ici AuthRequest = 0 etc.... on veut verif les valeurs 1 & 2

                    msg.Trim();
                    string[] spliter = msg.Split('|');

                    MySqlDataAdapter adapter;

                    DataTable table = new DataTable();
                    adapter = new MySqlDataAdapter("SELECT `email`, `password` FROM `users` WHERE `email` = '" + spliter[1] + "' AND `password` = '" + spliter[2] + "'", connection);
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        //bon send msg
                        
                        try
                        {
                            MySqlCommand verifban = new MySqlCommand("SELECT ban FROM users WHERE email = '" + spliter[1] + "'", connection);
                            using(MySqlDataReader reader = verifban.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        string ban = reader.GetValue(0).ToString();
                                        if(ban == "0")
                                        {
                                            string msgsendinfo = "Connexion acceptée";
                                            byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                                            stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                                            Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Connexion acceptée", Console.ForegroundColor = ConsoleColor.Green);
                                        }
                                        else
                                        {
                                            Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Connexion refusée (compte banni)", Console.ForegroundColor = ConsoleColor.Red);
                                            try
                                            {

                                                string msgsendinfo = "Connexion refusée";
                                                byte[] messagesendrefuse = Encoding.Unicode.GetBytes(msgsendinfo);
                                                stream.Write(messagesendrefuse, 0, messagesendrefuse.Length);
                                            }
                                            catch
                                            {
                                                Console.WriteLine("[" + DateTime.Now + "] " + "Impossible d'envoyer la réponse...", Console.ForegroundColor = ConsoleColor.Red);
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                        catch
                        {
                            Console.WriteLine("[" + DateTime.Now + "] " + "Impossible d'envoyer la réponse...", Console.ForegroundColor = ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        //mauvais send msg
                        Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Connexion refusée", Console.ForegroundColor = ConsoleColor.Red);
                        try
                        {

                            string msgsendinfo = "Connexion refusée";
                            byte[] messagesendrefuse = Encoding.Unicode.GetBytes(msgsendinfo);
                            stream.Write(messagesendrefuse, 0, messagesendrefuse.Length);
                        }
                        catch
                        {
                            Console.WriteLine("[" + DateTime.Now + "] " + "Impossible d'envoyer la réponse...", Console.ForegroundColor = ConsoleColor.Red);
                        }
                    }
                }// requête connexion
                if (msg.Contains("Nouvelle connexion"))
                {
                    joueurs++;
                }// joueurs en ligne
                if (msg.Contains("Nouvelle deconnexion"))
                {
                    joueurs--;
                }// joueurs en ligne
                if (msg.Contains("RegisterRequest"))
                {
                    msg.Trim();
                    string[] regrequest = msg.Split('|'); // 0 useless
                                                          // prenom - nom - email - age - sexe - mdp - vip - ban - balance - pseudo
                    string prenom = regrequest[1];
                    string nom = regrequest[2];
                    string email = regrequest[3];
                    string age = regrequest[4];
                    string sexe = regrequest[5];
                    string mdp = regrequest[6];
                    string vip = regrequest[7];
                    string ban = regrequest[8];
                    string balance = regrequest[9];
                    string pseudo = regrequest[10];

                    DataSet ds = new DataSet();
                    MySqlConnection regcon = new MySqlConnection("datasource=mysql-gamblehuboff.alwaysdata.net; port=3306;Initial Catalog='gamblehuboff_db';username=158469;password=K8LhE283et");
                    regcon.Open();
                    MySqlCommand cmds = new MySqlCommand("SELECT * FROM users WHERE username = '" + pseudo + "' ", regcon);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmds);
                    DataSet dsmail = new DataSet();
                    MySqlCommand cmdsmail = new MySqlCommand("SELECT * FROM users WHERE email = '" + email + "'", regcon);
                    MySqlDataAdapter daemail = new MySqlDataAdapter(cmdsmail);
                    daemail.Fill(dsmail);

                    endpoint:
                    // prenom - nom - email - age - sexe - password


                    int xmail = dsmail.Tables[0].Rows.Count;
                    if (xmail > 0)
                    {

                        string msgsendinfo = "Mail déjà pris";
                        byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                        stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                        Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Mail déjà pris", Console.ForegroundColor = ConsoleColor.Red);
                    }
                    else
                    {
                        da.Fill(ds);
                        int psdo = ds.Tables[0].Rows.Count;
                        if (psdo > 0)
                        {
                            string msgsendinfo = "Pseudo déjà pris";
                            byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                            stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                            Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Pseudo déjà pris", Console.ForegroundColor = ConsoleColor.Red);

                        }
                        else
                        {

                            string reg = "INSERT INTO users(prenom, nom, email, age, sexe, password, vip, ban, balance, username) VALUES('" + prenom + "','" + nom + "','" + email + "','" + age + "', '" + sexe + "', '" + mdp + "', '" + "0" + "','" + "0" + "', '" + "0" + "', '" + pseudo + "')";
                            MySqlCommand cmd = new MySqlCommand(reg, regcon);
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch
                            {
                                Console.WriteLine("[" + DateTime.Now + "] " + "Erreur fatale lors de l'inscription !", Console.ForegroundColor = ConsoleColor.Red);

                            }
                            //bon
                            string msgsendinfo = "Inscrit avec succès";
                            byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                            stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                            Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Inscrit avec succès !", Console.ForegroundColor = ConsoleColor.Green);

                        }
                    }








                }
                if (msg.Contains("CreateGame"))
                {
                   
                    
                }
                if (msg.Contains("EndGame"))
                {

                }
                if (msg.Contains("ShowGame"))
                {

                } // récupère les games avec le tag actif pour que le client les affiche
                if (msg.Contains("JoinGame"))
                {
                    msg.Trim();
                    string[] splitter = msg.Split('|');
                    string user = splitter[1];
                    string jeu = splitter[2];
                    int rollslots = 8;
                    int rollmaxslots = 8;
                    string slots = rollslots.ToString();
                    string maxslots = "";
                    string joueurslist = "";
                    Console.WriteLine("[" + DateTime.Now + "] " + "JoinRequest de " + splitter[1] + "(" + jeu +")", Console.ForegroundColor = ConsoleColor.Gray);
                    if (jeu == "roulette")
                    {


                                    if (rollslots > 0)
                                    {
                                        rollslots--;
                                        MySqlCommand join = new MySqlCommand("UPDATE games SET joueurs = '" + joueurslist + user + "/" + "', slots = '" + rollslots.ToString() + "' WHERE id = '1' AND auteur = 'Administrateur' AND jeu = 'roulette'", connection);
                                        try
                                        {
                                            join.ExecuteNonQuery();
                                            Console.WriteLine("[" + DateTime.Now + "] " + "Le joueur " + user + " a rejoind la roulette !", Console.ForegroundColor = ConsoleColor.Cyan);
                                            string msgsendinfo = "joinsuccess";
                                            byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                                            stream.Write(messagesendaccept, 0, messagesendaccept.Length);

                                MySqlCommand cmd = new MySqlCommand("SELECT * FROM games WHERE jeu = 'roulette'", connection);
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        if (reader.Read())
                                        {
                                            int id = (int)reader.GetValue(0);
                                            string auteur = reader.GetValue(1).ToString();
                                            jeu = reader.GetValue(2).ToString();
                                            slots = reader.GetValue(3).ToString();
                                            maxslots = reader.GetValue(4).ToString();
                                            joueurslist = reader.GetValue(13).ToString();
                                            string msgsendinfos = id.ToString() + "|" + auteur + "|" + jeu + "|" + rollslots.ToString() + "|" + rollmaxslots.ToString() + "|" + joueurslist;
                                            byte[] messagesendaccepts = Encoding.Unicode.GetBytes(msgsendinfos);
                                            stream.Write(messagesendaccepts, 0, messagesendaccepts.Length);
                                            Console.WriteLine("[" + DateTime.Now + "] " + "Sent : " + msgsendinfos, Console.ForegroundColor = ConsoleColor.Gray);
                                            rollslots = int.Parse(slots);
                                            rollmaxslots = int.Parse(maxslots);
                                        }
                                    }
                                }
                                        }
                                        catch(Exception e)
                                        {
                                            Console.WriteLine("[" + DateTime.Now + "] " + "Erreur, impossible de rejoindre la roulette ! " + e.ToString(), Console.ForegroundColor = ConsoleColor.Red);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("[" + DateTime.Now + "] " + "Erreur, roulette pleine !", Console.ForegroundColor = ConsoleColor.Red);
                                        string msgsendinfo = "joinfull";
                                        byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                                        stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                                    }
                                    

                               
                           
                        
                        
                        
                    }
                }
                if (msg.Contains("BuyRankVIP"))
                {

                }
                if (msg.Contains("Disconnect")) // crash de co ?
                {
                    joueurs--;
                    Console.WriteLine("Crash de connexion | logiciel fermé ");
                }
                if (msg.Contains("PasswordRequest"))
                {
                    msg.Trim();
                    string[] spliter = msg.Split('|');
                    string mails = spliter[1];

                    MySqlConnection connections = new MySqlConnection("datasource=mysql-gamblehuboff.alwaysdata.net; port=3306;Initial Catalog='gamblehuboff_db';username=158469;password=K8LhE283et");
                    if (connections.State == System.Data.ConnectionState.Open)
                    {

                    }
                    else
                    {
                        connections.Open();
                    }
                    string prenomcmds = "SELECT nom, prenom, sexe, age, vip, balance, email, password FROM users WHERE email='" + mails + "'";
                    MySqlCommand cmds = new MySqlCommand(prenomcmds, connections);
                    using (MySqlDataReader reader = cmds.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                string resetmdp;
                                resetmdp = reader.GetValue(7).ToString();
                                MailMessage mailss = new MailMessage();


                                mailss.From = new MailAddress("gamblehuboff@gmail.com");
                                mailss.To.Add(new MailAddress(mails));

                                mailss.Subject = "Votre mot de passe GAMBLEHUB";
                                StringBuilder sbs = new StringBuilder();


                                sbs.Append("Votre mot de passe GAMBLEHUB est : " + resetmdp + "\n\n Merci d'utiliser nos services !");



                                mailss.Body = sbs.ToString();



                                mailss.Priority = MailPriority.High;
                                SmtpClient smtpss = new SmtpClient("smtp.gmail.com", 587);
                                smtpss.EnableSsl = true;
                                smtpss.Credentials = new NetworkCredential("gamblehuboff@gmail.com", "H2762KDbzt");
                                try
                                {
                                    smtpss.Send(mailss);
                                }
                                catch
                                {
                                    Console.WriteLine("[" + DateTime.Now + "] " + "Erreur d'envoi du mail", Console.ForegroundColor = ConsoleColor.Red);
                                }
                                string success = "Mail envoyé !";
                                byte[] messagesendaccept = Encoding.Unicode.GetBytes(success);
                                stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                                Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Mail envoyé !", Console.ForegroundColor = ConsoleColor.Green);

                            }
                        }
                        else
                        {
                            string fail = "Mail non trouvé";
                            byte[] messagesendaccept = Encoding.Unicode.GetBytes(fail);
                            stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                            Console.WriteLine("[" + DateTime.Now + "] " + "Sent : Mail non trouvé", Console.ForegroundColor = ConsoleColor.Red);
                        }
                    }
                }
                if (msg.Contains("PlayerInfoRequest"))
                {
                    msg.Trim();
                    string[] spliter = msg.Split('|');
                    string usermail = spliter[1];
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        goto nxtpart;
                    }
                    else
                    {
                        connection.Open();
                        goto nxtpart;
                    }
                    nxtpart:
                    string prenomcmd = "SELECT nom, prenom, sexe, age, vip, balance, email, password FROM users WHERE email='" + usermail + "'";

                    MySqlCommand cmds = new MySqlCommand(prenomcmd, connection);

                    using (MySqlDataReader reader = cmds.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            if (reader.Read())
                            {
                                string nomcompte;
                                string prenomcompte;
                                string sexe;
                                string agecompte;
                                string vipcompte;
                                string emailcompte;
                                float solde;
                                string soldecompte;
                                string mdp;
                                int vips;

                                nomcompte = reader.GetValue(0).ToString();
                                prenomcompte = reader.GetValue(1).ToString();
                                sexe = reader.GetValue(2).ToString();
                               

                                agecompte = reader.GetValue(3).ToString() + " ans";
                                vips = (int)reader.GetValue(4);
                             
                               
                                emailcompte = reader.GetValue(6).ToString();
                                solde = (float)reader.GetValue(5);
                                soldecompte = solde.ToString() + " Crédits";
                                mdp = reader.GetValue(7).ToString();

                                string msgsendinfo = "PlayerInfo:|" + nomcompte + "|" + prenomcompte + "|" + sexe + "|" + agecompte + "|" + vips.ToString() + "|" + emailcompte + "|" + soldecompte;
                                byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                                stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                                Console.WriteLine("[" + DateTime.Now + "] " + "Sent : " + msgsendinfo, Console.ForegroundColor = ConsoleColor.Gray);

                            }


                        }

                        else
                        {
                            string msgsendinfo = "Pas de compte";
                            byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                            stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                            Console.WriteLine("Sent : " + msgsendinfo, Console.ForegroundColor = ConsoleColor.Red);

                            //this.Hide();
                            //MessageBox.Show("Compte inexistant");
                            //this.Close();

                        }
                        reader.Close();

                    }
                   
                }
                if (msg.Contains("WithdrawRequest"))
                {
                    msg.Trim();
                    string[] splitter = msg.Split('|');


                    string verifsolde = "SELECT balance FROM users WHERE email='" + splitter[1] + "'";

                    MySqlCommand cmds = new MySqlCommand(verifsolde, connection);

                    using (MySqlDataReader reader = cmds.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            string solde = "";
                            int demande = 0;
                            if (reader.Read())
                            {
                                solde = reader.GetValue(0).ToString();
                                demande = int.Parse(splitter[2]);


                               

                            }
                            reader.Close();
                            try
                            {
                                MySqlCommand addtolist = new MySqlCommand("INSERT INTO retrait(mail, soldecompte, demanderetrait) VALUES('" + splitter[1] + "', '" + solde.ToString() + "', '" + demande.ToString() + "')", connection);
                                addtolist.ExecuteNonQuery();

                                string msgsendinfo = "withdrawsuccess";
                                byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                                stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                                Console.WriteLine("[" + DateTime.Now + "] " + splitter[1] + " Ajouté à la base de donnée !", Console.ForegroundColor = ConsoleColor.Green);

                            }
                            catch
                            {
                                Console.WriteLine("[" + DateTime.Now + "] " + splitter[1] + " Une demande de retrait est déjà en cour !", Console.ForegroundColor = ConsoleColor.Red);
                                string msgsendinfo = "alreadywithdraw";
                                byte[] messagesendaccept = Encoding.Unicode.GetBytes(msgsendinfo);
                                stream.Write(messagesendaccept, 0, messagesendaccept.Length);
                            }
                        }
                    }
                }

                

                
            }
        }
    }
}

