using Internet_Market_WebApi.Abstract;
using Internet_Market_WebApi.Constants;
using Internet_Market_WebApi.Data.Entities.Identity;
using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Internet_Market_WebApi.Data.Entities
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app) 
        {
            using (var scope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<ISmtpEmailService>();
                context.Database.Migrate();
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();

                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<RoleEntity>>();

                if (!context.Roles.Any())
                {
                    RoleEntity admin = new RoleEntity
                    {
                        Name = Roles.Admin,
                    };
                    RoleEntity user = new RoleEntity
                    {
                        Name = Roles.User,
                    };
                    var result = roleManager.CreateAsync(admin).Result;
                    result = roleManager.CreateAsync(user).Result;
                }
                if (!context.Categories.Any())
                {
                    CategoryEntity pizza = new CategoryEntity()
                    {
                        Name = "Pizza",
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                    };
                    context.Categories.Add(pizza);
                    CategoryEntity drinks = new CategoryEntity()
                    {
                        Name = "Drinks",
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                    };
                    context.Categories.Add(drinks);

                    CategoryEntity deserts = new CategoryEntity()
                    {
                        Name = "Deserts",
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                    };
                    context.Categories.Add(deserts);

                    context.SaveChanges();
                }
                if (!context.Subcategories.Any())
                {
                    SubcategoryEntity onRedSause = new SubcategoryEntity()
                    {
                        Name = "Made on Red Sauce",
                        Category = context.Categories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(onRedSause);
                    SubcategoryEntity onWhiteSause = new SubcategoryEntity()
                    {
                        Name = "Made on White Sauce",
                        Category = context.Categories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(onWhiteSause);

                    SubcategoryEntity alchohol = new SubcategoryEntity()
                    {
                        Name = "Alchohol",
                        Category = context.Categories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(alchohol);

                    SubcategoryEntity soda = new SubcategoryEntity()
                    {
                        Name = "Soda",
                        Category = context.Categories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(soda);

                    SubcategoryEntity Juice = new SubcategoryEntity()
                    {
                        Name = "Juice",
                        Category = context.Categories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(Juice);

                    SubcategoryEntity water = new SubcategoryEntity()
                    {
                        Name = "Water",
                        Category = context.Categories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(water);

                    SubcategoryEntity cakes = new SubcategoryEntity()
                    {
                        Name = "Cakes",
                        Category = context.Categories.ToList()[2],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(cakes);

                    SubcategoryEntity iceCream = new SubcategoryEntity()
                    {
                        Name = "Ice Cream",
                        Category = context.Categories.ToList()[2],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(iceCream);
                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    //Pizza on red sauce
                    ProductEntity pepperoni = new ProductEntity()
                    {
                        Name = "Pepperoni Pizza",
                        Description = "You literally can't go wrong with pepperoni and mozzarella cheese. Classic for a reason.",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 10.99,
                        SalePrice = 5.99,
                        Image = "https://image.similarpng.com/very-thumbnail/2022/03/Delicious-Pepperoni-pizza-on-transparent-background-PNG.png"
                    };
                    context.Products.Add(pepperoni);
                    ProductEntity meatLover = new ProductEntity()
                    {
                        Name = "Meat Lover's® Pizza",
                        Description = "Packed with pepperoni, Italian sausage, ham, bacon, seasoned pork and beef, this pizza is NOT messing around.",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 14.99,
                        SalePrice = 0,
                        Image = "https://www.pngmart.com/files/1/Pepperoni-Pizza-Transparent-Background.png"
                    };
                    context.Products.Add(meatLover);

                    ProductEntity newYorker = new ProductEntity()
                    {
                        Name = "The Big New Yorker - Double Pepperoni",
                        Description = "It’s back! An iconic 16’’ New York-inspired pizza with 6 XL foldable slices, sweet marinara, classic and crispy cupped pepperoni & parmesan oregano seasoning on top",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 18.99,
                        SalePrice = 15.99,
                        Image = "https://www.citypng.com/public/uploads/small/11665837933adn5gvyxayk0vigrgoys7phmcq8dqin6uyastrmu8s4lzeyo7ucu3kjpm8bfcax0vuzyupw6utvl4p3appmh3wqjyomaipxei3ds.png"
                    };
                    context.Products.Add(newYorker);

                    ProductEntity supreme = new ProductEntity()
                    {
                        Name = "Supreme Pizza",
                        Description = "This loaded pizza is the perfect choice for family dinner or a lunch with your crew. Includes pepperoni, seasoned pork, beef, mushrooms, green bell peppers and onions.",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 11.99,
                        SalePrice = 7.99,
                        Image = "https://pngimg.com/d/pizza_PNG43991.png"
                    };
                    context.Products.Add(supreme);

                    //Pizza on white Sauce
                    ProductEntity hawaiian = new ProductEntity()
                    {
                        Name = "Hawaiian Chicken Pizza",
                        Description = "Give your taste buds a tropical vacation with this amped up Hawaiian pizza. It's got tasty chicken, ham, pineapple AND green peppers. ",
                        Subcategory = context.Subcategories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 11.99,
                        SalePrice = 6.99,
                        Image = "https://www.caseys.com/medias/sys_master/images/h2c/hcc/8921632604190/8429_base-400x400/8429-base-400x400.png"
                    };
                    context.Products.Add(hawaiian);

                    ProductEntity tona = new ProductEntity()
                    {
                        Name = "Pizza al tonno",
                        Description = "Italian pizza that uses tuna as a topping. It consists of a basic pizza dough that is topped with white sauce, mozzarella, and olive oil-packed tuna.",
                        Subcategory = context.Subcategories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 12.99,
                        SalePrice = 0,
                        Image = "https://tfkrestaurant.com/wp-content/uploads/2023/01/Hotpot13-450x450.png"
                    };
                    context.Products.Add(tona);

                    ProductEntity bianca = new ProductEntity()
                    {
                        Name = "Bianca",
                        Description = "fresh-baked focaccia topped with nothing more than olive oil, salt, and sesame seeds or rosemary if you're feeling adventurous.",
                        Subcategory = context.Subcategories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 9.99,
                        SalePrice = 0,
                        Image = "https://pngimg.com/d/pizza_PNG43987.png"
                    };
                    context.Products.Add(bianca);

                    ProductEntity cheese = new ProductEntity()
                    {
                        Name = "Cheese Pizza",
                        Description = "A classic cheese pizza is the ultimate crowd-pleaser. The delicious combination of crispy pizza crust, flavorful tomato sauce, and bubbly cheese make for an unbeatable combination. Even if you're a fan of unique toppings, it's hard to resist a slice of a plain cheese pie!",
                        Subcategory = context.Subcategories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 9.99,
                        SalePrice = 5.99,
                        Image = "https://valentinos.com/wp-content/uploads/2019/06/cheese.png"
                    };
                    context.Products.Add(cheese);

                    //Alchohol
                    ProductEntity aperol = new ProductEntity()
                    {
                        Name = "Aperol Spritz",
                        Description = "Aperol, an orange-red liquor invented by the Barbieri brothers in Padova in 1919, is a go-to Spritz option. Low in alcohol, pleasantly citrusy and slightly bitter, it is a light and fresh aperitif that owes its flavors and aromas to sweet and bitter oranges, rhubarb, and gentian root.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Alchohol"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 13.99,
                        SalePrice = 7.99,
                        Image = "https://img.freepik.com/premium-photo/glass-aperol-spritz-cocktail-isolated-white-background_802770-19.jpg?w=2000"
                    };
                    context.Products.Add(aperol);

                    ProductEntity redWine = new ProductEntity()
                    {
                        Name = "Red Wine",
                        Description = "Indulge in the robust flavors of our red wine, with notes of dark fruit and a smooth finish. At 12% alcohol, this wine is the perfect complement to any meal or occasion.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Alchohol"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 13.99,
                        SalePrice = 0,
                        Image = "https://media1.popsugar-assets.com/files/thumbor/XFdgEGjXrPMUmR9nABQUpm0_bvc/fit-in/2048xorig/filters:format_auto-!!-:strip_icc-!!-/2017/11/30/762/n/24155406/tmp_KvXKVY_bf971dc54d346a9c_wine.jpg"
                    };
                    context.Products.Add(redWine);

                    ProductEntity whiteWine = new ProductEntity()
                    {
                        Name = "White Wine",
                        Description = "Savor the crisp and refreshing taste of our white wine, with hints of citrus and a subtle sweetness. At 12% alcohol, this wine pairs perfectly with seafood, light pasta dishes, or as a delightful aperitif.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Alchohol"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 13.99,
                        SalePrice = 0,
                        Image = "https://annapolis-sons-and-daughters-of-italy-in-america-lodge-2225.weebly.com/uploads/1/3/1/5/131550486/s234497489680235550_p188_i1_w615.jpeg"
                    };
                    context.Products.Add(whiteWine);

                    //Soda
                    ProductEntity cola = new ProductEntity()
                    {
                        Name = "Coca Cola",
                        Description = "0.5 of classic Coca Cola soda",
                        Subcategory = context.Subcategories.First(x => x.Name == "Soda"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 3.99,
                        SalePrice = 0,
                        Image = "https://thumbs.dreamstime.com/b/coca-cola-can-white-background-chisinau-moldova-november-flavored-soft-drink-created-company-64145051.jpg"
                    };
                    context.Products.Add(cola);

                    ProductEntity fanta = new ProductEntity()
                    {
                        Name = "Fanta",
                        Description = "0.5 of classic Fanta soda",
                        Subcategory = context.Subcategories.First(x => x.Name == "Soda"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 3.99,
                        SalePrice = 0,
                        Image = "https://www.citypng.com/public/uploads/preview/hd-fanta-orange-soda-can-png-31625592657gc2rwjgkay.png"
                    };
                    context.Products.Add(fanta);

                    ProductEntity sprite = new ProductEntity()
                    {
                        Name = "Sprite",
                        Description = "0.5 of classic Sprite soda",
                        Subcategory = context.Subcategories.First(x => x.Name == "Soda"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 3.99,
                        SalePrice = 0,
                        Image = "https://st.depositphotos.com/2075661/3785/i/950/depositphotos_37857465-stock-photo-sprite-bottle-can.jpg"
                    };
                    context.Products.Add(sprite);

                    //Water
                    ProductEntity stillWater = new ProductEntity()
                    {
                        Name = "Still Water",
                        Description = "0.5 glass of still water",
                        Subcategory = context.Subcategories.First(x => x.Name == "Water"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 1.00,
                        SalePrice = 0,
                        Image = "https://www.freepnglogos.com/uploads/water-glass-png/water-glass-biolution-2.png"
                    };
                    context.Products.Add(stillWater);

                    ProductEntity sparklingL = new ProductEntity()
                    {
                        Name = "Lightly Sparkling Water",
                        Description = "0.5 glass of lightly sparkling water",
                        Subcategory = context.Subcategories.First(x => x.Name == "Water"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 1.00,
                        SalePrice = 0,
                        Image = "https://www.freepnglogos.com/uploads/water-glass-png/water-glass-biolution-2.png"
                    };
                    context.Products.Add(sparklingL);

                    ProductEntity sparkling = new ProductEntity()
                    {
                        Name = "Sparkling Water",
                        Description = "0.5 glass of sparkling water",
                        Subcategory = context.Subcategories.First(x => x.Name == "Water"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 1.00,
                        SalePrice = 0,
                        Image = "https://www.freepnglogos.com/uploads/water-glass-png/water-glass-biolution-2.png"
                    };
                    context.Products.Add(sparkling);

                    //Cakes
                    ProductEntity chocolateCake = new ProductEntity()
                    {
                        Name = "Chocolate Cake",
                        Description = "Peace of delcious soft cake flavored with melted chocolate, cocoa powder, or both.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Cakes"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 10.99,
                        SalePrice = 4.99,
                        Image = "https://www.hersheyland.com/content/dam/hersheyland/en-us/recipes/recipe-images/2_Hersheys_Perfectly_Chocolate_Cake_11-18.jpeg"
                    };
                    context.Products.Add(chocolateCake);

                    ProductEntity napoleon = new ProductEntity()
                    {
                        Name = "Napoleon",
                        Description = "Napoleon is composed of many layers of puff pastry with a whipped pastry cream filling and encrusted with more pastry crumbs",
                        Subcategory = context.Subcategories.First(x => x.Name == "Cakes"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 8.99,
                        SalePrice = 0,
                        Image = "https://letthebakingbegin.com/wp-content/uploads/2013/07/The-Best-Napoleon-Cake-is-made-with-thin-puff-pastry-layers-then-sandwiched-with-rich-and-buttery-custard.-This-Napoleon-dessert-is-one-of-my-familys-favorite-2.jpg"
                    };
                    context.Products.Add(napoleon);

                    ProductEntity strudel = new ProductEntity()
                    {
                        Name = "Apple Strudel",
                        Description = "Perfect classic Austrian strudel, made with fresh apples, baked with love.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Cakes"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://houseofnasheats.com/wp-content/uploads/2021/10/Apple-Strudel-Apfelstrudel-Square-1.jpg"
                    };
                    context.Products.Add(strudel);

                    //Ice Cream
                    ProductEntity chocolateIceCream = new ProductEntity()
                    {
                        Name = "Chocolate Ice Cream",
                        Description = "The Best Chocolate Ice Cream is a bold name, but it is equally matched by the bold chocolate flavor of this custard-based ice cream.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Ice Cream"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://joyfoodsunshine.com/wp-content/uploads/2020/06/homemade-chocolate-ice-cream-recipe-7.jpg"
                    };
                    context.Products.Add(chocolateIceCream);

                    ProductEntity vanilla = new ProductEntity()
                    {
                        Name = "Vanilla Ice Cream",
                        Description = "Super-smooth and creamy. Spot-on vanilla flavor. Oh-so cold and refreshing. Basically EVERYTHING a classic vanilla ice cream should be.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Ice Cream"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://joyfoodsunshine.com/wp-content/uploads/2020/07/homemade-vanilla-ice-cream-recipe-6.jpg"
                    };
                    context.Products.Add(vanilla);

                    ProductEntity strawberry = new ProductEntity()
                    {
                        Name = "Strawberry Ice Cream",
                        Description = "Our strawberry ice cream is so rich and creamy, with an amazing fresh strawberry taste.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Ice Cream"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://ohsweetbasil.com/wp-content/uploads/creamy-homemade-strawberry-ice-cream-recipe-6-scaled.jpg"
                    };
                    context.Products.Add(strawberry);

                    context.SaveChanges();

                }
            }
        }
    }
}
