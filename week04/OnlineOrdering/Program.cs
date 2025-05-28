using System;

class Program
{
    static void Main()
    {
        // First Order (USA Customer)
        Address addr1 = new Address("123 Apple St", "New York", "NY", "USA");
        Customer cust1 = new Customer("Alice Johnson", addr1);

        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Notebook", "NB001", 4.99, 3));
        order1.AddProduct(new Product("Pen Pack", "PN101", 2.49, 2));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}\n");

        // Second Order (International Customer)
        Address addr2 = new Address("45 Rue de Paris", "Paris", "Ile-de-France", "France");
        Customer cust2 = new Customer("Jean Dupont", addr2);

        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Sketchbook", "SK555", 9.99, 1));
        order2.AddProduct(new Product("Colored Pencils", "CP333", 5.49, 2));
        order2.AddProduct(new Product("Eraser", "ER007", 0.99, 5));

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}");
    }
}
