using e_library.DTOs;
using e_library.Repositories;

namespace e_library.Services
{
    public class GenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository repo)
        {
            _genreRepository = repo;
        }

        public async Task<GenreResult> GetGenres()
        {
            var result = new GenreResult();

            try
            {
                var genres = await _genreRepository.GetGenres();
                result.success = true;
                result.genres = genres;
                return result;
            }
            catch (Exception)
            {
                result.success = false;
                result.isInternalError = true;
                result.error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<GenreResult> CreateGenre(string name)
        {
            var result = new GenreResult();

            try
            {
                var genreExists = await _genreRepository.GetGenreByName(name) != null;

                if (genreExists)
                {
                    result.success = false;
                    result.error = "Genre already exists.";
                    result.isInternalError = false;
                    return result;
                }

                var id = await _genreRepository.AddNewGenre(name);

                result.success = true;
                result.id = id;
                return result;

            }
            catch (Exception)
            {
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }
        }

        public async Task<GenreResult> DeleteGenre(int id)
        {
            var result = new GenreResult();

            try
            {
                await _genreRepository.DeleteGenre(id);
                result.success = true;
                return result;

            }
            catch (Exception)
            {
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }
        }

    }

}
