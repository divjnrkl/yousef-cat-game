// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;


namespace My_Prorgram
{
class Product
{
    public int ID { get; set; }

    public string number { get; set; } 
    public string name { get; set; }
    public double price { get; set; }
    public double quantity { get; set; }
    

    public Product(int _ID, string _number, string _name, double _price, double _quantity)
    {
        ID = _ID;
        number = _number;
        name = _name;
        price = _price;
        quantity = _quantity;
    }

    public string Details()
    {
        return $"ID: {ID}, Name: {name}, Number = {number}, Price: {price}, Quantity = {quantity}";
    }

    public void Update(int new_price)
    {
        price = new_price;
        Console.WriteLine("Product is updated");
    }   
}

class Stock
{
    List<Product> products = new List<Product>();
    public int Count => products.Count;

    public int ID { get; set; }


    public void Add_Product(int Id)
    {
        ID = Id;
    }

    public void Update_Product(int ID, double new_price)
    {
        var product = products.FirstOrDefault(p => p.ID == ID);
        if (product != null)
        {
            product.price = new_price;
            Console.WriteLine("Product is updated successfully");
        }
    }

    public void Delete_Product(int id)
    {   
        var product = products.FirstOrDefault(p => p.ID == id);
        if(product != null)
        {
            products.Remove(product);
        }

        Console.WriteLine("Product is deleted");
    }

    public double Search_Product(int ID)
    {
        var product = products.FirstOrDefault(p => p.ID == ID);
        return product != null ? product.price : 0;
    }    
}



class Customer
{   
    public List<Customer> customers = new List<Customer>();
    public int ID{ get; set; }
    public string phone { get; set; }
    
    public Customer(){} 
    public Customer(int _ID, string _phone)
    {
        ID = _ID;
        phone = _phone;
    }

    public void Add_Customer(int id, string _phone)
    {
        ID = id;
        phone = _phone;
    }

    public void Update_Customer(int ID, string new_phone)
    {
        var customer = customers.FirstOrDefault(p => p.ID == ID);
        if (customer != null)
        {
            customer.phone = new_phone;
        }
    }

    public void Delete_Customer(int id)
    {   
        var customer = customers.FirstOrDefault(p => p.ID == id);
        if(customer != null)
        {
            customers.Remove(customer);
        }
    }

    public void print_customers()
    {   
        if(customers.Count == 0)
        {
            Console.WriteLine("No customers available yet");
        }
        //Console.WriteLine("List of all customers");
        foreach(var customer in customers)
        {
            Console.WriteLine(customer);
        }
    }

    public int Search_Customers(int ID)
    {
        var customer = customers.FirstOrDefault(c => c.ID == ID);
        return customer != null ? customer.ID : 0;
    }    



    
    public string Details()
    {
        return $"ID: {ID}, Phone: {phone}";
    }

}

class OrderItem
{
    public double sales_price { get; set; }
    public Product product { get; set; }
    

    public OrderItem(double _sales_price, Product _product)
    {
       sales_price = _sales_price;
       product = _product;
    }

    public void Update(double new_quantity)
    {
        product.quantity = new_quantity;
        Console.WriteLine("Item is updated");
    }

    public string Details()
    {
        return $"Sales Price = {sales_price}, Quantity: {product.quantity}";
    }
}



class Order
{   
    public int ID { get; set; }
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; }

    public double total_order_amount { get; set; }
    
   

    public Order(double _total_order_amount)
    {
        OrderNumber = new Random().Next(1, 100000);
        OrderDate = DateTime.Now;
        Items = new List<OrderItem>();
        total_order_amount = _total_order_amount;
    }

    public void Add_Item(OrderItem item)
    {
        Items.Add(item);
    }


    public string Details()
    {
        return $"ID = {ID}, Order Number: {OrderNumber}, Date Time: {OrderDate}";
    }

    public void print_items()
    {   
        Console.WriteLine("List of all items : ");
        foreach(var items in Items)
        {
            Console.WriteLine(items);
        }
    }
}


abstract class Payment
{
    public DateTime Payment_Date { get; set; }
    public double Amount { get; set; }
    public abstract void Process_Payment();
}

class Cash_Payment : Payment
{
    public override void Process_Payment()
    {
        Console.WriteLine($"Processing cash payment of {Amount}");
    }
}

class Credit_Payment : Payment
{
    public string Card_Number { get; set; } = string.Empty;
    public override void Process_Payment()
    {
        Console.WriteLine($"Processing credit payment of {Amount} from card {Card_Number}");
    }
}


class Check_Payment : Payment
{
    public string Check_Number { get; set; } = string.Empty;
    public override void Process_Payment()
    {
        Console.WriteLine($"Processing check payment of {Amount} with check {Check_Number}");
    }
}


class Transaction
{
    public Order Order { get; set; }
    public Payment Payment { get; set; }

    public Transaction(Order order, Payment payment)
    {
        Order = order;
        Payment = payment;
    }

    public string Details()
    {
        return $"Order {Order.OrderNumber}, Payment: {Payment.Amount}";
    }

}


class My_Program
{   
    static void Main(string[] args)
    {   

        Console.Title = "messi";
        Console.ForegroundColor = ConsoleColor.DarkCyan;

        Stock stock = new Stock();
        Customer customer = new Customer();
        List<Transaction> transactions = new List<Transaction>();

        while (true)
        {
            Console.WriteLine("1. Data Entry");
            Console.WriteLine("2. Sales Process");
            Console.WriteLine("3. Print");
            Console.WriteLine("4. Exit");
            Console.Write("\nChoose an option: ");


            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();


            if (choice == 4) 
            {
                break;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("1. Add New/Update/Delete Customer");
                    Console.WriteLine("2. Add New/Update/Delete Product in Stock");
                    Console.Write("\nChoose an option: ");


                    int Sub_Choice = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    if (Sub_Choice == 1)
                    {
                        Manage_Customer(customer);
                    }
                    else if (Sub_Choice == 2)
                    {
                        Manage_Stock(stock);
                    }
                    break;
                case 2:
                        Console.WriteLine("1. Add Transaction");
                        Console.WriteLine("2. Update Order");
                        Console.WriteLine("3. Pay Order");
                        Console.Write("\nChoose an option: ");

                        int Sales_Choice = Convert.ToInt32(Console.ReadLine());

                        Console.Clear();

                        if(Sales_Choice == 1)
                        {
                            Console.WriteLine("Transaction is added");
                        }

                        else if(Sales_Choice == 2)
                        {
                            Console.WriteLine("Order is updated");
                        }

                        else if(Sales_Choice == 3)
                        {
                            Console.WriteLine("Order payed");
                        }
                     break;

                case 3:
                        Console.WriteLine("1. Print Customers");
                        Console.WriteLine("2. Print Transactions");
                        Console.Write("\nChoose an option: ");
                        int Choice = Convert.ToInt32(Console.ReadLine());

                        Console.Clear();

                        if (Choice == 1) 
                        {
                            customer.print_customers();
                        }
                        else 
                        {
                            Print_Transactions(transactions);
                        }

                        break;                

                default:
                {
                    Console.WriteLine("Invalid Option");
                    break;  
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
    }

        static void Manage_Stock(Stock stock)
        {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Edit Product");
            Console.WriteLine("3. Delete Product");
            Console.Write("\nChoose an option: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            
            switch(choice)
            {
                case 1: //Add Product
                     Console.Write("Enter ID : ");
                     int ID01 = Convert.ToInt32(Console.ReadLine());
                     
                     stock.Add_Product(ID01);
                     Console.WriteLine("\nProduct is added successfully");
                     break;

                case 2: // Update Product
                     Console.Write("Enter ID : ");
                     int ID02 = Convert.ToInt32(Console.ReadLine());
                     
                     Console.Write("Enter price : ");
                     double price = Convert.ToInt32(Console.ReadLine());

                     stock.Update_Product(ID02, price);
                     Console.WriteLine("\nProduct's price is updated successfully");

                     break;

                     
                case 3: //Delete Product
                    Console.Write("Enter ID : ");
                    int ID03 = Convert.ToInt32(Console.ReadLine());

                    stock.Delete_Product(ID03);
                    Console.WriteLine("\nProduct is deleted from the system");
                    break;

                    default:
                            Console.WriteLine("Invalid choice");
                            break;

                
            }            

        }

        static void Manage_Customer(Customer customer)
        {
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. Edit Customer");
            Console.WriteLine("3. Delete Customer");
            Console.Write("\nChoose an option: ");
        
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            
            switch(choice)
            {
                case 1: //Add customer
                     
                     Console.Write("Enter ID : ");
                     int ID01 = Convert.ToInt32(Console.ReadLine());
                     
                     Console.Write("Enter phone : ");
                     string? phone01 = Console.ReadLine();

                     if(phone01 != null)
                     {
                        customer.Add_Customer(ID01, phone01);
                     }
                     

                     Console.WriteLine("\nCustomer is added successfully");
                     break;

                case 2: // Update customer
                     Console.Write("Enter ID : ");
                     int ID02 = Convert.ToInt32(Console.ReadLine());
                     
                     Console.Write("Enter phone : ");
                     string? phone02 = Console.ReadLine();

                     customer.Update_Customer(ID02, phone02);
                     Console.WriteLine("\nCustomer's phone is updated successfully");

                     break;


                case 3: //Delete customer
                    Console.Write("Enter ID : ");
                    int ID03 = Convert.ToInt32(Console.ReadLine());

                    customer.Delete_Customer(ID03);
                    Console.WriteLine("\nCustomer is deleted from the system");
                    break;

                    default:
                            Console.WriteLine("Invalid choice");
                            break;
                
            }
        }

        static void Print_Transactions(List<Transaction> transactions)
        {
        if (transactions.Count > 0)
        {   
            Console.WriteLine("List of all transactions : ");
            foreach(Transaction transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
        else
        {
            Console.WriteLine("\nNo transactions available yet");
        }
        }

        static void Print_Customer(List<Customer> customers)
        {
        if (customers.Count > 0)
        {   
            Console.WriteLine("List of all transactions : ");
            foreach(Customer customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
        else
        {
            Console.WriteLine("No transactions available yet");
        }
        }
           
}
}
