namespace Data.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    void Commit(string name, string type);
}
