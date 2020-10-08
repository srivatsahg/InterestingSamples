using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FactoryWithoutSwitch
{
    class FactorySample
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Factory without switch case");

            var factory = new CarFactory();
            factory.RegisterCar("Mercedes", () => new Mercedes { Model = "W123" });
            factory.RegisterCar("Kia", () => new Kia { Model = "Seltos" });
            factory.RegisterCar("BMW", () => new BMW { Model = "i8" });

            //TEST FOR MERC, 
            Console.WriteLine($"Car type is : {factory["Kia"].GetType().ToString()}");

            Console.ReadLine();
        }
    }

    #region Model
    public abstract class Car
    {
        public string Model { get; set; } = "Unknown";
    }

    public class Mercedes : Car { };
    public class Kia : Car { };
    public class BMW : Car { };

    #endregion

    #region Factory
    public class CarFactory
    {
        public readonly Dictionary<string, Func<Car>> cars;

        public CarFactory()
        {
            cars = new Dictionary<string, Func<Car>>();
        }

        public Car this[string carType] => CreateCar(carType);

        public Car CreateCar(string carType) => cars[carType]();

        public string[] RegisteredCarTypes => cars.Keys.ToArray();

        public void RegisterCar(string carType, Func<Car> factoryMethod)
        {
            if (string.IsNullOrEmpty(carType)) return;
            if (factoryMethod is null) return;

            cars[carType] = factoryMethod;
        }
    }

    #endregion
}
