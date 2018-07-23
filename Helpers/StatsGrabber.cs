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

        public double[] baseAndScaling(string node)
        {
            var node1 = double.Parse(node.Split(' ')[0]);
            var node2 = double.Parse(node.Split(' ')[1].Split('+', ')')[1]);
            return new [] {node1, node2};
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
            god.SetInhandType(nodes[3].InnerText.Trim());
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

            // health
            var bands = baseAndScaling(nodes[12].InnerText.Trim());
            god.SetHealth(bands[0]);
            god.SetHealthIncreasePerLevel(bands[1]);

            return god;
        }
    }
}
