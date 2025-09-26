using e_library.Models;

namespace e_library.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetGenres();

        Task<int> AddNewGenre(string name);

        Task<Genre?> GetGenreByName(string name);

        Task DeleteGenre(int id);

    }

}
