using System;
using System.Collections.Generic;

/// <summary>
/// Her bir noktanın 2 boyutlu koordinat düzlemindeki konumunu tutan sınıftır.
/// </summary>
public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X:F2}, {Y:F2})";
}

/// <summary>
/// 2 boyutlu düzlemdeki poligonun köşelerini oluşturan noktaları ve afin dönüşümünü aşamarıyla yapan fonksiyonları tutan sınıf
/// </summary>
public class Polygon
{
    public List<Point> Points { get; private set; }

    public Polygon(List<Point> points)
    {
        Points = points;
    }

    // Ölçekleme(Scale) işlemi
    public Polygon Scale(double scaleX, double scaleY)
    {
        var scaledPoints = new List<Point>();
        foreach (var point in Points)
        {
            scaledPoints.Add(new Point(point.X * scaleX, point.Y * scaleY));    // Poligonun köşe noktaları x ve y ölçek sabitleriyle çarpılarak ölçeklenmiş poligon elde edilir.
        }
        return new Polygon(scaledPoints);
    }

    // Döndürme (Rotate) işlemi
    public Polygon Rotate(double angleInDegrees)
    {
        // Derece cinsinden gelen açı, formülüyle birlikte radyan(yarıçap uzunluğunu göre açı) cinsine çevrilir. 
        double angleInRadians = angleInDegrees * Math.PI / 180;

        var rotatedPoints = new List<Point>();

        foreach (var point in Points)
        {
            // Her bir nokta için Rotate formülü uygulanarak döndürme sonucundaki yeni poligonun köşe noktaları elde edilir.
            double xNew = point.X * Math.Cos(angleInRadians) - point.Y * Math.Sin(angleInRadians);
            double yNew = point.X * Math.Sin(angleInRadians) + point.Y * Math.Cos(angleInRadians);
            rotatedPoints.Add(new Point(xNew, yNew));
        }
        return new Polygon(rotatedPoints);
    }

    // Öteleme (Translate) işlemi
    public Polygon Translate(double translateX, double translateY)
    {
        var translatedPoints = new List<Point>();
        foreach (var point in Points)
        {
            // Her bir köşe noktası verilen öteleme miktarı kadar (toplanarak)kaydırılır. Esasında basit bir koordinat dönüşümü yapılmış olur.
            translatedPoints.Add(new Point(point.X + translateX, point.Y + translateY));
        }
        return new Polygon(translatedPoints);
    }

    public void PrintCoordinates()
    {
        foreach (var point in Points)
        {
            Console.WriteLine(point);
        }
    }
}

public class Program
{
    public static void Main()
    {
        var points = new List<Point> { new Point(1, 1), new Point(3, 1), new Point(2, 4) };
        var polygon = new Polygon(points);

        Console.WriteLine("Orjinal Poligon:");
        polygon.PrintCoordinates();

        Console.WriteLine("\nÖlçekleme(Scale) (ScaleX: 2, ScaleY: 1.5) uygulanmış poligon:");
        var scaledPolygon = polygon.Scale(2, 1.5);
        scaledPolygon.PrintCoordinates();

        Console.WriteLine("\nDöndürme(Rotate) (45 derece) uygulanmış polygon:");
        var rotatedPolygon = polygon.Rotate(45);
        rotatedPolygon.PrintCoordinates();

        Console.WriteLine("\nÖteleme (Translate) (TranslateX: 1, TranslateY: -2) uygulanmış poligon:");
        var translatedPolygon = polygon.Translate(1, -2);
        translatedPolygon.PrintCoordinates();
    }
}