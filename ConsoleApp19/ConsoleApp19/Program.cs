using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    public abstract class Vehicle
    {
        public int NumberOfWheels { get; set; }
        public abstract void Move();

        
    }
    public interface IWaterVehicle
    {
        void Water();
    }
    public interface IAirVehicle
    {
        void Fly();
    }
    public class Car : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine("Я еду по дороге!");
        }
    }
    public class Boat : Vehicle, IWaterVehicle
    {
        public void Water()
        {
            Console.WriteLine("Я плыву");
        }
        public override void Move()
        {
            Console.WriteLine("По воде");
        }
    }
    public class Plane : Vehicle, IAirVehicle
    {

        public void Fly()
        {
            Console.WriteLine("Я лечу");
        }
        public override void Move()
        {
            Console.WriteLine("По небу");
        }

    }
    public class TransportShowcase
    {
        public void DemonstrateTransport(List<Vehicle> veh)
        {
            foreach (var fruit in veh)
            {
                fruit.Move();
                fruit.Fly();
                fruit.Water();
            }
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Vehicle car = new Car();
            Vehicle boat = new Boat();
            Vehicle plane = new Plane();

            List<Vehicle> vehicles = new List<Vehicle> { car, boat, plane };

            var showcase = new TransportShowcase();
            showcase.DemonstrateTransport(vehicles);

        }
    }

}
