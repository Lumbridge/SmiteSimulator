using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;
using SmiteSimulator.Classes;
using SmiteSimulator.Interfaces;
using static SmiteSimulator.Helpers.ConsoleHelper;

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

        public List<IGod> GetAllGodNames()
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

        public IGod GetGodInfo(IGod god)
        {
            var doc = GetHtmlDocument("https://smite.gamepedia.com/" + god.GetName());

            var nodes = doc.DocumentNode.SelectNodes("//table[contains(@class, 'infobox')]/tr/td");

            // title
            god.SetTitle(nodes[1].InnerText.Trim());
            // pantheon
            god.SetPantheon(nodes[2].InnerText.Trim());
            // inhand type
            god.SetInhandType1(nodes[3].InnerText.Trim().Split(',')[0]);
            god.SetInhandType2(nodes[3].InnerText.Trim().Split(',')[1]);
            // class type
            god.SetClass(nodes[4].InnerText.Trim());
            // pros
            god.SetPros(nodes[5].InnerText.Trim());
            // difficulty
            god.SetDifficulty(nodes[6].InnerText.Trim());
            // release date
            god.SetReleaseDate(nodes[7].InnerText.Trim());
            // favour cost
            god.SetFavourCost(nodes[8].InnerText.Trim());
            // gem cost
            god.SetGemCost(nodes[9].InnerText.Trim());
            // voice actor
            god.SetVoiceActor(nodes[11].InnerText.Trim());

            // health + scaling
            var bands = BaseAndScaling(nodes[12].InnerText.Trim());
            god.SetHealth(bands[0]);
            god.SetHealthIncreasePerLevel(bands[1]);

            // mana + scaling
            bands = BaseAndScaling(nodes[13].InnerText.Trim());
            god.SetMana(bands[0]);
            god.SetManaIncreasePerLevel(bands[1]);

            // speed + scaling
            bands = BaseAndScaling(nodes[14].InnerText.Trim());
            god.SetSpeed(bands[0]);
            god.SetSpeedIncreasePerLevel(bands[1]);

            // range + scaling
            bands = BaseAndScaling(nodes[15].InnerText.Trim());
            god.SetRange(bands[0]);
            god.SetRangeIncreasePerLevel(bands[1]);

            // attack speed + scaling
            bands = BaseAndScaling(nodes[16].InnerText.Trim());
            god.SetAttacksPerSecond(bands[0]);
            god.SetAttacksPerSecondIncreasePerLevelPercent(bands[1]);

            // inhand damage + scaling
            bands = BaseAndScaling(nodes[17].InnerText.Trim());
            god.SetInhandBaseDamage(bands[0]);
            god.SetInhandBaseDamageIncreasePerLevel(bands[1]);
            god.SetInhandScalingPercentage(bands[2]);

            // AA progression
            var progression = ParseProgression(nodes[18].InnerText.Trim());
            god.SetProgressionDamageScaling((List<double>)progression[0]);
            god.SetProgressionSpeedScaling((List<double>)progression[1]);

            // physical protections + scaling
            bands = BaseAndScaling(nodes[19].InnerText.Trim());
            god.SetPhysicalProtections(bands[0]);
            god.SetPhysicalProtectionsIncreasePerLevel(bands[1]);

            // magical protections + scaling
            bands = BaseAndScaling(nodes[20].InnerText.Trim());
            god.SetMagicalProtections(bands[0]);
            god.SetMagicalProtectionsIncreasePerLevel(bands[1]);

            // health regen + scaling
            bands = BaseAndScaling(nodes[21].InnerText.Trim());
            god.SetHP5(bands[0]);
            god.SetHP5IncreasePerLevel(bands[1]);
            
            // mana regen + scaling
            bands = BaseAndScaling(nodes[22].InnerText.Trim());
            god.SetMP5(bands[0]);
            god.SetMP5IncreasePerLevel(bands[1]);
            
            return god;
        }
    }
}
