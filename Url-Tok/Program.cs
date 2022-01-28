using Leaf.xNet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            }

            Interface();
        }
        public static void Interface()
        {
            Task.Factory.StartNew(delegate ()
            {
                Console.Title = String.Format("UrlTok by Avoidy - github.com/Avoidy - Avoidy#3443", new object[]
                {

                });
            });

            Console.WriteLine("UrlTok by Avoidy\n");
            Console.WriteLine("[1] Filter TikTok Urls");
            Console.WriteLine("[2] Convert Tiktok Urls\n");
            Console.Write("[OPTION] :: ");
            int interOption = Convert.ToInt32(Console.ReadLine());

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
            Console.WriteLine("UrlTok: Converting\n");

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

                    Utils.tiktokUrls.Distinct().ToList();

                    foreach (string url in Utils.tiktokUrls)
                    {
                        Utils.UrlCount++;
                        Console.Write("Currently converting url: {0}/{1}\r", Utils.UrlCount, Utils.tiktokUrls.Count.ToString());
                        
                        if (url.Contains(".com/@"))
                        {
                            string longCreator = url.Split(new string[]
                            {
                                ".com/@"
                            }, StringSplitOptions.None)[1];

                            if (longCreator.Contains("/video/")) {

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
                        Tikmate(url);
                    }

                    Console.WriteLine("Done converting {0} urls!", Utils.tiktokUrls.Count.ToString());
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
                    Thread.Sleep(5000);
                    Tikmate(Url);
                }

                JObject jobj = JObject.Parse(Utils.tikmateRequest);
                string tikToken = jobj.SelectToken("token").ToString();
                string tikID = jobj.SelectToken("id").ToString();

                string tikUrl = string.Concat(new string[]
                {
                    "https://tikmate.app/download/",
                    tikToken,
                    "/",
                    tikID,
                    ".mp4?hd=1"
                });

                Console.WriteLine(tikUrl);

                Parser(tikUrl);
            }
        }

        private static void Parser(string convert)
        {
            var ctimeName = DateTime.Now.ToString("yyyy-MM-dd");

            var combined = string.Format("{0}-{1}-{2}", new object[]
            {
                ctimeName,
                Utils.videoCreator,
                Utils.videoID
            });

            if (combined.Contains("?is_copy_url=1&is_from_webapp=v1"))
            {
                combined = combined.Split(new string[]
                {
                    "?is_copy_url=1&is_from_webapp=v1"
                }, StringSplitOptions.None)[0];
            }

            var parseLocation = AppDomain.CurrentDomain.BaseDirectory + @"\TikTok\Converted\" + dirTime + @"\" + combined + ".mp4";

            using (var Client = new WebClient())
            {
                Client.DownloadFile(convert, parseLocation); // Download the tiktok video
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
        }
    }
}
