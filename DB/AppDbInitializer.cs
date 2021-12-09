using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shhop.Models;

namespace shhop.DB
{
    public class AppDbInitializer
    {
        public static void Initial(ApplicationContext context)
        {
            if (!context.CategoryItems.Any())
            {
                context.CategoryItems.AddRange(CategoryItems.Select(c => c.Value));
            }

            if (!context.Category.Any())
            {
                context.Category.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Thing.Any())
            {
                context.AddRange(
                    new Thing
                    {

                        name = "Naruto",
                        description = "qphefpwehgwegv",
                        img = "",
                        price = 250,
                        author = "kwjpkwegj",
                        categoryId = 1,
                        category = Categories["Top"]

                    },
                    new Thing
                    {
                        name = "VE",
                        description = "qpifjpwgn",
                        img = "",
                        price = 890,
                        author = "[qojfpqwjf",
                        categoryId = 2,
                        category = Categories["Bottom"]
                    }


                    );
            }
            context.SaveChanges();


        }
        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new Category[]
                    {
                       new Category{name="Top",description="Cloth for top",categoryItemId=1,categoryItem=CategoryItems["Художественная литература"]},
                       new Category{name="Bottom",description="Cloth for bottom",categoryItemId=2,categoryItem=CategoryItems["Манга и комиксы"]}
                    };
                    category = new Dictionary<string, Category>();
                    foreach (Category el in list)
                        category.Add(el.name, el);
                }
                return category;
            }
        }
        private static Dictionary<string, CategoryItem> categoryItem;
        public static Dictionary<string, CategoryItem> CategoryItems
        {
            get
            {
                if (categoryItem == null)
                {
                    var list = new CategoryItem[]
                    {
                       new CategoryItem{name="Художественная литература",description="Художественная литература — пространство, где каждый находит то, что ему по душе. Фэнтези и фантастика, любовные романы, детективы, вечная классика, модные бестселлеры и многое другое. "},
                       new CategoryItem{name="Манга и комиксы",description="Любите истории в картинках? Значит, комиксы, артбуки, манга и графические романы — ваш формат. Осталось выбрать вид и тематику."}
                    };
                    categoryItem = new Dictionary<string, CategoryItem>();
                    foreach (CategoryItem el in list)
                        categoryItem.Add(el.name, el);
                }
                return categoryItem;
            }
        }
    }
}
