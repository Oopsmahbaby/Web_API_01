using Web_API.Data;
using Web_API.Models;

namespace Web_API.Repositories
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> getAllBooksAsync();
        public Task<BookModel> getBookAsync(int id);
        public Task<int> addBookAsync(BookModel model);
        public Task updateBookAsync(int id,BookModel model);
        public Task deleteBookAsync(int id);
    }
}
