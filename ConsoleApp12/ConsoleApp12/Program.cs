using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Store
    {
        private string Name;
        private string Address;
        private string ProfileDescription;
        private string ContactPhone;
        private string ContactEmail;
        public void SetName(string name)
        {
            this.Name = name;
        }
        public void SetAddress(string address)
        {
            this.Address = address;
        }
        public void SetProfileDescription(string ProfileDescription)
        {
            this.ProfileDescription = ProfileDescription;
        }
        public void SetContactPhone(string ContactPhone)
        {
            this.ContactPhone = ContactPhone;
        }
        public void SetContactEmail(string ContactEmail)
        {
            this.ContactEmail = ContactEmail;
        }

        public string GetName()
        {
            return Name;
        }
        public string GetAddress()
        {
            return Address;
        }
        public string GetProfileDescription()
        {
            return ProfileDescription;
        }
        public string GetContactPhone()
        {
            return ContactPhone;
        }
        public string GetContactEmail()
        {
            return ContactEmail;
        }
        public void InputData()
        {
         string name;
         string address;
         string profileDescription;
         string contactPhone;
         string contactEmail;
        Console.WriteLine("Введите Название");
          name = Console.ReadLine();
            SetName(name);
            Console.WriteLine("Введите Адрес");
            address = Console.ReadLine();
            SetAddress(address);
            Console.WriteLine("Введите Описание профеля магазина");
            profileDescription = Console.ReadLine();
            SetProfileDescription(profileDescription);
            Console.WriteLine("Введите Телефон");
            contactPhone = Console.ReadLine();
            SetContactPhone(contactPhone);
            Console.WriteLine("Введите email");
            contactEmail = Console.ReadLine();
            SetContactEmail(contactEmail);
        }
        public void DisplayData()
        {
            Console.WriteLine($"Название: {GetName()}");
            Console.WriteLine($"Адрес: {GetAddress()}");
            Console.WriteLine($"Описание магазина: {GetProfileDescription()}");
            Console.WriteLine($"Телефон: {GetContactPhone()}");
            Console.WriteLine($"Email: {GetContactEmail()}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Store m0 = new Store();
            m0.InputData();
            m0.DisplayData();
        }
    }
}
