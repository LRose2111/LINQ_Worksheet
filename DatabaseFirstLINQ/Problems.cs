using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            ProblemTwenty();
            
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var user = _context.Users;
            var numberOfUsers = user.ToList().Count();
            Console.WriteLine(numberOfUsers);

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;


            foreach (Product product in products)
            {
                if (product.Price > 150)
                {
                    Console.WriteLine(product.Name + " " + product.Price);
                }
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;
            var productsContainS = products.Where(p => p.Name.Contains("s"));
            foreach (Product product in productsContainS)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            var users = _context.Users.Where(m => m.RegistrationDate < DateTime.Parse("01/01/2016"));

            foreach (User user in users)
            {
                Console.WriteLine(user.Email + user.RegistrationDate);
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            // self-note: Reference for DateTime https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            var users = _context.Users;
            DateTime date2016 = new DateTime(2016, 01, 01);
            DateTime date2018 = new DateTime(2018, 01, 01);
            var usersRegBtwn2016And2018 = users.Where(u => u.RegistrationDate > date2016 && u.RegistrationDate < date2018);
            foreach (User user in usersRegBtwn2016And2018)
            {
                Console.WriteLine("User Email: " + user.Email + " Registered: " + user.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var scProductsWithAfton = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == "afton@gmail.com");

            foreach (ShoppingCart item in scProductsWithAfton)
            {
                Console.WriteLine($"Product: {item.Product.Name} Price: ${item.Product.Price} Quantity: {item.Quantity}"); //self-note: second dollar symbol is used to reflect $299 as price (example)
            }


        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var odaShoppingCartPrice = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();

            Console.WriteLine($"Total Price: ${odaShoppingCartPrice}");


        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var employees = _context.UserRoles.Where(ur => ur.Role.RoleName == "Employee").Select(ur => ur.User.Id);
            var scProducts = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => employees.Contains(sc.User.Id));

            foreach (ShoppingCart item in scProducts)
            {
                Console.WriteLine($"Email: {item.User.Email} Product: {item.Product.Name} Price: ${item.Product.Price} Quanity: {item.Quantity}");
            }

        }

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Elden Ring",
                Price = 70,
                Description = "RPG Video Game"
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.

            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Name == "Elden Ring").Select(p => p.Id).SingleOrDefault();

            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };

            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();

        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.

            var product = _context.Products.Where(p => p.Name == "Elden Ring").SingleOrDefault();
            product.Price = 60;
            _context.Products.Update(product);
            _context.SaveChanges();


        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.

            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }


        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var odaEmail = _context.Users.Where(ur => ur.Email == "oda@gmail.com");

            foreach (User user in odaEmail)
            {
                _context.Users.Remove(user);
            }
            _context.SaveChanges();


        }

//        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

//        private void BonusOne()
//        {
//            // Prompt the user to enter in an email and password through the console.
//            // Take the email and password and check if the there is a person that matches that combination.
//            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
//        }

//        private void BonusTwo()
//        {
//            // Write a query that finds the total of every users shopping cart products using LINQ.
//            // Display the total of each users shopping cart as well as the total of the toals to the console.
//        }

//        // BIG ONE
//        private void BonusThree()
//        {
//            // 1. Create functionality for a user to sign in via the console
//            // 2. If the user succesfully signs in
//                // a. Give them a menu where they perform the following actions within the console
//                    // View the products in their shopping cart
//                    // View all products in the Products table
//                    // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
//                    // Remove a product from their shopping cart
//            // 3. If the user does not succesfully sing in
//                // a. Display "Invalid Email or Password"
//                // b. Re-prompt the user for credentials

//        }

//    }
//}
