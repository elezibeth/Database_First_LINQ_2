using DatabaseFirstLINQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            ProblemOne();
            ProblemTwo();
            ProblemThree();
            ProblemFour();
            ProblemFive();
            ProblemSix();
            ProblemSeven();
            ProblemEight();
            ProblemNine();
            ProblemTen();
            ProblemEleven();
            ProblemTwelve();
            ProblemThirteen();
            ProblemFourteen();
            ProblemFifteen();
            ProblemSixteen();
            ProblemSeventeen();
            ProblemEighteen();
            ProblemNineteen();
            ProblemTwenty();
        }

        //<><><><><><><><> R Actions(Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            var userCount = _context.Users.ToList().Count();
            // HINT: .ToList().Count
            Console.WriteLine($"Problem ONE solution = {userCount}");
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine($"problem 2 {user.Email}");
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            var products = _context.Products.Where(e => e.Price > 150);

            foreach (Product item in products)
            {
                Console.WriteLine($"Problem 3:Name: " + item.Name);
                Console.WriteLine($"Problem 3: Price: " + item.Price);
            }
            // Then print the name and price of each product from the above query to the console.


        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.


            var sContaining =
            from item in _context.Products
            where item.Name.Contains("s")
            select item;

            var listS = sContaining.ToList();

            // Then print the name of each product from the above query to the console.
            foreach (Product item in listS)
            {
                Console.WriteLine($"Problem 4:  {item.Name}");
            }


        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            var users = _context.Users.Where(x => x.RegistrationDate < new DateTime(2016, 1, 1));

            foreach (User user in users)
            {
                Console.WriteLine($"Problem #5: {user.Email}");
            }

            // Then print each user's email and registration date to the console.

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            var users = _context.Users.Where(x => x.RegistrationDate > new DateTime(2016, 1, 1) && x.RegistrationDate < new DateTime(2018, 1, 1));
            // Then print each user's email and registration date to the console.
            foreach (User user in users)
            {
                Console.WriteLine($"Problem six:email: {user.Email} date:{user.RegistrationDate} password: {user.Password}");
            }

        }

        //        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"#7 Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            var userCart = _context.ShoppingCarts
                            .Include(s => s.User)
                            .Include(s => s.Product)
                            .Where(s => s.User.Email == "afton@gmail.com");

            foreach (ShoppingCart p in userCart)
            {
                Console.WriteLine($"#8 - Product name: {p.Product.Name}.  Product price: ${p.Product.Price}.  Quantity: {p.Quantity}");
            }

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" 
            var productsSum = _context.ShoppingCarts
                .Where(s => s.User.Email == "oda@gmail.com")
                .Select(s => s.Product.Price).Sum();

            Console.WriteLine($"#9 - Product's sum: {productsSum}");
            Console.ReadLine();
        }

        private void ProblemTen()
        {
            //    // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee"
            //  
            var employees = _context.UserRoles.Include(uR => uR.Role).Include(uS => uS.User).Include(usC => usC.Role.RoleName == "Employee").ToList();
                           

            var products = _context.ShoppingCarts.Include(u => u.User).Include(u => u.Product).Where(u => u.User.Id == u.UserId).ToList();
                           
            foreach (ShoppingCart sC in products)
            {
                Console.WriteLine($"Ten: User:{sC.User.Email}, price -  {sC.Product.Price}, name -  {sC.Quantity} ");
                Console.ReadLine();
            }


            //    //Then print the user's email as well as the product's name, price, and quantity to the console.

        }




        //<><><><><><><><> CUD(Create, Update, Delete) Actions<><><><><><><><><>

        //<><> C Actions(Create) <><>

        private void ProblemEleven()
        {
            //Create a new User object and add that user to the Users table using LINQ.
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
            Product product = new Product()
            {
                Name = "Silly Putty",
                Description = "Polymer toy",
                Price = 2

            };
            _context.Products.Add(product);
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
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").Select(r => r.Id).SingleOrDefault();
            var product = _context.Products.Where(p => p.Name == "Silly Putty").Select(p => p.Id).SingleOrDefault();
            ShoppingCart sC = new ShoppingCart()
            {
                UserId = user,
                ProductId = product

            };
            _context.ShoppingCarts.Add(sC);
            _context.SaveChanges();
        }






        //        // <><> U Actions (Update) <><>

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
            var product = _context.Products.Where(p => p.Name == "Silly Putty").SingleOrDefault();
            product.Price = 3;
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


        //        //        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
         

            var uRole = _context.UserRoles.Where(u => u.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(uRole);
            _context.SaveChanges();
        }

    

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ. include statement
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }



        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var userOda = _context.Users.Where(u => u.Email == "oda@gmail.com").FirstOrDefault();
            _context.Users.Remove(userOda);
            _context.SaveChanges();

        }



        //        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            Console.WriteLine("Please enter your eamil address");
            var eMail = Console.ReadLine();
            Console.WriteLine("Please enter a password");
            var password = Console.ReadLine();
            //Take the email and password and check if the there is a person that matches that combination.
            var matcheMail = _context.Users.Where(u => u.Email == eMail && u.Password == password).FirstOrDefault();

            if (matcheMail != null)
            {
                Console.WriteLine("Bonus ONE: Signed in!");
            }
            else
            {
                Console.WriteLine("Bonus One: Invalid Email or Password");
            }
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
        }

        //        private void BonusTwo()
        //        {
        //            // Write a query that finds the total of every users shopping cart products using LINQ.
        //            var usersWithCarts = _context.Users.Where(u => u.ShoppingCarts.Count >= 1).ToList();
        //            foreach(User us in usersWithCarts)
        //            {
        //                var useriD = us.Id;
        //                var allCartsForUser = _context.ShoppingCarts.Where(p => p.UserId == useriD).ToList();
        //                allCartsForUser.get


        //            }
        //            var total = _context.ShoppingCarts.Where(u => usersWithCarts.Id == )

        //            // Display the total of each users shopping cart as well as the total of the toals to the console.
        //        }

        //        //        // BIG ONE
        //        //        private void BonusThree()
        //        //        {
        //        //            // 1. Create functionality for a user to sign in via the console
        //        //            // 2. If the user succesfully signs in
        //        //                // a. Give them a menu where they perform the following actions within the console
        //        //                    // View the products in their shopping cart
        //        //                    // View all products in the Products table
        //        //                    // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
        //        //                    // Remove a product from their shopping cart
        //        //            // 3. If the user does not succesfully sing in
        //        //                // a. Display "Invalid Email or Password"
        //        //                // b. Re-prompt the user for credentials

        //        //        }


    }
}

