using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace dotNetAssignmentShape
{

    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Circle))]
   public class Shape
    {
        public virtual string  Colour { get; set; }
        public virtual double Area { get; set; }
    }
   public interface IShape
    {
        string Colour { get; set; }
        double Area { get; }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override string Colour { get; set; }
        public override double Area => Width * Height;

    }
    public class Circle : Shape
    {
        public double Radius { get; set; }
        public override string Colour { get; set; }
        public override double Area => Math.PI * Math.Pow(Radius, 2);

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Loading shapes from XML:");

            var listOfShapes = new List<Shape>
            {
            new Circle { Colour = "Red", Radius = 2.5 },
            new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
            new Circle { Colour = "Green", Radius = 8 },
            new Circle { Colour = "Purple", Radius = 12.3 },
            new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
            };

            string xmlFile = "shapes.xml";
            ToXmlFile(xmlFile, listOfShapes);

            List<Shape> loadedShapesXml =
                 FromXmlFile<List<Shape>>(xmlFile);

            foreach (Shape item in loadedShapesXml)
            {
                Console.WriteLine($"{item.GetType().Name} is {item.Colour} and has an area of { item.Area} ");
            }




        }
        public static S FromXmlFile<S>(string file)
        {
            XmlSerializer xmls = new XmlSerializer(typeof(S));
            var xmlContent = File.ReadAllText(file);
            using (StringReader sr = new StringReader(xmlContent))
            {
                return (S)xmls.Deserialize(sr);
            }
        }
        public static void ToXmlFile<S>(string file, S obj)
        {
            using (StringWriter sw = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(S));
                xmls.Serialize(sw, obj);
                File.WriteAllText(file, sw.ToString());
            }
        }
       
    }
}