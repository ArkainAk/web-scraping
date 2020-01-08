using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScraperWebSite
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://hotelyar.com/hotel/1764/%D9%87%D8%AA%D9%84-%D9%88%DB%8C%D8%B3%D8%AA%D8%B1%DB%8C%D8%A7-%D8%AA%D9%87%D8%B1%D8%A7%D9%86";
            FetchUrl(url);
            Console.ReadLine();
        }
        public static async void FetchUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var html = await client.GetStringAsync(url);

                string tBodyTag = "<table class=\"ss-single-reserve-table\">.*<tbody>(.*)</tbody>.*</table>";
                //string resultSetPattern = "";
                //string roomCatPattern = "";
                string roomTypePattern = "<span class=\"ss-single-reserve-table-t-name\">(.*?)</span>";
                string discountPattern = "<span class=\"ss-single-reserve-table-t-off\">(.*?)</span>";
                string roomCapacityPattern = "<span>(2?)</span>";
                string extraServicePattern = @"<span>(1?|\D)</span>";
                string boardPricePattern = "<span class=\"ss-single-reserve-table-p-old\">(.*?)</span>.*</td>";
                string finalPricePattern = "<span class=\"ss-single-reserve-table-p-new\">(.*?)</span>.*</td>";
                string pointsRoomPattern = @"<span>(\d{2})</span>";
                
                string tbody = Regex.Match(html, tBodyTag, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                
                string trTag = "<tr>.*?</tr>";
                var matches = Regex.Matches(tbody, trTag, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match match in matches)
                {
                    var roomType = Regex.Match(match.Value, roomTypePattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    var discount = Regex.Match(match.Value, discountPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    var roomCapacity = Regex.Match(match.Value, roomCapacityPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    var extraService = Regex.Match(match.Value, extraServicePattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    var boardPrice = Regex.Match(match.Value, boardPricePattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    var finalPrice = Regex.Match(match.Value, finalPricePattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    var pointsRoom = Regex.Match(match.Value, pointsRoomPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;

                }

            }
        }
    }
}
