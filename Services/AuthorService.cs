using e_library.DTOs;
using e_library.Repositories;
using e_library.Models;
using System.Threading.Tasks;

namespace e_library.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository repo)
        {
            _authorRepository = repo;
        }

        public async Task<AuthorResult> GetAuthors()
        {
            var result = new AuthorResult();

            try
            {
                var authors = await _authorRepository.GetAuthors();
                result.success = true;
                result.authors = authors;
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

        public async Task<AuthorResult> CreateAuthor(string name, string bio)
        {
            var result = new AuthorResult();

            try
            {
                var authorExists = await _authorRepository.GetAuthorByName(name) != null;

                if (authorExists)
                {
                    result.success = false;
                    result.error = "Author already exists.";
                    result.isInternalError = false;
                    return result;
                }

                var id = await _authorRepository.AddNewAuthor(name, bio);

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

        public async Task<AuthorResult> DeleteAuthor(int id)
        {
            var result = new AuthorResult();

            try
            {
                await _authorRepository.DeleteAuthor(id);
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

        public async Task<AuthorResult> UpdateAuthor(Author updatedAuthor)
        {
            var result = new AuthorResult();

            try
            {
                var existing = await _authorRepository.GetAuthorById(updatedAuthor.id);
                if (existing == null)
                {
                    result.success = false;
                    result.error = "Author not found.";
                    return result;
                }

                // author found
                var rows = await _authorRepository.UpdateAuthor(updatedAuthor);

                if (rows > 0)
                {
                    result.success = true;
                    return result;
                }

                result.success = false;
                result.error = "Update failed.";
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
