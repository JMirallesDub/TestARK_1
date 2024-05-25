namespace TestARK_1
{
    public abstract class Animal
    {
        public bool IsVaccinated { get; set; }
    }

    public class Cat : Animal { }
    public class Dog : Animal { }

    public class Veterinary
    {
        public List<Animal> WaitingRoom { get; } = new List<Animal>();
        public List<Animal> RecoveryRoom { get; } = new List<Animal>();

        public List<Animal> Vaccinate()
        {
            const int MaxOccupation = 2;
            List<Animal> vaccinated = new List<Animal>();

            Animal animal = null;

            var pending = WaitingRoom.Where(x => x.IsVaccinated == false).ToList();
            while (vaccinated.Count < MaxOccupation)
            {
                if (pending.Count == 0)
                {
                    if (vaccinated.Count == 0)
                    {
                        return vaccinated;
                    }
                    break;
                }

                animal = vaccinated.Count % 2 == 0 ? pending.OfType<Dog>().FirstOrDefault() : pending.OfType<Cat>().FirstOrDefault();
                animal ??= pending.OfType<Cat>().FirstOrDefault();

                animal.IsVaccinated = true;
                pending.Remove(animal);
                vaccinated.Add(animal);
            }
            animal = (Animal)vaccinated.OfType<Dog>().FirstOrDefault() ?? vaccinated.OfType<Cat>().FirstOrDefault();
            WaitingRoom.Remove(animal);
            RecoveryRoom.Add(animal);

            return vaccinated;
        }
    }

    class Program
    {
        static void Main()
        {
            var veterinary = new Veterinary { };

            veterinary.WaitingRoom.AddRange(new Animal[] { new Cat(), new Cat(), new Cat(), new Dog(), new Dog(), new Dog() });

            while (true)
            {
                var vaccinated = veterinary.Vaccinate();
                if (vaccinated.Count == 0) break;
                Console.WriteLine("Waiting room: " + veterinary.WaitingRoom.Count + " Dogs: " + veterinary.WaitingRoom.OfType<Dog>().Count() + " Cats: " + veterinary.WaitingRoom.OfType<Cat>().Count());

                Console.WriteLine("Recovery room: " + veterinary.RecoveryRoom.Count + " Dogs: " + veterinary.RecoveryRoom.OfType<Dog>().Count() + " Cats: " + veterinary.RecoveryRoom.OfType<Cat>().Count());

                foreach (var vacc in vaccinated)
                {
                    Console.WriteLine(vacc);
                }
            }

        }
    }
}
