using System.Reflection;

namespace Data.Utils
{
    public class DbColumn
    {
        public PropertyInfo Property { get; set; }

        public string Name { get; set; }


        public DbColumn(PropertyInfo property, string name)
        {
            Property = property;
            Name = name;
        }
    }
}
