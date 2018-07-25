using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using SmiteSimulator.Classes;
using SmiteSimulator.Interfaces;

namespace SmiteSimulator.Helpers
{
    class StatsGrabber
    {
        public HtmlDocument GetHtmlDocument(string url)
        {
            // object to store html doc
            var doc = new HtmlDocument();

            // webclient used to download the web page info
            var webClient = new WebClient();

            // download the web page
            var page = webClient.DownloadString(url);

            // load the html into the doc object
            doc.LoadHtml(page);

            return doc;
        }

        public List<IEntity> GetAllGodNames()
        {
            EntityManager entityManager = new EntityManager();
            var godList = entityManager.CreateEntityList(typeof(God));

            // get the page html data
            var doc = GetHtmlDocument("http://smite.wikia.com/wiki/Category:Gods");

            // select the nodes we want
            var nodes = doc.DocumentNode.SelectNodes("//div/table/tr/td/ul/li/a");

            // loop through the selected nodes
            foreach (var node in nodes)
            {
                // add each god name to the god list
                godList.Add(
                    entityManager.CreateSingleEntity(typeof(God), new object[] {node.Attributes["title"].Value}));
            }

            // return the complete god list
            return godList;
        }

        public double[] BaseAndScaling(string node)
        {
            try
            {
                var node1 = double.Parse(node.Split(' ')[0]);
                var node2 = double.Parse(node.Split(' ')[1].Split('+', ')')[1]);
                return new[] {node1, node2};
            }
            catch
            {
                return BaseAndPercentScaling(node);
            }
        }
        public double[] BaseAndPercentScaling(string node)
        {
            try
            {
                var node1 = double.Parse(node.Split(' ')[0]);
                var node2 = double.Parse(node.Split(' ')[1].Split('+', '%')[1]);
                return new[] {node1, node2};
            }
            catch
            {
                var node1 = double.Parse(node.Split(' ')[0]);
                var node2 = double.Parse(node.Split('+', ')')[1]);
                var node3 = double.Parse(node.Split('\n', '%')[1].Remove(0,1));
                return new[] {node1, node2, node3};
            }
        }

        private static List<double> ExtractAttackChain(string input)
        {
            if (input.EndsWith("."))
                input = input.Remove(input.LastIndexOf(".", StringComparison.Ordinal));
            return new string(input.Where(c => char.IsDigit(c) || c == '/' || c == '.').ToArray()).Split('/').Select(double.Parse).ToList();
        }

        public object[] ParseProgression(string node)
        {
            if (node.Contains("/") && node.Contains(",\n")) // dmg and speed separated
            {
                node = node.Replace("\n", "");
                var damageScaling = node.Split(',')[0].ToLower().Replace("x damage", "").Split('/').Select(double.Parse).ToList();
                var speedScaling = node.Split(',')[1].ToLower().Replace("x speed", "").Split('/').Select(double.Parse).ToList();
                return new object[] { damageScaling, speedScaling };
            }
            if(node.Contains("/") && !node.Contains(",\n")) // normal ones with no extra message
            {
                var progression = ExtractAttackChain(node);
                return new object[] { progression, progression };
            }
            
            return new object[]{ new List<double>(){1.0}, new List<double>(){1.0} };
        }

        public IEntity GetGodInfo(IEntity god)
        {
            var godname = HttpUtility.UrlEncode(god.Name)?.Replace("+", "%20");
            var doc = GetHtmlDocument("https://smite.gamepedia.com/" + godname);

            var nodes = doc.DocumentNode.SelectNodes("//table[contains(@class, 'infobox')]/tr/td");

            // title
            god.Title = nodes[1].InnerText.Trim();
            // pantheon
            god.Pantheon = nodes[2].InnerText.Trim();
            // inhand type
            god.InhandType1 = nodes[3].InnerText.Trim().Split(',')[0];
            god.InhandType2 = nodes[3].InnerText.Trim().Split(',')[1];
            // class type
            god.Class = nodes[4].InnerText.Trim();
            // pros
            god.Pros = nodes[5].InnerText.Trim();
            // difficulty
            god.Difficulty = nodes[6].InnerText.Trim();
            // release date
            god.ReleaseDate = nodes[7].InnerText.Trim();
            // favour cost
            god.FavourCost = nodes[8].InnerText.Trim();
            // gem cost
            god.GemCost = nodes[9].InnerText.Trim();
            // voice actor
            god.VoiceActor = nodes[11].InnerText.Trim().Replace("\n",", ");

            // health + scaling
            var bands = BaseAndScaling(nodes[12].InnerText.Trim());
            god.Health.SetBaseAndScaling(bands);

            // mana + scaling
            bands = BaseAndScaling(nodes[13].InnerText.Trim());
            god.Mana.SetBaseAndScaling(bands);

            // speed + scaling
            bands = BaseAndScaling(nodes[14].InnerText.Trim());
            god.Speed.SetBaseAndScaling(bands);

            // range + scaling
            bands = BaseAndScaling(nodes[15].InnerText.Trim());
            god.Range.SetBaseAndScaling(bands);

            // attack speed + scaling
            bands = BaseAndScaling(nodes[16].InnerText.Trim());
            god.AttacksPerSecond.SetBaseAndScaling(bands);

            // inhand damage + scaling
            bands = BaseAndScaling(nodes[17].InnerText.Trim());
            god.InhandDamage.SetBaseAndScaling(bands);

            // AA progression
            var progression = ParseProgression(nodes[18].InnerText.Trim());
            god.ProgressionDamageScaling = (List<double>)progression[0];
            god.ProgressionSpeedScaling = (List<double>)progression[1];

            // physical protections + scaling
            bands = BaseAndScaling(nodes[19].InnerText.Trim());
            god.PhysicalProtections.SetBaseAndScaling(bands);

            // magical protections + scaling
            bands = BaseAndScaling(nodes[20].InnerText.Trim());
            god.MagicalProtections.SetBaseAndScaling(bands);

            // health regen + scaling
            bands = BaseAndScaling(nodes[21].InnerText.Trim());
            god.HP5.SetBaseAndScaling(bands);
            
            // mana regen + scaling
            bands = BaseAndScaling(nodes[22].InnerText.Trim());
            god.MP5.SetBaseAndScaling(bands);
            
            return god;
        }

        public List<IEntity> GetAllSmiteGodsAndStats()
        {
            var AllGods = GetAllGodNames();

            Parallel.ForEach(AllGods, (currentGod) =>
            {
                try
                {
                    var s = new StatsGrabber();
                    s.GetGodInfo(currentGod);
                }
                catch{/*Ignored*/}
            });

            return AllGods;
        }

        public GodCollection GetAllSmiteGodsAndStatsGodCollection()
        {
            var allGods = GetAllGodNames();

            Parallel.ForEach(allGods, (currentGod) =>
            {
                try
                {
                    var s = new StatsGrabber();
                    s.GetGodInfo(currentGod);
                }
                catch {/*Ignored*/}
            });

            return new GodCollection(allGods);
        }
    }
}
