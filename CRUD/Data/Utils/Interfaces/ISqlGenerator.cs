using Domain.Models.Base;

namespace Data.Utils
{
    public interface ISqlGenerator<T> where T : class
    {
        string SelectAll();

        string Select();

        string Insert();

        string Update();

        string Delete();
    }
}