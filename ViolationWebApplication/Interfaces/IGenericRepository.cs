namespace ViolationWebApplication.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //Получение объекта из базы данных по id
        Task<T> Get(int id);
        //Получение всех объектов из одной таблицы базы данных
        Task<IList<T>> GetAll();
        //Добавление обекта в таблицу базы данных
        Task Add(T entity);
        //Удаление обекта из таблицы базы данных
        Task Delete(int id);
        //Удалить множество объектов из таблицы базы данных
        void DeleteRange(IEnumerable<T> entities);
        //Обновление объекта в базе данных
        void Update(T entity);
        //Подгрузка свойства объекта с типом class_name
        Task ExplicitLoading(T entity,string property);
        //Подгрузка свойства объекта с типом ICollection<class_name>
        Task ExplicitLoadingCollection(T entity,string property);
        //Подгрузка свойста объектов одного класса
        Task ExplicitLoadingRange(IEnumerable<T> entities, string property);
    }
}
