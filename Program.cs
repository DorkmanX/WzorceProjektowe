using System;
using static Program;

public class Program
{
    public abstract class Component
    {
        public abstract void Print();
    }

    public class Decorator : Component
    {
        protected Component _component;
        public override void Print()
        {
            _component.Print();
        }
        public Decorator(Component component) 
        {
            this._component = component;
        }
    }
    public class ConcreteDecoratorA : Decorator
    {
        private string _info = string.Empty;
        public ConcreteDecoratorA(Component decorator,string info) : base(decorator) 
        {
            _info = info;
        }

        public Component GetDecoratedObject()
        { 
            return this; 
        }
        public override void Print()
        {
            Console.WriteLine("------------------------------------------------");
            _component.Print();
            Console.WriteLine("Zamowienie dodatkowe: "+_info);
            Console.WriteLine("------------------------------------------------");
        }
    }
    public class Dish : Component
    {
        public string FirstIngriedient { get; set; } = string.Empty;
        public string SecondIngriedient { get; set; } = string.Empty;
        public string ThirdIngriedient { get; set; } = string.Empty;

        public Dish(string partA, string partB, string partC)
        {
            FirstIngriedient = partA;
            SecondIngriedient = partB;
            ThirdIngriedient = partC;
        }
        public Dish() { }
        public override void Print()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Ingriedient 1: " + this.FirstIngriedient);
            Console.WriteLine("Ingriedient 2: " + this.SecondIngriedient);
            Console.WriteLine("Ingriedient 3: " + this.ThirdIngriedient);
            Console.WriteLine("------------------------------------------------");
        }
    }

    public abstract class Kitchen
    {
        public abstract void CreateBreakfast();
        public abstract void CreateLunch();
        public abstract void CreateDinner();
        public abstract Dish GetDish();
        public Kitchen() { }
    }

    public class PolishKitchen : Kitchen
    {
        private Dish _dish;
        private Random rnd = new Random();

        private List<string> polish_kitchen_dishes = new List<string>()
        {
            "Jajko","Kielbasa","Ogorki","Kotlet schabowy","Ziemniaki",
            "Surowka","Ser","Szynka","Pomidory"
        };
        public override void CreateBreakfast() 
        {
            _dish.FirstIngriedient = polish_kitchen_dishes[rnd.Next(0, 3)] + " " + polish_kitchen_dishes[rnd.Next(0, 3)] + " " + polish_kitchen_dishes[rnd.Next(0, 3)];
        }
        public override void CreateLunch()
        {
            _dish.SecondIngriedient = polish_kitchen_dishes[rnd.Next(3, 6)] + " " + polish_kitchen_dishes[rnd.Next(3, 6)] + " " + polish_kitchen_dishes[rnd.Next(3, 6)];
        }
        public override void CreateDinner()
        {
            _dish.ThirdIngriedient = polish_kitchen_dishes[rnd.Next(6, 9)] + " " + polish_kitchen_dishes[rnd.Next(6, 9)] + " " + polish_kitchen_dishes[rnd.Next(6, 9)];
        }
        public override Dish GetDish() 
        {
            return _dish;
        }
        public PolishKitchen() { _dish = new Dish(); }
    }

    public class UkrainianKitchen : Kitchen
    {
        private Dish _dish;
        private Random rnd = new Random();

        private List<string> ukrainian_kitchen_list = new List<string>()
        {
            "barszcz ukraiński","kapuśniak","pielmieni","słonina wieprzowa",
            "sękacz","kutia","chleb korowaj","pierogi ukrainskie","ogorki kiszone"
        };
        public override void CreateBreakfast()
        {
            _dish.FirstIngriedient = ukrainian_kitchen_list[rnd.Next(0, 3)] + " " + ukrainian_kitchen_list[rnd.Next(0, 3)] + " " + ukrainian_kitchen_list[rnd.Next(0, 3)];
        }
        public override void CreateLunch()
        {
            _dish.SecondIngriedient = ukrainian_kitchen_list[rnd.Next(3, 6)] + " " + ukrainian_kitchen_list[rnd.Next(3, 6)] + " " + ukrainian_kitchen_list[rnd.Next(3, 6)];
        }
        public override void CreateDinner()
        {
            _dish.ThirdIngriedient = ukrainian_kitchen_list[rnd.Next(6, 9)] + " " + ukrainian_kitchen_list[rnd.Next(6, 9)] + " " + ukrainian_kitchen_list[rnd.Next(6, 9)];
        }
        public override Dish GetDish()
        {
            return _dish;
        }
        public UkrainianKitchen() { _dish = new Dish(); }
    }

    public class Director
    {
        private Kitchen _kitchen;
        private Random rnd = new Random();
        public Director(Kitchen kitchen)
        {
            _kitchen = kitchen;
        }
        public Dish Construct()
        {
            _kitchen.CreateLunch();
            _kitchen.CreateBreakfast();
            _kitchen.CreateDinner();
            return _kitchen.GetDish();
        }
        
    }
    public class Orders
    {
        private List<Component> _orders_list;
        public Orders() 
        {
            _orders_list = new List<Component>();
        }
        public void AddOrder(Component component) 
        {
            _orders_list.Add(component);
        }
        public void AddExtraOrder(int orderId)
        {
            Component decorated_component = new ConcreteDecoratorA(_orders_list[orderId], "Srebrna zastawa");
            _orders_list[orderId] = decorated_component;
        }
        public void Print()
        {
            foreach (Component component in _orders_list) { component.Print(); }
        }
    }
    public static void Main()
    {
        Orders orders = new Orders();
        Director director = new Director(new PolishKitchen());
        Console.WriteLine("Added one order. List of orders:");
        orders.AddOrder(director.Construct());

        orders.Print();

        director = new Director(new UkrainianKitchen());
        Console.WriteLine("Added second order. List of orders:");
        orders.AddOrder(director.Construct());

        orders.Print();

        orders.AddExtraOrder(0);
        Console.WriteLine("Added additional order to existing order. List of orders:");
        orders.Print();


    }
}
