using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TestXml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Укажите полный путь к месту нахождения xml-файла. Пример: C:\owner.xml");
            var path = Console.ReadLine();
            var cultureInfo = new CultureInfo("ru-RU");
            Owner owner = new Owner();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                if (childnode.Name == "OwnerLastName")
                    owner.OwnerLastName = childnode.InnerText;

                if (childnode.Name == "OwnerFirstName")
                    owner.OwnerFirstName = childnode.InnerText;

                if (childnode.Name == "OwnerMiddleName")
                    owner.OwnerMiddleName = childnode.InnerText;

                if (childnode.Name == "OwnerBirthDate")
                    owner.OwnerBirthDate = DateTime.ParseExact(childnode.InnerText, "yyyy-MM-dd", cultureInfo);
            }

            Console.WriteLine($"Полученные данные из Xml-файла: {owner}");

            var advice = "Нажмите F1 для изменения фамилии.\n" +
                "Нажмите F2 для изменения имени.\n" +
                "Нажмите F3 для изменения отчества.\n" +
                "Нажмите F4 для изменения даты рождения.\n" +
                "Нажмите F5 для выхода.";
            Console.WriteLine(advice);
            var key = Console.ReadKey();
            Console.WriteLine();
            while (key.Key != ConsoleKey.F5)
            {
                switch (key.Key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine($"Текущая фамилия - {owner.OwnerLastName}");
                        owner.OwnerLastName = Console.ReadLine();
                        break;
                    case ConsoleKey.F2:
                        Console.WriteLine($"Текущее имя - {owner.OwnerFirstName}");
                        owner.OwnerFirstName = Console.ReadLine();
                        break;
                    case ConsoleKey.F3:
                        Console.WriteLine($"Текущее отчество - {owner.OwnerMiddleName}");
                        owner.OwnerMiddleName = Console.ReadLine();
                        break;
                    case ConsoleKey.F4:
                        Console.WriteLine($"Текущая дата рождения - {owner.OwnerBirthDate}. Пример: 01.01.2001");
                        owner.OwnerBirthDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", cultureInfo);
                        break;
                }
                Console.WriteLine(advice);
                key = Console.ReadKey();    
            }

            XDocument newXDoc = new XDocument();

            XElement ownerXml = new XElement("Owner");
            XElement ownerLastNameXml = new XElement("OwnerLastName", owner.OwnerLastName);
            XElement ownerFirstNameXml = new XElement("OwnerFirstName", owner.OwnerFirstName);
            XElement ownerMiddleNameXml = new XElement("OwnerMiddleName", owner.OwnerMiddleName);
            XElement ownerBirthDateXml = new XElement("OwnerBirthDate", owner.OwnerBirthDate);

            ownerXml.Add(ownerLastNameXml);
            ownerXml.Add(ownerFirstNameXml);
            ownerXml.Add(ownerMiddleNameXml);
            ownerXml.Add(ownerBirthDateXml);

            newXDoc.Add(ownerXml);

            newXDoc.Save(path);

            Console.WriteLine($"Данные сохранены в {path}.");
        }
    }
}
