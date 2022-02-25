using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WebAPI.Models.Pagination;

namespace WebAPI.Models
{
    public class PersonFilter
    {
        public string GetContent(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            reader.Close();
            response.Close();
            return output.ToString();
        }
        public List<Person> GetPeople(string url)
        {
            string request = GetContent(url);
            List<Person> peopleInfo = JsonConvert.DeserializeObject<List<Person>>(request);
            List<Person> detailedPeople = new List<Person>();
            int i = 0;
            foreach (var person in peopleInfo)
            {
                detailedPeople.Add(JsonConvert.DeserializeObject<Person>(
                            GetContent(String.Format("{0}/{1}", url, person.Id))));
                detailedPeople[i].Id = person.Id;
                i++;
            }
            return detailedPeople;
        }
        public List<Person> FilterByAge(List<Person> people, int? AgeX = null, int? AgeY = null)
        {
            if (AgeX != null || AgeY != null)
            {
                List<Person> items = null;
                if (AgeX == null)
                    items = people.OrderBy(p => p.Age).ToList().FindAll(x => (x.Age <= AgeY));
                else if(AgeY == null)
                    items = people.OrderBy(p => p.Age).ToList().FindAll(x => (x.Age >= AgeX));
                else
                    items = people.OrderBy(p => p.Age).ToList().FindAll(x => (x.Age >= AgeX) && (x.Age <= AgeY));
                return items;
            }
            else
                return people;
        }
        public List<Person> FilterByGender(List<Person> people, string filter = "")
        {
            List<Person> items;
            if (filter == "male")
            {
                items = people.FindAll(x => x.Sex == "male").ToList();
            }
            else if (filter == "female")
            {
                items = people.FindAll(x => x.Sex == "female").ToList();
            }
            else
                items = people;

            return items;
        }
        public List<Person> Pagination(List<Person> people, PagingParameters param)
        {
            PagedList paged = new PagedList(param.PageNumber, param.PageSize, people.Count());

            var pagedItems = people.Skip((paged.CurrentPage - 1) * paged.PageSize)
                                           .Take(paged.PageSize).ToList();
            return pagedItems;
        }


    }
}
