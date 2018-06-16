using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private static List<Person> list = new List<Person>
        {
            new Person("yamen", "habeeb", new DateTime(1996, 8, 10)),
            new Person("alhassan", "alkhaddour", new DateTime(1998, 3, 1)),
            new Person("noman", "jessri", new DateTime(1995, 10, 10)),
            new Person("bassel", "mariam", new DateTime(2001, 1, 1)),
            new Person("mohammad", "mansour", new DateTime(1996, 5, 18)),
        };

        [WebMethod]
        public List<Person> AddPerson(string firstName, string lastName, DateTime dateOfBirth)
        {
            Person person = new Person(firstName, lastName, dateOfBirth);
            list.Add(person);
            return list;
        }

        [WebMethod]
        public List<Person> GetList()
        {
            return list;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }

    public class Person
    {
        public int Id { get; private set; }
        public int Age
        {
            // `set` is here because WSDL doesn't contain this property otherwise
            private set
            {
                throw new Exception();
            }
            get
            {
                TimeSpan duration = DateTime.Now - DateOfBirth;
                return (duration.Days + 364) / 365;
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }


        private static int IdSequence = 1;
        private static Object IdSequenceLock = new Object();

        public Person(string firstName, string lastName, DateTime dateOfBirth)
        {
            lock(IdSequenceLock)
            {
                Id = IdSequence;
                IdSequence++;
            }

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        // necessary for serilization for the WSDL
        public Person()
        {

        }
    }
}
