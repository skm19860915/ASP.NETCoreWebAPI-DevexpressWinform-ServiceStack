using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public static class Categories
    {
        public static CategoryDto CategoryFirst { get; }
        public static CategoryDto CategorySecond { get; }
        public static CategoryDto CategoryThird { get; }
        public static CategoryDto CategoryFourth { get; }
        public static CategoryDto CategoryFifth { get; }
        public static CategoryDto CategorySix { get; }
        static Categories()
        {
            CategoryFirst = new CategoryDto
            {
                CategoryName = "Category one",
                Id =1
            };

            CategorySecond = new CategoryDto
            {
                CategoryName = "Category Two",
                Id = 2

            };
            CategoryThird = new CategoryDto
            {
                CategoryName = "Category Three",
                Id = 3
            };
            CategoryFourth = new CategoryDto
            {
                CategoryName = "AngularJs",
                Id = 4
            };
            CategoryFifth = new CategoryDto
            {
                CategoryName = "Asp.net core",
                Id = 5

            };
            CategorySix = new CategoryDto
            {
                CategoryName = "Angular 6",
                Id = 6
            };

        }

        public static List<CategoryDto> Get()
        {
            var list = new List<CategoryDto> {
                CategoryFirst,
                CategorySecond,
                CategoryThird,
                CategoryFourth,
                CategoryFifth,
                CategorySix
            };
            return list;
        }
    }
}
