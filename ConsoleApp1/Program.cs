// See https://aka.ms/new-console-template for more informatioC
using System;
using System.Reflection;

abstract class Smartphone{
    protected string Brand;
    
    
    protected Smartphone(string brand){
        Brand = brand;
    }

    private Smartphone(){}

    public abstract void PrintInfo();
}

class SmartphoneModel : Smartphone
{
    public string Model{
        get;
        
    }
    public int BatteryPower{
        get;
    }

    public SmartphoneModel(string brand, string model, int batteryPower) : base(brand){
        Model = model;
        BatteryPower = batteryPower;
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"Телефон марки: {Brand}, модели: {Model}");
    }

    private void ShowBattery(){
        Console.WriteLine($"Мощность аккумулятора: {BatteryPower}");
    }
    public void CheckBattery(){
        ShowBattery();
    }

    public static void ShowModel(SmartphoneModel phone){
        Console.WriteLine($"Модель телефона: {phone.Model}");
    }
}

class Program{
    static void Main(){
        
        //Test
        Console.WriteLine("Test");
        SmartphoneModel phone1 = new SmartphoneModel("Iphone", "12", 100);
        phone1.PrintInfo();
        phone1.CheckBattery();
        SmartphoneModel.ShowModel(phone1);
        
        Console.WriteLine(" ");
        
        
        //task1(a)
        Console.WriteLine("task1(a)");
        Type type0 = typeof(SmartphoneModel);
        Console.WriteLine("Члены производного класса: ");
        MemberInfo[] members = type0.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        foreach (var member in members)
        {
            Console.WriteLine($"{member.MemberType} - {member.Name}");
        }
        
        Console.WriteLine(" ");
        
        //Task1(b)
        Type[] type1 = { typeof(Smartphone), typeof(Smartphone) };
        foreach (Type type in type1)
        {
            GenerateHtmlDocumentation(type);
        }
        
        Type[] type2 = { typeof(SmartphoneModel), typeof(SmartphoneModel) };
        foreach (Type type in type2)
        {
            GenerateHtmlDocumentation(type);
        }
        
        
        
       //task2(a) 
        Type type3 = typeof(SmartphoneModel);
        Console.WriteLine("Конструкторы класса SmartphoneModel:");
        ConstructorInfo[] constructors = type3.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        
        foreach (var constructor in constructors)
        {
            Console.Write("Модификатор доступа: ");
            Console.WriteLine(constructor.IsPublic ? "public" : constructor.IsPrivate ? "private" : "protected");
            
            Console.Write("Параметры: ");
            ParameterInfo[] parameters = constructor.GetParameters();
            foreach (var param in parameters)
            {
                Console.Write($"{param.ParameterType.Name} {param.Name}, ");
            }
            Console.WriteLine();
        }
        
        object smartphoneInstance = Activator.CreateInstance(type3, "Iphone", "15", 100);
        
        
        
        
        Console.WriteLine(" ");
        Console.WriteLine("task2(b)");
        //task2(b)
        Console.WriteLine("Методы класса SmartphoneModel:");
        MethodInfo[] methods = type3.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        foreach (var method in methods)
        {
            Console.WriteLine(method.Name);
        }

        Console.WriteLine(" ");
        Console.WriteLine("Запуск приватного метода:");
        MethodInfo privateMethod = type3.GetMethod("ShowBattery", BindingFlags.NonPublic | BindingFlags.Instance);
        if (privateMethod != null)
        {   
            privateMethod.Invoke(smartphoneInstance, null);
        }
    }
    
    
    
    
    
    static void GenerateHtmlDocumentation(Type type)
    {
        string fileName = type.Name + ".html";
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine("<html><head><title>Документация: " + type.Name + "</title></head><body>");
            writer.WriteLine("<h1>Класс: " + type.Name + "</h1>");
            writer.WriteLine("<hr>");
            
            writer.WriteLine("<h2>Публичные поля</h2>");
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                writer.WriteLine("<p>" + field.Name + " : " + field.FieldType.Name + "</p>");
            
            writer.WriteLine("<h2>Публичные свойства</h2>");
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                writer.WriteLine("<p>" + prop.Name + " : " + prop.PropertyType.Name + "</p>");
            
            writer.WriteLine("<h2>Публичные методы</h2>");
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                writer.WriteLine("<p>" + method.Name + "()</p>");
            
            writer.WriteLine("<h2>Унаследованные методы</h2>");
            foreach (var method in type.BaseType?.GetMethods(BindingFlags.Public | BindingFlags.Instance) ?? Array.Empty<MethodInfo>())
                writer.WriteLine("<p>" + method.Name + "()</p>");
            
            writer.WriteLine("<h2>Статические методы</h2>");
            foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public))
                writer.WriteLine("<p>" + method.Name + "()</p>");
            
            writer.WriteLine("<h2>Приватные и защищенные элементы</h2>");
            foreach (var member in type.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                writer.WriteLine("<p>" + member.MemberType + " - " + member.Name + "</p>");
            
            writer.WriteLine("</body></html>");
        }
    }
}
