using System;
using System.Collections.Generic;
using System.Linq;

namespace sampleForExam
{
    class Program
    {
        static LinkedList<MenuItem>[] menuWhole = {
            new LinkedList<MenuItem>(new MenuItem[] {
                new MenuItem("Green Tea cheese cream", 105),
                new MenuItem("Matcha cheese cream", 115),
                new MenuItem("Black Tea cheese cream", 105),
                new MenuItem("Oolong Tea cheese cream", 105),
                new MenuItem("Cocoa cheese cream", 115),
                new MenuItem("Brown Sugar cheese cream", 105)
            }),

            new LinkedList<MenuItem>(new MenuItem[] {
                new MenuItem("Mango Yogurt", 105),
                new MenuItem("Lemon Yakult Yogurt", 90),
                new MenuItem("Blueberry Yogurt", 105),
                new MenuItem("Lemon Yogurt", 100),
                new MenuItem("Kumquat Yakult Yogurt", 90),
                new MenuItem("Lychee Yogurt", 105),
                new MenuItem("Kiwi Yogurt", 105),
            }),

            new LinkedList<MenuItem>(new MenuItem[] {
                new MenuItem("Bubble Black Tea milk tea", 85),
                new MenuItem("Red Beans Pudding milk tea", 95),
                new MenuItem("Oat and Red Beans milk tea", 95),
                new MenuItem("Wintermelon milk tea", 115),
                new MenuItem("Green Tea milk tea", 85),
                new MenuItem("Blueberry milk tea", 115),
                new MenuItem("Double milk tea", 95),
                new MenuItem("Caramel milk tea", 125),
                new MenuItem("Mango milk tea", 115),
                new MenuItem("Oreo milk tea", 115),
                new MenuItem("Sinkers Overload milk tea", 125),
                new MenuItem("Coffee milk tea", 145)
            }),

            new LinkedList<MenuItem>(new MenuItem[]
            {
                new MenuItem("Honey Peach juice", 105),
                new MenuItem("Lemon Pomelo juice", 85),
                new MenuItem("Passion and Kumquat juice", 105),
                new MenuItem("Pomelo Yakult juice", 115),
                new MenuItem("Blueberry Pomelo juice", 115),
                new MenuItem("Fresh Lemonade", 75),
                new MenuItem("Lemon Green Tea", 75),
                new MenuItem("Lemon Black Tea", 75),
                new MenuItem("Fresh Fruit Tea", 115),
                new MenuItem("Orange Mania Dynamite", 105),
                new MenuItem("Green Apple juice", 85),
                new MenuItem("Blueberry juice", 105),
                new MenuItem("Greentea Kiwi juice", 105),
                new MenuItem("Grape Fruit juice", 105)
            }),

            new LinkedList<MenuItem>(new MenuItem[]
            {
                new MenuItem("Coffee smoothie", 125),
                new MenuItem("Cranberry smoothie", 125),
                new MenuItem("Caramel smoothie", 125),
                new MenuItem("Matcha Red Beans smoothie", 125),
                new MenuItem("Chocolate smoothie", 125),
                new MenuItem("Mango smoothie", 125)
            }),

            new LinkedList<MenuItem>(new MenuItem[]
            {
                new MenuItem("Cocoa Pudding milk", 105),
                new MenuItem("Banana milk", 115),
                new MenuItem("Pudding Matcha milk", 105),
                new MenuItem("Taro milk", 115),
                new MenuItem("Brown Sugar Cocoa milk", 115)
            }),

            new LinkedList<MenuItem>(new MenuItem[]
            {
                new MenuItem("Cappuccino", 120),
                new MenuItem("Caffe Latte", 120),
                new MenuItem("Caffe Americano", 120),
                new MenuItem("Brewed Coffee", 120),
                new MenuItem("Mocha coffee", 120),
                new MenuItem("Iced Latte", 120),
                new MenuItem("Iced Cappuccino", 120)
            })
        };


        static Dictionary<string, Queue<string>> reservationQueue = new Dictionary<string, Queue<string>>();

        static void Main(string[] args)
        {
            string[] categoryNames = { "Cheese Cream", "Yogurt", "Milk Tea", "Juice", "Smoothies", "Milk Drinks", "Coffees" };
            string[] dateTime = { "April 23, 2024", "April 24, 2024", "April 25, 2024", "April 26, 2024" };
            string userDate;
            string ownerPassword = "password"; 

            Stack<List<CartItem>> cartStack = new Stack<List<CartItem>>();
            Stack<List<CartItem>> historyStack = new Stack<List<CartItem>>();

            List<CartItem> initialCart = new List<CartItem>();
            cartStack.Push(initialCart);

            List<CartItem> cart = new List<CartItem>();
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\tWELCOME TO HUALIEN!");
                Console.Write("\nPlease select option: \n[1] Order \n[2] Owner Options \n[3] Exit \nEnter: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        OrderMenu(dateTime, categoryNames, cartStack, historyStack);
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Enter owner password: ");
                        string password = Console.ReadLine();
                        if (password == ownerPassword)
                        {
                            OwnerOptions(dateTime);
                        }
                        else
                        {
                            Console.WriteLine("Invalid password!");
                            Console.ReadLine();
                        }
                        break;
                    case 3:
                        Console.WriteLine("\nThank you and please come again soon!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            } while (choice != 3);
        }

        static void OrderMenu(string[] dateTime, string[] categoryNames, Stack<List<CartItem>> cartStack, Stack<List<CartItem>> historyStack)
        {
            string userDate;
            int choice2;
            string userName;

            Console.Clear();
            do
            {
                Console.Clear();

                Console.Write("Please select an option: \n\n[1] Delivery \n[2] Pick-up \n[3] Dine-in \n[4] Exit \nEnter: ");
                choice2 = int.Parse(Console.ReadLine());

                switch (choice2)
                {
                    case 1:
                    case 2:
                        Console.Clear();
                        DisplayMenu(menuWhole, categoryNames);
                        AddToCart(menuWhole, categoryNames, cartStack, historyStack);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("--Available Dates--\n");

                        for (int i = 0; i < dateTime.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {dateTime[i]}");
                        }

                        Console.Write("\nPlease select date for reservation or type 'back' to go back \nEnter: ");
                        userDate = Console.ReadLine();

                        if (userDate.ToLower() == "back")
                        {
                            break;
                        }

                        if (int.TryParse(userDate, out int chosenDateIndex) && chosenDateIndex >= 1 && chosenDateIndex <= dateTime.Length)
                        {
                            Console.Write("\nEnter your name for reservation: ");
                            userName = Console.ReadLine();
                            ReserveTable(dateTime[chosenDateIndex - 1], userName);
                            Console.WriteLine($"Reservation successful! Your name '{userName}' is added to the queue for {dateTime[chosenDateIndex - 1]}.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.ReadLine();

            } while (choice2 != 4);
        }

        static void ReserveTable(string date, string userName)
        {
            if (!reservationQueue.ContainsKey(date))
            {
                reservationQueue[date] = new Queue<string>();
            }
            reservationQueue[date].Enqueue(userName);
        }

        static void OwnerOptions(string[] dateTime)
        {
            Console.WriteLine("-- Owner Options --");
            Console.WriteLine("Reservation Queue:");
            foreach (var date in dateTime)
            {
                if (reservationQueue.ContainsKey(date))
                {
                    Console.WriteLine($"\nDate: {date}");
                    var queue = reservationQueue[date];
                    foreach (var name in queue)
                    {
                        Console.WriteLine(name);
                    }
                }
            }
            Console.WriteLine("\nOptions:");
            Console.WriteLine("[1] Dequeue");
            Console.WriteLine("[2] Exit");
            Console.Write("Enter option: ");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    DequeueReservation();
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void DequeueReservation()
        {
            Console.WriteLine("\nEnter the date to dequeue from:");
            string date = Console.ReadLine();
            if (reservationQueue.ContainsKey(date))
            {
                var queue = reservationQueue[date];
                if (queue.Count > 0)
                {
                    string userName = queue.Dequeue();
                    Console.WriteLine($"Successfully dequeued '{userName}' from {date}.");
                }
                else
                {
                    Console.WriteLine("Queue is empty for this date.");
                }
            }
            else
            {
                Console.WriteLine("Invalid date.");
            }
            Console.ReadLine();
        }

        static void DisplayMenu(LinkedList<MenuItem>[] menuWhole, string[] categoryNames)
        {
            Console.WriteLine("----- Menu -----");

            int itemNumber = 1;

            for (int i = 0; i < menuWhole.Length; i++)
            {
                Console.WriteLine($"\n{categoryNames[i]}:");

                foreach (var menuItem in menuWhole[i])
                {
                    Console.WriteLine($"{itemNumber}. {menuItem.Name} - ${menuItem.Price:0.00}");
                    itemNumber++;
                }
            }
        }

        static void AddToCart(LinkedList<MenuItem>[] menuWhole, string[] categoryNames, Stack<List<CartItem>> cartStack, Stack<List<CartItem>> historyStack)
        {
            Console.WriteLine("\n\n----- Add to Cart -----");

            Console.Write("\nEnter the item number to add to the cart (type 'back' to go back, 0 to check cart)\n");
            while (true)
            {
                Console.Write("Enter: ");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "back")
                {
                    break;
                }
                if (!int.TryParse(userInput, out int itemNumber) || itemNumber < 0 || itemNumber > menuWhole.SelectMany(x => x).Count())
                {
                    Console.WriteLine("Invalid input. Please enter a valid item number or 'back'."); continue;
                }
                if (itemNumber == 0 && cartStack.Count != 0)
                {
                    Console.Clear();

                    Console.WriteLine("\n----- Shopping Cart -----\n");

                    DisplayCart(cartStack.Peek());

                    Console.WriteLine("\nOptions for cart:\n[1] Go back to adding items\n[2] Proceed to payment menu\n[3] Undo last change");
                    Console.Write("Enter option: ");
                    int cartOption = int.Parse(Console.ReadLine());

                    switch (cartOption)
                    {
                        case 1:
                            break;

                        case 2:
                            Console.WriteLine("Payment menu. Press Enter to continue...");
                            PaymentMenu(cartStack.Peek());
                            Console.ReadLine();
                            break;
                        case 3:
                            if (historyStack.Count != 0)
                            {
                                cartStack.Pop();
                                cartStack.Push(new List<CartItem>(historyStack.Pop()));
                                Console.WriteLine("\nUndo successful.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nNothing to undo.");
                            }
                            break;
                        default:
                            Console.WriteLine("\nInvalid option. Press Enter to continue...");
                            Console.ReadLine();
                            break;
                    }
                    break;
                }
                else if (itemNumber == 0 && cartStack.Count == 0)
                {
                    Console.Clear();
                    DisplayCart(cartStack.Peek());
                    Console.WriteLine("\nPress enter to go back to adding items");
                    Console.ReadLine();
                    break;
                }

                var currentCart = new List<CartItem>(cartStack.Peek());

                foreach (var categoryList in menuWhole)
                {
                    if (itemNumber <= categoryList.Count)
                    {
                        var node = categoryList.First;
                        for (int i = 1; i < itemNumber; i++)
                        {
                            node = node.Next;
                        }
                        MenuItem selectedItem = node.Value;
                        currentCart.Add(new CartItem(selectedItem.Name, selectedItem.Price));
                        Console.WriteLine($"{selectedItem.Name} added to the cart.");

                        historyStack.Push(new List<CartItem>(cartStack.Peek()));
                        cartStack.Push(currentCart);
                        break;
                    }
                    else
                    {
                        itemNumber -= categoryList.Count;
                    }
                }
            }
        }

        static void DisplayCart(List<CartItem> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
            }
            else
            {
                double total = 0;
                foreach (var item in cart)
                {
                    Console.WriteLine($"{item.Name,-25} ${item.Price:0.00}");
                    total += item.Price;
                }
                Console.WriteLine();
                Console.WriteLine($"Total: ${total:0.00}");
            }
        }

        static void PaymentMenu(List<CartItem> cart)
        {
            Console.Clear();

            int paymentChoice;
            double totalAmount;
            string address, cardNumber;

            Console.WriteLine("----- PAYMENT MENU -----\n");

            DisplayCart(cart);

            Console.Write("\nChoose payment option \n[1]Cash on Delivery(COD) \n[2]Card \nEnter: ");
            paymentChoice = int.Parse(Console.ReadLine());

            switch (paymentChoice)
            {
                case 1:

                    Console.Clear();

                    totalAmount = CalculateTotal(cart);
                    Console.WriteLine($"Total Amount: ${totalAmount:0.00}");

                    Console.Write("\nEnter your home address: ");
                    address = Console.ReadLine();

                    Console.WriteLine($"Payment successful! Thank you for your order.\nAddress: {address}");
                    PrintReceipt(cart, address);

                    break;
                case 2:
                    Console.Clear();

                    totalAmount = CalculateTotal(cart);
                    Console.WriteLine($"Total Amount: ${totalAmount:0.00}");

                    Console.Write("\nEnter your card number: ");
                    cardNumber = Console.ReadLine();

                    Console.Write("Enter your home address: ");
                    address = Console.ReadLine();

                    Console.WriteLine($"Payment successful! Thank you for your order.\nAddress: {address}");
                    PrintReceipt(cart, address);

                    break;
                default:
                    Console.WriteLine("Invalid option. Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
            Console.ReadLine();
        }

        static double CalculateTotal(List<CartItem> cart)
        {
            double total = 0; foreach (var item in cart)
            {
                total += item.Price;
            }
            return total;
        }

        static void PrintReceipt(List<CartItem> cart, string address)
        {
            Console.Clear();

            Console.WriteLine("----- Receipt -----\n");
            DisplayCart(cart);

            Console.WriteLine($"Payment successful! Thank you for your order.\nAddress: {address}");
            Console.WriteLine("\nOptions:\n[1] Order again\n[2] Exit");

            int receiptOption;
            ConsoleKeyInfo key;

            do
            {
                Console.Write("Enter option: ");
                key = Console.ReadKey();

            } while (!int.TryParse(key.KeyChar.ToString(), out receiptOption) || (receiptOption != 1 && receiptOption != 2));

            switch (receiptOption)
            {
                case 1:
                    cart.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\nExiting the program...");
                    System.Threading.Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    class CartItem

    {
        public string Name { get; }
        public double Price { get; }
        public CartItem(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    class MenuItem
    {
        public string Name { get; }
        public double Price { get; }

        public MenuItem(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
