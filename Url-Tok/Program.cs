using Leaf.xNet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Console = Colorful.Console;

/*                     

[*] Author: Avoidy - Github.com/Avoidy - Avoidy#3443

[*] This is a free product, for any inquiries or problems please contact me with the details above.

[*] You may not resell this product as a proprietary product!

*/

namespace Url_Tok
{
    internal class Program
    {
        public static string dirTime = DateTime.Now.ToString("dd-MM-yyyy");
        public static string txtTime = DateTime.Now.ToString("hh-mm");

        [STAThread]
        static void Main(string[] args)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\");
            }

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\");

            }

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\");

            }

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\" + dirTime + @"\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\" + dirTime + @"\");

            }

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\" + dirTime + @"\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\" + dirTime + @"\");
            }

            Interface();
        }

        public static void Design()
        {
            Console.WriteLine(@"  _   _      _ _____     _    ", Color.MediumPurple);
            Console.WriteLine(@" | | | |_ __| |_   _|__ | | __", Color.MediumPurple);
            Console.WriteLine(@" | | | | '__| | | |/ _ \| |/ /", Color.MediumPurple);
            Console.WriteLine(@" | |_| | |  | | | | (_) |   < ", Color.MediumPurple);
            Console.WriteLine(@"  \___/|_|  |_| |_|\___/|_|\_\", Color.MediumPurple);
            Console.WriteLine(@"                              ", Color.MediumPurple);
        }

        public static void Interface()
        {
            // Set main title
            Console.Title = "UrlTok by Avoidy - github.com/Avoidy - Avoidy#3443";

            // Design
            Design();

            // Option 0
            Console.Write("[", Color.MediumPurple);
            Console.Write("0", Color.White);
            Console.Write("]", Color.MediumPurple);
            Console.Write(" UrlTok Options\n", Color.White);

            // Option 1
            Console.Write("[", Color.MediumPurple);
            Console.Write("1", Color.White);
            Console.Write("]", Color.MediumPurple);
            Console.Write(" Filter to Urls\n", Color.White);

            // Option 2
            Console.Write("[", Color.MediumPurple);
            Console.Write("2", Color.White);
            Console.Write("]", Color.MediumPurple);
            Console.Write(" Convert to Mp4\n\n", Color.White);

            //Option
            Console.Write("[", Color.MediumPurple);
            Console.Write("OPTION", Color.White);
            Console.Write("]", Color.MediumPurple);
            Console.Write(" >> ", Color.White);


            int interOption = Convert.ToInt32(Console.ReadLine()); // Option Input

            switch (interOption)
            {
                case 1:
                    Console.Clear();
                    Filter();
                    break;
                case 2:
                    Console.Clear();
                    Converter();
                    break;
                default:
                    Console.Clear();
                    Interface();
                    break;
            }
        }
        public static void Filter()
        {
            Console.WriteLine("UrlTok: Filtering\n");

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    var filterThis = File.ReadAllLines(filePath);

                    foreach (string filter in filterThis)
                    {
                        Utils.toFilterCount++;
                        Utils.toFilter.Add(filter);
                    }

                    foreach (string filtered in Utils.toFilter)
                    {
                        if (filtered.Contains("href=\"https://www.tiktok.com/")) // Searches valid lines that include tiktok video urls
                        {
                            Utils.Filtered.Add(filtered);
                        }
                    }

                    foreach (string split in Utils.Filtered)
                    {
                        string washedUrl = split.Split(new string[]
                        {
                            "href=\""
                        }, StringSplitOptions.None)[1]; // Split urls from html code

                        string filterUrl = washedUrl.Split(new string[]
                        {
                            "\""
                        }, StringSplitOptions.None)[0]; // Removes unnecessary characters from line

                        Utils.Washed.Add(filterUrl);
                    }

                    Utils.Washed.Distinct().ToList().ToString();
                    Utils.WashedCount = Utils.Washed.Count();
                    Console.WriteLine("Filtered {0} urls!", Utils.WashedCount);

                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\" + dirTime + @"\" + txtTime + " (" + Utils.WashedCount + ")" + @".txt"))
                    {
                        // File.Create(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\" + dirTime + @"\" + txtTime + @".txt");
                    }

                    using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\" + dirTime + @"\" + txtTime + " (" + Utils.WashedCount + " Urls)" + @".txt"))
                    {
                        foreach (string ttUrl in Utils.Washed)
                        {
                            sw.WriteLine(ttUrl);
                        }
                    };

                    Console.WriteLine("Location: {0}", new Object[]
                    {
                        AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Filtered\" + dirTime + @"\" + txtTime + " (" + Utils.WashedCount + " Urls)" + @".txt"
                    });

                    Console.WriteLine("Press any key to return!");
                    Console.ReadKey();
                    Console.Clear();
                    Interface();

                }
            }

        }
        public static void Converter()
        {

            // Set title
            Console.Title = "UrlTok by Avoidy - Module 2 - Convert to Mp4";

            //Design
            Design();

            // Module display
            Console.Write("[", Color.MediumPurple);
            Console.Write("Module", Color.White);
            Console.Write("]", Color.MediumPurple);
            Console.Write(" >> 2\n", Color.White);
            Console.Write("[", Color.MediumPurple);
            Console.Write("Description", Color.White);
            Console.Write("]", Color.MediumPurple);
            Console.Write(" >> Convert to Mp4\n", Color.White);

            // File input
            Console.Write("[", Color.MediumPurple);
            Console.Write("Input", Color.White);
            Console.Write("]", Color.MediumPurple);
            Console.Write(" >> Please open your file!\n\n", Color.White);

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    var filterThis = File.ReadAllLines(filePath);

                    foreach (string url in filterThis)
                    {
                        Utils.tiktokUrls.Add(url);
                    }

                    Utils.tiktokUrls.Distinct().ToList(); // Removes duplicates
                    Utils.tiktokUrls.Sort(); // Sort on user
                    var urlsAmount = Utils.tiktokUrls.Count();

                    Console.Write("Successfully loaded", Color.MediumPurple);
                    Console.Write(" {0}", Color.White, urlsAmount);
                    Console.Write(" valid TikTok urls!\n\n", Color.MediumPurple, urlsAmount);

                    foreach (string url in Utils.tiktokUrls)
                    {
                        Utils.UrlCount++;

                        if (url.Contains(".com/@"))
                        {
                            string longCreator = url.Split(new string[]
                            {
                                ".com/@"
                            }, StringSplitOptions.None)[1];

                            if (longCreator.Contains("/video/"))
                            {

                                Utils.videoCreator = longCreator.Split(new string[]
                                {
                                    "/video/"
                                }, StringSplitOptions.None)[0];
                            }

                            if (longCreator.Contains("/video/"))
                            {

                                Utils.videoID = longCreator.Split(new string[]
                                {
                                    "/video/"
                                }, StringSplitOptions.None)[1];
                            }

                        }

                        Console.Write("Converting Url ", Color.White);
                        Console.Write("[", Color.MediumPurple);
                        Console.Write("{0}", Color.White, Utils.UrlCount);
                        Console.Write("] ", Color.MediumPurple);
                        Console.Write(" | ID:");
                        Console.Write(" {0}\n", Color.MediumPurple, Utils.videoID);

                        Tikmate(url);
                    }


                    Console.Write("Done Converting", Color.White);
                    Console.Write(" {0}", Color.MediumPurple, Utils.UrlCount);
                    Console.Write(" urls!", Color.White);
                    Console.WriteLine("Press any key to return!");
                    Console.ReadKey();
                    Console.Clear();
                    Interface();
                }
            }

        }

        private static void Tikmate(string Url)
        {
            using (HttpRequest httpRequest = new HttpRequest())
            {
                httpRequest.AllowAutoRedirect = false;
                httpRequest.Proxy = null;
                httpRequest.UserAgentRandomize(); // Avoids status code 403

                string postData = string.Concat(new string[]
                {
                    "url=",
                    Url
                });

                string requestUrl()
                {
                    return "https://api.tikmate.app/api/lookup"; // Might change in the future, will update this when necessary!
                }

                try
                {
                    Utils.tikmateRequest = httpRequest.Post(requestUrl(), postData, "application/x-www-form-urlencoded; charset=UTF-8").ToString();
                }
                catch (Exception)
                {
                    Thread.Sleep(1);
                    string nothing = string.Empty;
                    if (Utils.tikmateRequest == nothing)
                    {
                        Console.WriteLine("[!] Private or Deleted Video!", Color.Red);
                    }
                    else
                    {
                        Console.WriteLine("[!] Api error! 5000ms timeout!", Color.Red);
                    }
                }

                // Console.WriteLine("{" + Utils.tikmateRequest + "}");
                /* 
                Example: 
                {"author_avatar":"https://p16-sign-va.tiktokcdn.com/tos-maliva-avt-0068/3c7d9b00a233621b6b70d3b39c7971c0~c5_1080x1080.webp?x-expires=1643580000\u0026x-signature=l%2FV2m%2BC%2FrliCQv%2F3Quvju%2FPPeso%3D","author_id":"_acyeast_","author_name":"AcyEast","comment_count":1110,"create_time":"Jun 2, 2021","id":"6968921591354821894","like_count":685761,"share_count":20063,"success":true,"token":"ctTtCeMOVb2KtzV7Fnv9_HjE5tvqqt3gmAd9_nTiJ7YUEFuTWaetr-BYJ5YgkJeMb_19jHX7MPov3zzR"}
                */

                try
                {
                    JObject jobj = JObject.Parse(Utils.tikmateRequest);
                    string tikToken = jobj.SelectToken("token").ToString();
                    string tikID = jobj.SelectToken("id").ToString();
                    Utils.tikDate = jobj.SelectToken("create_time").ToString();
                    

                    Utils.tikUrl = string.Concat(new string[]
                    {
                    "https://tikmate.app/download/",
                    tikToken,
                    "/",
                    tikID,
                    ".mp4?hd=1"
                    });

                    Parser(Utils.tikUrl);
                } 
                catch (Exception)
                {
                    return;
                } 
            }
        }

        private static void Parser(string convert)
        {
            var ctimeName = DateTime.Now.ToString("yyyy-MM-dd");

            var combined = string.Format("{0}-{1}-{2}", new object[]
            {
                Utils.videoCreator,
                Utils.videoID,
                Utils.tikDate
            });

            if (combined.Contains("?is_copy_url=1&is_from_webapp=v1"))
            {
                combined = combined.Split(new string[]
                {
                    "?is_copy_url=1&is_from_webapp=v1"
                }, StringSplitOptions.None)[0];
            }

            bool dirsort = true;

            if (dirsort == true)
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\" + Utils.videoCreator + @"\"))
                {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\" + dirTime + @"\" + Utils.videoCreator + @"\");

                }
            }


            if (dirsort == true)
            {
                Utils.parseLocation = AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\" + dirTime + @"\" + Utils.videoCreator + @"\" + combined + ".mp4";

            } else
            {
                Utils.parseLocation = AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\" + dirTime + @"\" + combined + ".mp4";
            }
            

            using (var Client = new WebClient())
            {
                Client.DownloadFile(convert, Utils.parseLocation); // Download the tiktok video
                return;
            }
        }

        public static class Utils
        {
            public static string filterLoc = string.Empty;
            public static string tikmateRequest = string.Empty;
            public static List<string> toFilter = new List<string>();
            public static List<string> Filtered = new List<string>();
            public static List<string> Washed = new List<string>();
            public static List<string> tiktokUrls = new List<string>();
            public static int toFilterCount = 0;
            public static int WashedCount = 0;
            public static int UrlCount = 0;

            public static string videoCreator = string.Empty;
            public static string videoID = string.Empty;
            public static string tikUrl = string.Empty;
            public static string tikDate = string.Empty;
            public static string parseLocation = string.Empty;
        }
    }
}
